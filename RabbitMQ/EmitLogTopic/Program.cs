using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace EmitLogTopic
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
                    channel.ExchangeDeclare(exchange: "logs_topic", type: ExchangeType.Topic);

                    string severity = args.Length > 0 ? args[0] : "default";
                    string msg = string.Join(" ",args.Skip(1));
                    var body = Encoding.UTF8.GetBytes(msg);

                    channel.BasicPublish(exchange: "logs_topic", routingKey: severity, basicProperties: null, body: body);

                    Console.WriteLine($"x=>[x] send mesage {msg}");
                    Console.ReadLine();
                }
            }
        }
    }
}
