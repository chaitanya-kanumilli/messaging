using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMqSender
{
    internal class Send
    {
        public static void SendMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                
                //channel.QueueDeclare(queue: "hello",
                //                 durable: false,
                //                 exclusive: false,
                //                 autoDelete: false,
                //                 arguments: null);

                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                // by using default exchange and queue specified by the routingkey
                //channel.BasicPublish(exchange: "",
                //                     routingKey: "hello",
                //                     basicProperties: null,
                //                     body: body);
                channel.BasicPublish(exchange: "logs",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
