using System;
using System.Collections.Generic;
using System.IO;
using Godot;

public partial class UIManager : CanvasLayer
{
    private static readonly Logging.Logger logger = Logging.CreateLogger<UIManager>();

    private readonly HashSet<UI_BaseWindow> activeWindows = new();

    private readonly HashSet<UI_BaseWindow> modalWindows = new();

    public static UIManager Instance { get; private set; }

    public static bool UIOpen => Instance.modalWindows.Count > 0;

    public override void _Ready()
    {
        if (Instance != null)
        {
            throw new Exception();
        }

        Instance = this;
    }

    public override void _UnhandledInput(InputEvent _event)
    {
        if (UIOpen) return;

        if (_event.IsActionPressed(InputMaps.pause))
            LoadWindow<UI_PauseMenu>();

        if (_event.IsActionPressed(InputMaps.cmdOpen))
            LoadWindow<UI_Console>();

        if (_event.IsActionPressed(InputMaps.ShowDebugHUD))
            UI_DebugHUD.Instance.ToggleVisibility();
    }

    public override void _Process(double delta)
    {
        UI_DebugHUD.Instance.SetText("activeWindows", activeWindows.Count);
        UI_DebugHUD.Instance.SetText("modalWindows", modalWindows.Count);
    }

    public Texture2D LoadUiIcon(string iconName)
    {
        var gdPath = $"{Constants.uiIconsDirectory}/{iconName}";

        if (GDUtils.FileExists(gdPath))
        {
            return ResourceLoader.Load(gdPath) as Texture2D;
        }

        logger.Warning($"icon not found: '{gdPath}'");

        return null;
    }

    public T LoadUiComponent<T>() where T : Control
    {
        string path = $"{Constants.uiComponentsDirectory}/{typeof(T).Name}.tscn";

        if (GD.Load<PackedScene>(path) is not PackedScene scene)
        {
            throw new FileNotFoundException(path);
        }

        return scene.Instantiate<T>();
    }

    public T LoadWindow<T>() where T : UI_BaseWindow
    {
        string path = $"{Constants.uiDirectory}/{typeof(T).Name}.tscn";

        if (GD.Load<PackedScene>(path) is not PackedScene scene)
        {
            throw new FileNotFoundException(path);
        }

        T window = scene.Instantiate<T>();

        window.OnClose += OnWindowClosed;
        activeWindows.Add(window);
        AddChild(window);

        if (window.Modal)
        {
            modalWindows.Add(window);
            GDUtils.SetMouseMode(Input.MouseModeEnum.Visible);
        }

        logger.Debug($"load window: '{typeof(T).Name}'");

        return window;
    }

    public T TryGetWindow<T>() where T : UI_BaseWindow
    {
        foreach (var window in activeWindows)
        {
            if (window is T) return window as T;
        }

        return null;
    }

    public bool IsOpened<T>() where T : UI_BaseWindow
    {
        foreach (var window in activeWindows)
        {
            if (window is T) return true;
        }

        return false;
    }

    private void OnWindowClosed(UI_BaseWindow window)
    {
        logger.Debug($"Closing window '{window.GetType().Name}'");

        activeWindows.Remove(window);
        window.OnClose -= OnWindowClosed;

        if (window.Modal)
            modalWindows.Remove(window);

        if (modalWindows.Count == 0)
            GDUtils.SetMouseMode(Input.MouseModeEnum.Captured);
    }
}
