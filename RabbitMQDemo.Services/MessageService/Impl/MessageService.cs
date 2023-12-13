using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQDemo.Services.MessageService.Impl;

public class MessageService : IMessageService
{
    private readonly IConnection _connection;
    private string _queue;

    public MessageService(IConfiguration configuration)
    {
        var host = configuration.GetRequiredSection("RabbitMQConnection:Host").Value;
        var connectionFactory = new ConnectionFactory() { HostName = host };
        _connection = connectionFactory.CreateConnection();
        _queue = "DefaultQueue";
    }

    public void SendMessage(string message)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(_queue, false, false, false, null);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(string.Empty, _queue, null, body);
    }

    public void StartListening()
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(_queue, false, false, false, null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(message);
        };

        channel.BasicConsume(_queue, true, consumer);
        Console.ReadLine();
    }

    public string GetQueue()
    {
        return _queue;
    }

    public void SetQueue(string queue)
    {
        _queue = queue;
    }
}