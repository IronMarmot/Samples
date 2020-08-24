using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Channels;

namespace Asynchronously
{
    class Program
    {
        static ConcurrentDictionary<ulong, string> outstandingConfirms = new ConcurrentDictionary<ulong, string>();

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };
            using (var connection = factory.CreateConnection())
            {
                using (var channle = connection.CreateModel())
                {
                    channle.QueueDeclare(queue: "async", durable: false, exclusive: false, autoDelete: true, arguments: null);
                    channle.ConfirmSelect();


                    channle.BasicAcks += (sender, ea) =>
                          {
                              cleanOutstandingConfirms(ea.DeliveryTag, ea.Multiple);
                          };

                    channle.BasicNacks += (sender, ea) =>
                          {
                              outstandingConfirms.TryGetValue(ea.DeliveryTag, out string body);
                              Console.WriteLine($"Message with body {body} has been nack-ed. Sequence number: {ea.DeliveryTag}, multiple: {ea.Multiple}");
                              cleanOutstandingConfirms(ea.DeliveryTag, ea.Multiple);
                          };
                    DateTime time = DateTime.Now;
                    int i = 0;
                    while (i < 6000)
                    {
                        string msg = "hello world";
                        var body = System.Text.Encoding.UTF8.GetBytes(msg);
                        IBasicProperties props = channle.CreateBasicProperties();
                        outstandingConfirms.TryAdd(channle.NextPublishSeqNo, msg);
                        channle.BasicPublish(exchange: "", routingKey: "async", basicProperties: props, body);
                        i++;
                    }
                    DateTime time1 = DateTime.Now;

                    Console.WriteLine($"publish 6000 'hello world' async expand time {(time1 - time).TotalMilliseconds} ms");
                    Console.ReadLine();
                }
            }
        }

        static void cleanOutstandingConfirms(ulong sequenceNumber, bool multiple)
        {
            if (multiple)
            {
                var confirms = outstandingConfirms.Where(o => o.Key <= sequenceNumber);
                foreach (var item in confirms)
                {
                    outstandingConfirms.TryRemove(item.Key, out _);
                }
            }
            else
            {
                outstandingConfirms.TryRemove(sequenceNumber, out _);
            }
        }
    }
}
