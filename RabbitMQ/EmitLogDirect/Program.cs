using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace EmitLogDirect
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };

            using (var connection=factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "directLog", type: ExchangeType.Direct);

                    string severity = args.Length > 0 ? args[0] : "info";

                    string msg = args.Length > 1 ? string.Join(" ", args.Skip(1)).ToString() : "Hello World";

                    var body = Encoding.UTF8.GetBytes(msg);

                    channel.BasicPublish(exchange: "directLog",
                        routingKey: severity,
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($" [x] send message {severity}: {msg}");

                    Console.WriteLine("press enter to exit.");
                    Console.ReadLine();
                }
            }

            
        }
    }
}
