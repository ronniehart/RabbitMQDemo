using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQDemo.Services.ConsoleService;
using RabbitMQDemo.Services.ConsoleService.Impl;
using RabbitMQDemo.Services.MessageService;
using RabbitMQDemo.Services.MessageService.Impl;

namespace RabbitMQDemo.Sender;

public class Pogram
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var serviceProvider = new ServiceCollection()
            .AddTransient<IConsoleService, ConsoleService>()
            .AddTransient<IMessageService, MessageService>()
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<Application>()
            .BuildServiceProvider();

        var application = serviceProvider.GetRequiredService<Application>();
        application.Run();
    }
}