using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace _02Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName= "123.57.233.60" };
            using (var connection=factory.CreateConnection())
            {
                using (var channel=connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "task_queue",
                        durable: true,//持久化
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    //限定同时只能接受一个消息，(Fair Dispatch)
                    //这样不会使耗时多的consumer堆积消息
                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    Console.WriteLine(" [x] Waitting for message.");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine($" [x] received {message}");

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);
                        Console.WriteLine(" [x] done");

                        //注意一点要加这句，不然会消耗内存
                        //不加的话：当consumer拒绝消息时，消息会重新发送，但是RabbitMQ不会释放没有确认的消息。
                        //可通过[sudo] rabbitmqctl[.bat] list_queues name messages_ready messages_unacknowledged调试
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };

                    channel.BasicConsume(queue: "task_queue",
                        autoAck: false,
                        consumer: consumer);

                    Console.WriteLine(" Press any key to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
