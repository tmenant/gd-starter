public class ConsoleCmdClear : ConsoleCmdAbstract
{
    public override string[] Commands => ["clear", "cls"];

    public override string Description => "Clear the logs history in the console.";

    public override void Execute(string[] args)
    {
        ConsoleManager.ClearHistory();
    }
}