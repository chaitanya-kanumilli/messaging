using System; // Namespace for Console output
using System.Configuration; // Namespace for ConfigurationManager
using System.Threading.Tasks; // Namespace for Task
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage

namespace AzureQueueSender
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=marketdev1;AccountKey=zJV/clWxgQ+xbpImkyi2tH0lbQ3Jr5RmLMC9EumiD+YcMmtpPfLvIx3BdtZlmDNUNv22TmWMaMx9EGwpfGq52A==;EndpointSuffix=core.windows.net";
            //CreateQueueClient("marketdev1queue", storageConnectionString);
            //CreateQueue("marketdev1queue", storageConnectionString);
            InsertMessage("marketdev1queue1", "hello chai 1", storageConnectionString);

            Console.WriteLine("Hello World!");
        }
        //-------------------------------------------------
        // Create the queue service client
        //-------------------------------------------------
        public static void CreateQueueClient(string queueName, string connectionString)
        {
            // Get the connection string from app settings
            //string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);
        }

        //-------------------------------------------------
        // Create a message queue
        //-------------------------------------------------
        public static bool CreateQueue(string queueName, string connectionString)
        {
            try
            {
                // Get the connection string from app settings
                //string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

                // Instantiate a QueueClient which will be used to create and manipulate the queue
                QueueClient queueClient = new QueueClient(connectionString, queueName);

                // Create the queue
                queueClient.CreateIfNotExists();

                if (queueClient.Exists())
                {
                    Console.WriteLine($"Queue created: '{queueClient.Name}'");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n\n");
                Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return false;
            }
        }

        //-------------------------------------------------
        // Insert a message into a queue
        //-------------------------------------------------
        public static void InsertMessage(string queueName, string message, string connectionString)
        {
            // Get the connection string from app settings
            //string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                queueClient.SendMessage(message);
                Console.WriteLine($"Inserted: {message}");

                //// Peek at the next message
                //PeekedMessage[] peekedMessage = queueClient.PeekMessages();

                //// Display the message
                //Console.WriteLine($"Peeked message: '{peekedMessage[0].Body}'");

                QueueProperties properties = queueClient.GetProperties();

                // Retrieve the cached approximate message count.
                int cachedMessagesCount = properties.ApproximateMessagesCount;

                // Display number of messages.
                Console.WriteLine($"Number of messages in queue: {cachedMessagesCount}");

                // Get the message from the queue
                QueueMessage[] retrievedMessages = queueClient.ReceiveMessages(cachedMessagesCount);

                //// Update the message contents
                //queueClient.UpdateMessage(retrievedMessages[0].MessageId,
                //        retrievedMessages[0].PopReceipt,
                //        "Updated contents",
                //        TimeSpan.FromSeconds(60.0)  // Make it invisible for another 60 seconds
                //    );
                foreach (QueueMessage retrievedMessage in retrievedMessages)
                {
                    // Process (i.e. print) the message in less than 30 seconds
                    Console.WriteLine($"Dequeued message: '{retrievedMessage.Body}'");

                    // Delete the message
                    queueClient.DeleteMessage(retrievedMessage.MessageId, retrievedMessage.PopReceipt);
                }

                //// Delete the queue
                //queueClient.Delete();

            }

            
        }
    }
}
