using RabbitMQDemo.Services.ConsoleService;
using RabbitMQDemo.Services.ConsoleService.Models.ConsoleMenu;
using RabbitMQDemo.Services.MessageService;

namespace RabbitMQDemo.Receiver;

public class Application
{
    private readonly IConsoleService _consoleService;
    private readonly IMessageService _messageService;

    public Application(IConsoleService consoleService, IMessageService messageService)
    {
        _consoleService = consoleService;
        _messageService = messageService;
    }

    public void Run()
    {
        var mainMenu = BuildMainMenu();
        _consoleService.Menu(mainMenu);
    }

    private ConsoleMenu BuildMainMenu()
    {
        var mainMenu = _consoleService.CreateConsoleMenu("RabbitMQ Receiver Demo");
        mainMenu.AddOption(new MenuOptionBase("Select/Create Queue", SetQueue));
        mainMenu.AddOption(new MenuOptionBase("Start Listening", ListenMessages));

        return mainMenu;
    }

    private void SetQueue()
    {
        var queue = _consoleService.GetTextInput("Please enter the name of the queue", clearBefore: true);
        _messageService.SetQueue(queue);
    }

    private void ListenMessages()
    {
        var queue = _messageService.GetQueue();
        _consoleService.Info($"Listening to messages from queue \"{queue}\"...", true);
        _messageService.StartListening();
    }
}