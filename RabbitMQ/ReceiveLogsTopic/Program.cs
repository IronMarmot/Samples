using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ReceiveLogsTopic
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
                    channel.ExchangeDeclare(exchange: "logs_topic", type: ExchangeType.Topic);

                    string queueName = channel.QueueDeclare().QueueName;

                    if (args.Length<1)
                    {
                        Console.WriteLine("please input correct args");
                        return;
                    }

                    foreach (var item in args)
                    {
                        channel.QueueBind(queue: queueName,
                            exchange: "logs_topic",
                            routingKey: item);
                    }

                    Console.WriteLine("x=>[x] Waiting for message...");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                      {
                          var msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                          Console.WriteLine($"x=>[x] recevied {ea.RoutingKey}:{msg}");
                      };

                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                    Console.ReadLine();
                }
            }
        }
    }
}
