using System;
using System.Threading.Tasks;

namespace consoleAzureEventHub
{
    /// <summary>
    /// Referência de consume de 
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-standard-getstarted-send"/>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Produce events");
            // var sendEvents = new SendEvents();
            // var x = Task.Run(() => sendEvents.Run(3));
            var receiveEvents = new ReceiveEvents();
            var x = Task.Run(() => receiveEvents.Run(30));

            x.Wait();

        }
    }
}
