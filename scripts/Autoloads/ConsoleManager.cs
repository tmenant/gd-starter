using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Godot;


public partial class ConsoleManager : Node
{
    private static readonly Logging.Logger logger = Logging.CreateLogger<ConsoleManager>();

    public static List<ConsoleCmdAbstract> Commands { get; private set; } = new();

    public static List<string> LogsHistory { get; private set; } = new();

    public override void _Ready()
    {
        Commands = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(IsConsoleCmd)
            .Select(t => (ConsoleCmdAbstract)Activator.CreateInstance(t))
            .ToList();
    }

    private static bool IsConsoleCmd(Type t)
    {
        return t.IsClass && !t.IsAbstract && typeof(ConsoleCmdAbstract).IsAssignableFrom(t);
    }

    public static void ExecuteCommand(string command)
    {
        var args = command.Split(" ");

        var cmd = Commands
            .Where(cmd => cmd.Commands.Contains(args[0]))
            .FirstOrDefault(defaultValue: null);

        if (cmd == null)
        {
            logger.Warning($"Invalid command: '{args[0]}' ({command})");
            return;
        }

        try
        {
            cmd.Execute(args);
        }
        catch (Exception e)
        {
            logger.Error(e);
        }
    }

    public static void Log(string message)
    {
        LogsHistory.Add(message);
    }

    public static void ClearHistory()
    {
        LogsHistory.Clear();
    }
}
