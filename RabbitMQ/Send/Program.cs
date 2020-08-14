using System;
using System.Text;
using RabbitMQ.Client;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    string msg = "hello world";
                    byte[] bt = Encoding.UTF8.GetBytes(msg);

                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: bt);

                    Console.WriteLine(  $"[x] send {msg}");
                }

                Console.WriteLine("press any key exit.");
                Console.ReadLine();
            }
        }
    }
}
