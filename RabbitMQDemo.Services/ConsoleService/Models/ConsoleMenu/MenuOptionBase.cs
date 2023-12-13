namespace RabbitMQDemo.Services.ConsoleService.Models.ConsoleMenu;

public class MenuOptionBase
{
    public string Title { get; set; }

    public Action Action { get; set; }

    public MenuOptionBase(string title, Action action)
    {
        Title = title;
        Action = action;
    }
}