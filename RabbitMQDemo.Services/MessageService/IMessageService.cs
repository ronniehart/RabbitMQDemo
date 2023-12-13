namespace RabbitMQDemo.Services.MessageService;

public interface IMessageService
{
    void SendMessage(string message);

    void StartListening();

    string GetQueue();

    void SetQueue(string queue);
}