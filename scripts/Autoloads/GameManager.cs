using Godot;

public partial class GameManager : Node
{
    private static readonly Logging.Logger logger = Logging.CreateLogger<GameManager>();

    public static readonly Settings settings = new Settings();

    public static GameManager Instance { get; private set; }

    private bool IsPaused = false;

    public bool MainSceneLoaded => GetTree().CurrentScene.Name == "World";

    public GameManager()
    {
        if (Instance != null)
        {
            throw new System.Exception("Duplicate GameManager");
        }

        Instance = this;
    }

    public override void _Process(double delta)
    {
        UI_DebugHUD.Instance.SetText("fps", Engine.GetFramesPerSecond());
        UI_DebugHUD.Instance.SetText("drawCalls", Performance.GetMonitor(Performance.Monitor.RenderTotalDrawCallsInFrame));
        UI_DebugHUD.Instance.SetText("primitives", Performance.GetMonitor(Performance.Monitor.RenderTotalPrimitivesInFrame));
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;
        GetTree().Paused = IsPaused;
    }

    public void ExitGame()
    {
        GetTree().Quit();
    }
}