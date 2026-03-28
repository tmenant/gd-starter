using Godot;


public partial class UI_PauseMenu : UI_BaseWindow
{
    private static readonly Logging.Logger logger = Logging.CreateLogger<UI_PauseMenu>();

    public override bool Modal => true;

    public override void _Ready()
    {
        GameManager.Instance.TogglePause();
    }

    public override void _UnhandledInput(InputEvent _event)
    {
        if (_event.IsActionPressed(InputMaps.pause))
        {
            GameManager.Instance.TogglePause();
            GetViewport().SetInputAsHandled();
            QueueFree();
        }
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }

    private void OnButtonOptionsPressed()
    {
        var optionMenu = UIManager.Instance.LoadWindow<UI_OptionsMenu>();
        optionMenu.OnClose += OnOptionMenuClosed;
        Visible = false;
    }

    private void OnOptionMenuClosed(UI_BaseWindow optionMenu)
    {
        optionMenu.OnClose -= OnOptionMenuClosed;
        Visible = true;
    }

    private void OnButtonContinuePressed()
    {
        GameManager.Instance.TogglePause();
        QueueFree();
    }

    private void OnButtonExitPressed()
    {
        GameManager.Instance.ExitGame();
    }

}
