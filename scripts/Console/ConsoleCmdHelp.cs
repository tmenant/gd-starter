using System.Linq;
using Godot;


public class ConsoleCmdHelp : ConsoleCmdAbstract
{
    public override string[] Commands => ["help", "ls"];

    public override string Description => "Show description of all available commands.";

    public override void Execute(string[] args)
    {
        logger.Info($"Available commands:");

        var text = ConsoleManager.Commands
            .Select(GetCmdDescription)
            .OrderBy(cmd => cmd)
            .ToArray()
            .Join("\n");

        ConsoleManager.Log($"[ul]{text}[/ul]");

        // GD Console don't support [ul] tags, here is why we log it differently
        logger.Log(text, toConsole: false);
    }

    private string GetCmdDescription(ConsoleCmdAbstract cmd)
    {
        var commands = cmd.Commands.Join(", ");
        var description = cmd.Description;

        return $"  [b][i]{commands}[/i][/b] -- {description}";
    }
}