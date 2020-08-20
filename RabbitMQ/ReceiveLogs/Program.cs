using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace ReceiveLogs
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
                    //定义exchange
                    channel.ExchangeDeclare(exchange: "logs",
                        type: ExchangeType.Fanout);

                    //定义queue，并获取名称
                    var queueName = channel.QueueDeclare().QueueName;

                    //将exchange和queue绑定
                    channel.QueueBind(queue: queueName,
                        exchange: "logs",
                        routingKey: "");

                    Console.WriteLine($" [x] Waitting for message...");

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, ea) =>
                      {
                          var msg = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                          Console.WriteLine($" [x] received message===>{msg}");
                      };

                    //启动consumer
                    channel.BasicConsume(queue: queueName,
                        autoAck: true,
                        consumer: consumer);

                    Console.WriteLine(" [x] press enter to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
