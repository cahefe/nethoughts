using System;
using System.Threading.Tasks;
using org.apache.zookeeper;

namespace ConsoliZookeeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<WatchedEvent, Task> todo = ev =>
            {
                var path = ev.getPath();
                var type = ev.get_Type();
                var state = ev.getState();
                Console.WriteLine($"Path: {path} + type {type} + state {state}");
                return Task.CompletedTask;
            };
            // using (IZKClient zk = new ZKClient(ev =>
            // {
            //     var path = ev.getPath();
            //     var type = ev.get_Type();
            //     var state = ev.getState();
            //     Console.WriteLine($"Path: {path} + type {type} + state {state}");
            //     return Task.CompletedTask;
            // }))
            using (IZKClient zk = new ZKClient(todo))
            {
                string valor = new Random().Next(int.MaxValue).ToString();
                //  Testa node
                var exists = zk.Exists("/testnode", true);
                //  Atualizar node
                zk.SetAsync<string>("/testnode", valor).Wait();
                //  Tenta obter um valor
                var result = zk.GetAsync<string>("/testnode").Result;
                Console.WriteLine($"Valores: registrado: {valor} - obtido: {result} - {valor.Equals(result)}");
                //  Remover node
                zk.DeleteAsync("/testnode", true).Wait();
            }

            /*
                        using (var zk = new ZooKeeperClient())
                        {
                            //  Testa node
                            var exists = zk.Exists("/testnode", true);
                            //  Tenta obter um valor
                            var data = zk.Get("/testnode").Result;
                            Console.WriteLine($"Valor: {data ?? "N/D"}");
                            //  Atualizar node
                            zk.Set("/testnode", $"Valor atualizado: {DateTime.Now}").Wait();
                            //  Remover node
                            zk.Delete("/testnode", true).Wait();
                        }
                        */
        }
    }
}
