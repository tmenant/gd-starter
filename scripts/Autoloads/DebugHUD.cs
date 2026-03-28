using Godot;
using System.Collections.Generic;

public partial class UI_DebugHUD : CanvasLayer
{
    private static readonly Color TEXT_COLOR = Colors.Green;

    private static readonly Color TEXT_BG_COLOR = new Color(0.3f, 0.3f, 0.3f, 0.8f);

    private const int TEXT_SIZE = 25;

    private readonly Dictionary<string, string> texts = new Dictionary<string, string>();

    private readonly Dictionary<string, Label> labels = new Dictionary<string, Label>();

    private Font _font = ResourceLoader.Load<Font>("res://assets/fonts/Hack-Regular.ttf");

    private LabelSettings labelSettings = new LabelSettings() { FontColor = TEXT_COLOR };

    private ColorRect backgroundRect = new ColorRect() { Color = TEXT_BG_COLOR };

    public static UI_DebugHUD Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
        ProcessMode = ProcessModeEnum.Always;
        Layer = 100;

        backgroundRect.Position = new Vector2(0, 0);
        backgroundRect.Size = new Vector2(350, 0);

        AddChild(backgroundRect);

        Visible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Visible)
            return;

        foreach (var key in labels.Keys)
        {
            if (!texts.ContainsKey(key))
            {
                RemoveLabel(key);
            }
        }

        foreach (var key in texts.Keys)
        {
            if (!labels.TryGetValue(key, out var label))
            {
                label = AddLabel(key);
            }

            label.Text = texts[key].ToString();
        }

        texts.Clear();
    }

    private void RemoveLabel(string key)
    {
        labels[key].QueueFree();
        labels.Remove(key);

        var backgroundSize = backgroundRect.Size;

        backgroundSize.Y -= TEXT_SIZE;
        backgroundRect.Size = backgroundSize;
    }

    private Label AddLabel(string key)
    {
        var label = new Label()
        {
            Position = new Vector2(5, labels.Count * TEXT_SIZE),
            LabelSettings = labelSettings,
        };

        labels[key] = label;

        AddChild(label);

        var backgroundSize = backgroundRect.Size;

        backgroundSize.Y += TEXT_SIZE;
        backgroundRect.Size = backgroundSize;

        return label;
    }

    public void SetText(string key, object value)
    {
        texts[key] = $"{key}: {value}";
    }

    public void ToggleVisibility()
    {
        Visible = !Visible;
    }

}
