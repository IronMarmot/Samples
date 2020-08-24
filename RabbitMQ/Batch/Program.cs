using RabbitMQ.Client;
using System;

namespace Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            int batchSize = 100;
            var outstandingMessageCount = 0;

            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };
            using (var connection=factory.CreateConnection())
            {
                using (var channle=connection.CreateModel())
                {
                    DateTime time = DateTime.Now;
                    channle.QueueDeclare(queue: "batch", durable: false, exclusive: false, autoDelete: true, arguments: null);
                    channle.ConfirmSelect();
                    int i = 0;
                    while (i<6000)
                    {
                        var body = System.Text.Encoding.UTF8.GetBytes("hello world");

                        var props = channle.CreateBasicProperties();
                        channle.BasicPublish(exchange: "", routingKey: "batch", props, body: body);
                        outstandingMessageCount++;
                        if (outstandingMessageCount == batchSize)
                        {
                            channle.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
                            outstandingMessageCount = 0;
                        }
                        i++;
                    }
                    if (outstandingMessageCount>0)
                    {
                        channle.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
                    }
                    DateTime time1 = DateTime.Now;

                    Console.WriteLine($"publish 6000 'hello world' batch expand time {(time1-time).TotalMilliseconds} ms");
                    Console.ReadLine();
                }
            }
        }
    }
}
