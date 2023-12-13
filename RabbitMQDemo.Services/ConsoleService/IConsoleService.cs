using RabbitMQDemo.Services.ConsoleService.Models.ConsoleMenu;

namespace RabbitMQDemo.Services.ConsoleService;

public interface IConsoleService
{
    void Menu(ConsoleMenu menu);

    void Info(string message, bool clearBefore = false);

    void Success(string message, bool clearBefore = false);

    void Error(string message, bool clearBefore = false);

    void WaitForEnter();

    string GetTextInput(string requestMessage, string errorMessage = "Please enter a valid text", bool clearBefore = false);

    ConsoleMenu CreateConsoleMenu(string title);
}