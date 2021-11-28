using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;

namespace consoleAzureEventHub
{
    public class ReceiveEvents
    {
        /// <summary>
        /// Connection string to the Event Hubs namespace
        /// </summary>
        const string eventhubConnectionString = "Endpoint=sb://cahefeeventhubnamespae.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tKgc4Gt9Fp3MERCb71wcxovoFoC8JQHxee7mj/XLpLg=";

        /// <summary>
        /// Mame of the event hub
        /// </summary>
        const string eventHubName = "1steventhub";

        const string blobStorageConnectionString = "BlobEndpoint=https://storageaccountcahefe.blob.core.windows.net/;QueueEndpoint=https://storageaccountcahefe.queue.core.windows.net/;FileEndpoint=https://storageaccountcahefe.file.core.windows.net/;TableEndpoint=https://storageaccountcahefe.table.core.windows.net/;SharedAccessSignature=sv=2020-08-04&ss=bfqt&srt=sco&sp=rwdlacuptfx&se=2021-10-03T04:04:17Z&st=2021-10-02T20:04:17Z&spr=https&sig=LpqlhuDTrYQmkQqlYIYxWjgAkhlAsH%2BqRKnLFwSwm7s%3D";
        const string blobContainerName = "cahefe-event-blobcointainer";

        static BlobContainerClient storageClient;

        /// <summary>
        /// The Event Hubs client types are safe to cache and use as a singleton for the lifetime
        /// of the application, which is best practice when events are being published or read regularly.
        /// </summary>
        EventProcessorClient processor;

        /// <summary>
        /// Produz eventos em um Azure EventHub
        /// </summary>
        /// <param name="timeToDelay">Intervalo (em segundos) para aguardar por novas mensagens</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Run(int timeToDelay)
        {
            // Read from the default consumer group: $Default
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            // Create a blob container client that the event processor will use 
            storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            // Create an event processor client to process events in the event hub
            processor = new EventProcessorClient(storageClient, consumerGroup, eventhubConnectionString, eventHubName);

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
            await processor.StartProcessingAsync();

            // Wait for 30 seconds for the events to be processed
            await Task.Delay(TimeSpan.FromSeconds(timeToDelay));

            // Stop the processing
            await processor.StopProcessingAsync();
        }

        async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            Console.WriteLine("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));

            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            Console.WriteLine($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            Console.WriteLine(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }
}
