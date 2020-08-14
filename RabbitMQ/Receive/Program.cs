using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace Receive
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };
            using (var connection=factory.CreateConnection())
            {
                using (var channel=connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += Consumer_Received;
                    consumer.Received += (sender, e) =>
                    {
                        var body=e.Body;
                        string msg = Encoding.UTF8.GetString(body.ToArray());
                        Console.WriteLine($"[x] receive{msg}");
                    };

                    channel.BasicConsume(queue: "hello",
                        autoAck: true,
                        consumer: consumer);

                    Console.WriteLine("press any key exit.");
                    Console.ReadLine();
                }
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
