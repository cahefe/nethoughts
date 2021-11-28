using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;

namespace consoleAzureEventHub
{
    public class SendEvents
    {
        /// <summary>
        /// Connection string to the Event Hubs namespace
        /// </summary>
        const string connectionString = "Endpoint=sb://cahefeeventhubnamespae.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tKgc4Gt9Fp3MERCb71wcxovoFoC8JQHxee7mj/XLpLg=";

        /// <summary>
        /// Mame of the event hub
        /// </summary>
        const string eventHubName = "1steventhub";

        // /// <summary>
        // /// number of events to be sent to the event hub
        // /// </summary>
        // const int numOfEvents = 3;

        /// <summary>
        /// Produz eventos em um Azure EventHub
        /// </summary>
        /// <param name="numOfEvents">Número de eventos a produzir</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Run(int numOfEvents)
        {
            // Create a producer client that you can use to send events to an event hub
            var producerClient = new EventHubProducerClient(connectionString, eventHubName);

            // Create a batch of events 
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            for (int i = 1; i <= numOfEvents; i++)
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {i}"))))
                {
                    // if it is too large for the batch
                    throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
                }

            try
            {
                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine($"A batch of {numOfEvents} events has been published.");
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }
    }
}
