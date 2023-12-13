namespace RabbitMQDemo.Services.ConsoleService.Models.ConsoleMenu;

public class ConsoleMenu : MenuOptionBase
{
    public List<MenuOptionBase> Options { get; set; }

    public ConsoleMenu(string title) : base(title, null)
    {
        Options = new List<MenuOptionBase>();
    }

    public void AddOption(MenuOptionBase option)
    {
        Options.Add(option);
    }
}