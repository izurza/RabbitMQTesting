using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "crow-01.rmq.cloudamqp.com" };
factory.Uri = new Uri("amqps://hazsvrxh:G6d68U1hAgWu78oj-VCEHk8AD9Blyua0@crow.rmq.cloudamqp.com/hazsvrxh");
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
Console.WriteLine("[*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"[x] Received {message}");
    int dots = message.Split('.').Length-1;
    Thread.Sleep( dots*1000 );
    Console.WriteLine("[x] Done");

    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
};
channel.BasicConsume(queue: "hello",
    autoAck: false,
    consumer: consumer);

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();