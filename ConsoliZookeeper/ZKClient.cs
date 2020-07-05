using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using org.apache.zookeeper;
// using static org.apache.zookeeper.Watcher.Event;
// using static org.apache.zookeeper.ZooDefs;

namespace ConsoliZookeeper
{
    public class ZKClient : Watcher, IZKClient
    {
        JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            // PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true,
        };
        ZooKeeper _zkClient;
        readonly string _servers; //"localhost:2181,localhost:2888,localhost:3888"
        readonly int _connectionTimeout;
        readonly Func<WatchedEvent, Task> _watcherDelegate;

        public ZKClient(Func<WatchedEvent, Task> watcherDelegate = null)
        {
            _servers = "localhost:2181";
            _connectionTimeout = 30000;
            _watcherDelegate = watcherDelegate;
            Connect();
        }
        public Task Connect()
        {
            _zkClient = new ZooKeeper(_servers, _connectionTimeout, this);
            return Task.CompletedTask;
        }

        public async Task Disconnect() => await _zkClient.closeAsync();

        public async void Dispose() => await Disconnect();

        org.apache.zookeeper.data.Stat GetStat(string nodeName, bool watch = false) => _zkClient.existsAsync(nodeName, watch ? this : null).Result;
        public bool Exists(string nodeName, bool watch = false) => GetStat(nodeName, watch) != null;
        public async Task SetAsync<TContent>(string nodeName, TContent value, CreateModeEnum createMode = CreateModeEnum.PERSISTENT)
        {
            byte[] nodeData = JsonSerializer.SerializeToUtf8Bytes(value, typeof(TContent), _jsonOptions);

            if (await _zkClient.existsAsync(nodeName) == null)  //  Cria..,
            {
                var zkCreateMode = createMode.Equals(CreateModeEnum.PERSISTENT) ? CreateMode.PERSISTENT : CreateMode.EPHEMERAL;
                await _zkClient.createAsync(nodeName, nodeData, null, zkCreateMode);
            }
            else  //  Atualiza..,
                await _zkClient.setDataAsync(nodeName, nodeData);
        }
        public async Task<TContent> GetAsync<TContent>(string nodeName)
        {
            if (await _zkClient.existsAsync(nodeName) != null)
            {
                var dataResult = await _zkClient.getDataAsync(nodeName);
                if (dataResult != null)
                {
                    var content = Encoding.UTF8.GetString(dataResult.Data);
                    return JsonSerializer.Deserialize<TContent>(content, _jsonOptions);
                }
            }
            return default;
        }
        public async Task DeleteAsync(string nodeName, bool recursively = false)
        {

            // var node = await _zkClient.getDataAsync(nodeName, this);
            var node = GetStat(nodeName, false);
            if (node == null)
                return;

            //  Avalia se há filhos e deve apagar todos recursivamente...
            if (node.getNumChildren() > 0 && recursively)
            {
                var nodes = await _zkClient.getChildrenAsync(nodeName, this);
                if (nodes != null)
                    foreach (var childNode in nodes.Children)
                        await DeleteAsync(nodeName + '/' + childNode, recursively);
            }
            await _zkClient.deleteAsync(nodeName);
        }
        public override Task process(WatchedEvent @event)
        {
            if (_watcherDelegate != default)
                return _watcherDelegate(@event);
            return Task.CompletedTask;
        }
        /*
        {
            var path = @event.getPath();
            var type = @event.get_Type();
            var state = @event.getState();
            Console.WriteLine($"Path: {path} + type {type} + state {state}");

            switch (@event.get_Type())
            {
                case EventType.NodeDeleted:
                    //Node deleted, clear the leader so it'll recheck
                    _isLeader.Clear();
                    break;
                case EventType.None:
                    switch (@event.getState())
                    {
                        case KeeperState.SyncConnected:
                            //When connected add the root node
                            await AddRootNode();
                            break;
                        case KeeperState.Disconnected:
                            //When disconnected, reconnect and recheck leader
                            _isLeader.Clear();
                            await Connect();
                            break;
                    }
                    break;
            }
        }
        */
    }
}
