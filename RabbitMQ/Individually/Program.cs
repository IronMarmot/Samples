using RabbitMQ.Client;
using System;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace Individually
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };
            using (var connection=factory.CreateConnection())
            {
                using (var channel=connection.CreateModel())
                {
                    watch.Start();
                    channel.ConfirmSelect();
                    channel.QueueDeclare(queue: "individually",
                        durable: false,
                        exclusive: false,
                        autoDelete: true,
                        arguments: null);

                    int i = 0;
                    while (i<6000)
                    {
                        byte[] body = Encoding.UTF8.GetBytes("hello world");
                        var props = channel.CreateBasicProperties();
                        channel.BasicPublish(exchange: "", routingKey: "individually", basicProperties: props, body);
                        channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));
                        i++;
                    }
                    watch.Stop();
                    Console.WriteLine($"publish 6000 'hello world' individually expand time {watch.ElapsedMilliseconds} ms");
                    Console.ReadLine();
                }
            }
        }
    }
}
