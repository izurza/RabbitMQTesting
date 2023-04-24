using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "crow.rmq.cloudamqp.com" };
factory.Uri = new Uri("amqps://hazsvrxh:G6d68U1hAgWu78oj-VCEHk8AD9Blyua0@crow.rmq.cloudamqp.com/hazsvrxh");
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


var queueName = channel.QueueDeclare().QueueName;

channel.ExchangeDeclare("logs", ExchangeType.Fanout);

var message = GetMessage(args);

var body = Encoding.UTF8.GetBytes(message);

channel.QueueBind(queue:queueName,
    exchange: "logs",
    routingKey: string.Empty,
   );

Console.WriteLine($"[x] Sent {message}");

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}