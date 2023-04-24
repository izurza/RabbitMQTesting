using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "crow.rmq.cloudamqp.com" };
factory.Uri = new Uri("amqps://hazsvrxh:G6d68U1hAgWu78oj-VCEHk8AD9Blyua0@crow.rmq.cloudamqp.com/hazsvrxh");
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

var routingKey = (args.Length > 0) ? args[0] : "anonymous.info";

var message = (args.Length > 1 )
    ? string.Join(", ", args.Skip(1).ToArray()) 
    : "Hello World!";

var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "topic_exchange",
    routingKey: routingKey,
    basicProperties: null,
    body: body);

Console.WriteLine($"[x] Sent '{routingKey}':'{message}'");

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();

