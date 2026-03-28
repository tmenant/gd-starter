using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class UIC_CommandLineEdit : LineEdit
{
    private readonly List<string> autocompletion = new();

    private readonly List<string> autocompletionResults = new();

    private static List<string> CommandsHistory => Session.CommandsHistory;

    private int autocompletionIndex = -1;

    private int historyIndex = -1;

    public override void _Ready()
    {
        autocompletion.AddRange(ConsoleManager.Commands.Select(cmd => cmd.Commands.First()));
        Edit();
    }

    public override void _GuiInput(InputEvent _event)
    {
        if (_event.IsActionPressed(InputMaps.cmdAutocomplete))
            Autocomplete();

        else if (_event.IsActionPressed(InputMaps.cmdPrevious))
            PreviousCommand();

        else if (_event.IsActionPressed(InputMaps.cmdNext))
            NextCommand();

        else if (_event.IsActionPressed(InputMaps.pause) || _event.IsActionPressed(InputMaps.cmdOpen))
            QueueFree();

        else
            return;

        AcceptEvent();
    }

    private void Autocomplete()
    {
        if (autocompletionResults.Count == 0)
        {
            autocompletionResults.AddRange(autocompletion
                .Where(c => c.StartsWith(Text, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(c => c)
            );
        }

        if (autocompletionResults.Count == 0)
            return;

        autocompletionIndex = (autocompletionIndex + 1) % autocompletionResults.Count;
        Text = autocompletionResults[autocompletionIndex];
        CaretColumn = Text.Length;

        GetViewport().SetInputAsHandled();
    }

    private void PreviousCommand()
    {
        if (CommandsHistory.Count == 0)
            return;

        historyIndex = Mathf.Clamp(historyIndex + 1, 0, CommandsHistory.Count - 1);
        Text = CommandsHistory[CommandsHistory.Count - historyIndex - 1];
        CaretColumn = Text.Length;
    }

    private void NextCommand()
    {
        if (CommandsHistory.Count == 0)
            return;

        historyIndex = Mathf.Clamp(historyIndex - 1, -1, CommandsHistory.Count - 1);

        if (historyIndex == -1)
        {
            Text = "";
        }
        else
        {
            Text = CommandsHistory[CommandsHistory.Count - historyIndex - 1];
        }

        CaretColumn = Text.Length;
    }

    private void OnTextChanged(string text)
    {
        autocompletionIndex = -1;
        autocompletionResults.Clear();
        historyIndex = -1;
    }

    public void OnTextSubmitted(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            CommandsHistory.Add(text.Trim());
            ConsoleManager.ExecuteCommand(text.Trim());
        }

        Text = "";
        autocompletionResults.Clear();
        autocompletionIndex = -1;
        historyIndex = -1;

        CallDeferred("edit");
    }
}
