using Godot;

public partial class UI_Console : UI_BaseWindow
{
    private RichTextLabel console;

    private int historyIndex;

    public override void _Ready()
    {
        console = GDUtils.FindChild<RichTextLabel>(this);
    }

    public override void _Process(double delta)
    {
        if (ConsoleManager.LogsHistory.Count < historyIndex)
        {
            historyIndex = 0;
            console.Text = "";
        }

        for (int i = historyIndex; i < ConsoleManager.LogsHistory.Count; i++)
        {
            var message = ConsoleManager.LogsHistory[i];

            console.Text += $"{message}\n";
            historyIndex++;
        }
    }

    public void OnFocusExited()
    {
        QueueFree();
    }
}
