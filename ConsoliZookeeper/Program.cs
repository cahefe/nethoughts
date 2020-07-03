using System;
using System.Threading.Tasks;

namespace ConsoliZookeeper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var zk = new ZooKeeperClient())
            {
                //  Testa node
                var exists = zk.Exists("/testnode");
                //  Tenta obter um valor
                var data = zk.Get("/testnode").Result;
                Console.WriteLine($"Valor: {data ?? "N/D"}");
                //  Atualizar node
                zk.Set("/testnode", $"Valor atualizado: {DateTime.Now}").Wait();
                //  Remover node
                zk.Delete("/testnode", true).Wait();
            }
        }
    }
}
