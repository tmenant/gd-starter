using System.Collections.Generic;
using Godot;

public static class InputMaps
{
    public const string moveLeft = "move_left";

    public const string moveRight = "move_right";

    public const string moveForward = "move_forward";

    public const string moveBackward = "move_backward";

    public const string moveDown = "move_down";

    public const string pause = "pause";

    public const string cmdAutocomplete = "cmd_autocomplete";

    public const string cmdPrevious = "cmd_previous";

    public const string cmdNext = "cmd_next";

    public const string cmdOpen = "cmd_open";

    public const string ShowDebugHUD = "show_debug_hud";

    public static string GetPhysicalKeycode(string actionName)
    {
        var inputs = InputMap.ActionGetEvents(actionName);

        foreach (var inputEvent in inputs)
        {
            if (inputEvent is InputEventKey keyEvent)
            {
                return keyEvent.AsTextPhysicalKeycode();
            }
        }

        throw new KeyNotFoundException($"Unknown action: '{actionName}'");
    }
}