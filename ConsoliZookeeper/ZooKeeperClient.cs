using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.apache.zookeeper;
using static org.apache.zookeeper.Watcher.Event;
using static org.apache.zookeeper.ZooDefs;

namespace ConsoliZookeeper
{
    public class ZooKeeperClient : Watcher, IZooKeeperClient
    {
        private readonly string _uniqueGuid;
        private readonly string _nodeName;
        private readonly IDictionary<string, bool> _isLeader;
        private readonly ICollection<string> _nodeAdded;
        private ZooKeeper _zookeeper;
        // private bool _leaderCheckReady = false;
        private string _zookeeperServers = "localhost:2181";   //"localhost:2181,localhost:2888,localhost:3888"

        public ZooKeeperClient()
        {
            _zookeeper = new ZooKeeper(_zookeeperServers, 30000, this);

            //  Conteúdo irrelevante... baseado em outos exemplos
            _nodeName = "/testnode";
            _uniqueGuid = Guid.NewGuid().ToString();
            _isLeader = new Dictionary<string, bool>();
            _nodeAdded = new Collection<string>();
        }
        public bool Exists(string nodeName, bool watch = false) => _zookeeper.existsAsync(nodeName, watch) != null;
        public async Task Set(string nodeName, string nodeValue)
        {
            byte[] data = Encoding.ASCII.GetBytes(nodeValue);

            var nodeExists = await _zookeeper.existsAsync(nodeName);
            if (nodeExists != null)
            {
                await _zookeeper.setDataAsync(nodeName, data);  //  Atualiza..,
            }
            else
            {
                await _zookeeper.createAsync(nodeName, data, null, CreateMode.PERSISTENT);  //  cria...
            }
        }
        public async Task<string> Get(string nodeName)
        {
            var node = await _zookeeper.existsAsync(nodeName);
            if (node != null)
            {
                var dataResult = await _zookeeper.getDataAsync(nodeName);
                if (dataResult != null)
                    return Encoding.ASCII.GetString(dataResult.Data);
            }
            return default;
        }
        public async Task Delete(string nodeName, bool recursively = false)
        {
            var node = await _zookeeper.existsAsync(nodeName);
            if (node == null)
                return;

            if (!node.getNumChildren().Equals(0) && recursively)
            {
                var nodes = await _zookeeper.getChildrenAsync(nodeName);
                if (nodes != null)
                    foreach (var childNode in nodes.Children)
                        await Delete(nodeName + '/' + childNode, recursively);
            }
            await _zookeeper.deleteAsync(nodeName);
        }
        public Task Connect()
        {
            _zookeeper = new ZooKeeper(_zookeeperServers, 30000, this);
            return Task.CompletedTask;
        }

        public async Task Disconnect() =>
            await _zookeeper.closeAsync();

        public async void Dispose()
        {
            await Disconnect();
        }
        public override async Task process(WatchedEvent @event)
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



        public async Task<bool> IsLeader(string service)
        {
            if (_isLeader.Any() && _isLeader.TryGetValue(service, out var result))
                return result;
            return await CheckLeader(service);

        }


        private async Task AddRootNode()
        {
            var rootNode = await _zookeeper.existsAsync(_nodeName);
            if (rootNode == null)
                //Add application node
                await _zookeeper.createAsync(_nodeName, Encoding.UTF8.GetBytes(_nodeName), Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
            // _leaderCheckReady = true;
        }

        private async Task<bool> CheckLeader(string service)
        {
            try
            {
                //if (!_leaderCheckReady) return false;
                var path = $"{_nodeName}/{service}";
                if (!_nodeAdded.Any(x => x == service))
                {
                    var tenantNode = await _zookeeper.existsAsync(path);
                    if (tenantNode == null)
                        //Add service node
                        await _zookeeper.createAsync(path, Encoding.UTF8.GetBytes(path), Ids.OPEN_ACL_UNSAFE, CreateMode.PERSISTENT);
                    //Add specific service node
                    await _zookeeper.createAsync($"{path}/n_", Encoding.UTF8.GetBytes(_uniqueGuid), Ids.OPEN_ACL_UNSAFE, CreateMode.EPHEMERAL_SEQUENTIAL);
                    _nodeAdded.Add(service);
                }

                var childNodes = (await _zookeeper.getChildrenAsync(path)).Children.OrderBy(x => x);
                //Get data of the leader, and set watch variable to true
                var leadChild = await _zookeeper.getDataAsync($"{path}/{childNodes.First()}", true);
                var leaderData = Encoding.UTF8.GetString(leadChild.Data);
                _isLeader[service] = leaderData == _uniqueGuid;
                return _isLeader[service];
            }
            catch
            {
                return false;
            }
        }
    }
}
