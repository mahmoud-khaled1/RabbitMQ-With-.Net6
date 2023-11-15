// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client.Events;

Console.WriteLine("Welcome to Ticketing Service , I'm Consummer ");



var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/"
};
var conn = factory.CreateConnection();

using var channel = conn.CreateModel();

channel.QueueDeclare("bookings123", durable: false, exclusive: true, autoDelete: false,arguments: null);


var consummer = new EventingBasicConsumer(channel);

consummer.Received += (model, eventArgs) =>
{
    //getting my byte[]
    var body = eventArgs.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" new Ticket processing is Initiated for  -  {message}");
};

channel.BasicConsume("bookings123", true, consummer);

Console.ReadKey();