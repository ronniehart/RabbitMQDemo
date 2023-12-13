using RabbitMQDemo.Services.ConsoleService.Models.ConsoleMenu;

namespace RabbitMQDemo.Services.ConsoleService.Impl;

public class ConsoleService : IConsoleService
{
    private ConsoleColor _defaultForegroundColor { get; set; } = ConsoleColor.White;
    private ConsoleColor _defaultBackgroundColor { get; set; } = ConsoleColor.Black;
    private ConsoleColor _successForegroundColor { get; set; } = ConsoleColor.White;
    private ConsoleColor _successBackgroundColor { get; set; } = ConsoleColor.Green;
    private ConsoleColor _errorForegroundColor { get; set; } = ConsoleColor.White;
    private ConsoleColor _errorBackgroundColor { get; set; } = ConsoleColor.Red;

    public void Menu(ConsoleMenu menu)
    {
        var close = false;
        while (!close)
        {
            Console.Clear();
            Title(menu.Title);

            for (var i = 0; i < menu.Options.Count; i++)
            {
                Console.WriteLine($"{i}: {menu.Options[i].Title}");
            }
            Console.WriteLine($"{menu.Options.Count}: Close");

            var inputText = Console.ReadLine();
            if (!int.TryParse(inputText, out var input))
            {
                Error("Please enter a numeric value");
                WaitForEnter();
            }
            else if (input < 0 || input > menu.Options.Count)
            {
                Error("Please choose one of the given options above");
                WaitForEnter();
            }
            else if (input == menu.Options.Count)
            {
                close = true;
            }
            else
            {
                var chosenOption = menu.Options[input];
                chosenOption.Action();
            }

            Console.Clear();
        }
    }

    public void Info(string message, bool clearBefore)
    {
        if (clearBefore)
        {
            Console.Clear();
        }

        Console.WriteLine(message);
    }

    public void Success(string message, bool clearBefore)
    {
        if (clearBefore)
        {
            Console.Clear();
        }

        ApplySuccessColors();
        Console.WriteLine(message);
        ApplyDefaultColors();
    }

    public void Error(string message, bool clearBefore = false)
    {
        if (clearBefore)
        {
            Console.Clear();
        }

        ApplyErrorColors();
        Console.WriteLine(message);
        ApplyDefaultColors();
    }

    public void WaitForEnter()
    {
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    public string GetTextInput(string requestMessage, string errorMessage, bool clearBefore)
    {
        if (clearBefore)
        {
            Console.Clear();
        }

        Console.Write($"{requestMessage}: ");
        var input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
        {
            Error(errorMessage);
            WaitForEnter();
            return GetTextInput(requestMessage, errorMessage, clearBefore);
        }
        return input;
    }

    public ConsoleMenu CreateConsoleMenu(string title)
    {
        var consoleMenu = new ConsoleMenu(title);
        consoleMenu.Action = () => Menu(consoleMenu);

        return consoleMenu;
    }

    private void Title(string title)
    {
        Console.WriteLine(title);
        foreach (var character in title)
        {
            Console.Write("=");
        }
        Console.Write("\n");
    }

    private void ApplyDefaultColors()
    {
        Console.ForegroundColor = _defaultForegroundColor;
        Console.BackgroundColor = _defaultBackgroundColor;
    }

    private void ApplySuccessColors()
    {
        Console.ForegroundColor = _successForegroundColor;
        Console.BackgroundColor = _successBackgroundColor;
    }

    private void ApplyErrorColors()
    {
        Console.ForegroundColor = _errorForegroundColor;
        Console.BackgroundColor = _errorBackgroundColor;
    }
}