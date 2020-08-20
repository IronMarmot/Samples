using RabbitMQ.Client;
using System;

namespace EmitLog
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

                    var msg = GetMessage(args);
                    byte[] body = System.Text.Encoding.UTF8.GetBytes(msg);

                    channel.BasicPublish(exchange: "logs",
                        routingKey: "",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($" [x] send message===>{msg}");
                }

                Console.WriteLine("press enter to exit.");
                Console.ReadLine();
            }
        }

        static string GetMessage(string[] args)
        {
            return args.Length > 0 ? string.Join(" ", args) : "Hello World!";
        }
    }
}
