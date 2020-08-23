using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Reflection;
using System.Text;

namespace Server
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
                    channel.QueueDeclare(queue: "rpc_queue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queue: "rpc_queue", autoAck: true, consumer: consumer);
                    Console.WriteLine("Waiting RPC requests");
                    consumer.Received += (sender, ea) =>
                      {
                          string response_msg = null;
                          try
                          {
                              //接收消息
                              var msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                              response_msg = fobi(int.Parse(msg)).ToString();
                              Console.WriteLine($"Received {msg}==>fobi({msg})={response_msg}");
                          }
                          catch (Exception e)
                          {
                              Console.WriteLine(" [.] "+e.Message);
                              response_msg = "";
                          }
                          finally
                          {
                              var response_queueName = ea.BasicProperties.ReplyTo;
                              string correlationId = ea.BasicProperties.CorrelationId;

                              var replyProps = channel.CreateBasicProperties();
                              replyProps.CorrelationId = correlationId;

                              //发送返回消息
                              var response_dody = Encoding.UTF8.GetBytes(response_msg);
                              channel.BasicPublish(exchange: "",
                              routingKey: response_queueName,
                              basicProperties: replyProps,
                              body: response_dody);
                              channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                          }
                      };
                    Console.WriteLine("Press enter to exit.");
                    Console.ReadLine();
                }
            }
        }

        static int fobi(int i)
        {
            if (i == 0 || i == 1) return i;
            return fobi(i - 1) + fobi(i - 2);
        }
    }
}
