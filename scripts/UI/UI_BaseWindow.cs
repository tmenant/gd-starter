using System;
using Godot;

public abstract partial class UI_BaseWindow : Control
{
    public event Action<UI_BaseWindow> OnClose;

    public virtual bool Modal => true;

    public override void _UnhandledInput(InputEvent _event)
    {
        if (Modal && _event.IsActionPressed(InputMaps.pause))
        {
            GetViewport().SetInputAsHandled();
            QueueFree();
        }
    }

    public override void _ExitTree()
    {
        OnClose.Invoke(this);
    }
}