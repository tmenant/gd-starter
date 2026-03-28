public abstract class ConsoleCmdAbstract
{
    protected readonly Logging.Logger logger;

    public ConsoleCmdAbstract()
    {
        logger = Logging.CreateLogger(this.GetType().Name);
    }

    public abstract void Execute(string[] args);

    public abstract string[] Commands { get; }

    public abstract string Description { get; }

    public virtual string Help => Description;
}