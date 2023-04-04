﻿using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "crow.rmq.cloudamqp.com" };
factory.UserName = "****";
factory.Password = "****";
factory.Port = 1883;
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


channel.QueueDeclare(queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

const string message = "Hello World!";

var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: string.Empty,
    routingKey: "hello",
    basicProperties: null,
    body: body);

Console.WriteLine($"[x] Sent {message}");

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();