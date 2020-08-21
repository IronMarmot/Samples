using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ReceiveLogDirect
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };

            using (var connection = factory.CreateConnection())
            //{
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "directLog", type: ExchangeType.Direct);

                    var queueName = channel.QueueDeclare().QueueName;

                    if (args.Length<1)
                    {
                        Console.Error.WriteLine($"Usage: {Environment.GetCommandLineArgs()[0]} [info] [warning] [error]");
                        Environment.ExitCode = 1;
                        return;
                    }

                    foreach (var severity in args)
                    {
                        channel.QueueBind(queue: queueName,
                            exchange: "directLog",
                            routingKey: severity);
                    }

                    Console.WriteLine("Waitting for message");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                      {
                          var body = ea.Body.ToArray();
                          var msg = Encoding.UTF8.GetString(body);
                          var routingKey = ea.RoutingKey;
                          Console.WriteLine($"[x] received {routingKey}: {msg}");
                      };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                
                Console.WriteLine("press enter to exit.");
                Console.ReadLine();
            }
            //}

            
        }
    }
}
