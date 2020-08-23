using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace Client
{
    class Program
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public Program()
        {
            var factory = new ConnectionFactory() { HostName = "123.57.233.60" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            props.CorrelationId = Guid.NewGuid().ToString();
            props.ReplyTo = replyQueueName;

            consumer.Received += (sender, ea) =>
              {
                  var msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                  if (ea.BasicProperties.CorrelationId==props.CorrelationId)
                  {
                      respQueue.Add(msg);
                  }
              };
        }

        public string Call(string msg)
        {
            var body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish(exchange: "",
                routingKey: "rpc_queue",
                basicProperties: props,
                body: body);

            channel.BasicConsume(queue: replyQueueName, autoAck: true, consumer: consumer);

            return respQueue.Take();
        }
            

        public void Close()
        {
            connection.Close();
        }
        static void Main(string[] args)
        {
            var client = new Program();
            Console.WriteLine(" [x] Requesting fobi(30)");

            var response = client.Call("30");

            Console.WriteLine($" [.] Got {response}");
            client.Close();
            Console.ReadLine();
        }
    }
}
