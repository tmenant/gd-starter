using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Godot;


public partial class UIC_NumberEdit : LineEdit
{
    private string pattern = "";

    [Export]
    public float value = 0;

    [Export]
    public bool floatingNumber = true;

    [Export]
    public int decimals = 3;

    [Export]
    public float minValue = float.MinValue;

    [Export]
    public float maxValue = float.MaxValue;

    [Signal]
    public delegate void ValueChangedEventHandler(float value);

    public override void _Ready()
    {
        Text = value.ToString(CultureInfo.InvariantCulture);
    }

    public void SetValue(float newValue)
    {
        value = newValue;
        Text = newValue.ToString(CultureInfo.InvariantCulture);
    }

    public void OnTextSubmitted(string text)
    {
        if (float.TryParse(text, CultureInfo.InvariantCulture, out value))
        {
            value = Math.Clamp(value, minValue, maxValue);
            value = MathF.Round(value, decimals);

            if (!floatingNumber)
            {
                value = MathF.Round(value);
            }

            EmitSignalValueChanged(value);
        }

        Text = value.ToString(CultureInfo.InvariantCulture);
    }

    private void OnTextChanged(string newText)
    {
        var strValue = Regex.Replace(newText, @"[^\d^\-^\.]", "");

        if (strValue != newText)
        {
            Text = strValue;
            CaretColumn = strValue.Length;
        }
    }

}
