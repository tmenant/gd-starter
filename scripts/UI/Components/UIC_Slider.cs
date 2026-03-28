using Godot;

public partial class UIC_Slider : Control
{
    private UIC_NumberEdit numberEdit;

    private HSlider slider;

    private bool enableEvents = true;

    [Export] public double Step = 1;

    [Export] public float value = 0;

    [Export] public bool allowDecimals = true;

    [Export] public int decimals = 3;

    [Export] public float minValue = float.MinValue;

    [Export] public float maxValue = float.MaxValue;

    [Signal] public delegate void ValueChangedEventHandler(float value);

    public override void _EnterTree()
    {
        numberEdit = GDUtils.FindChild<UIC_NumberEdit>(this);
        numberEdit.minValue = minValue;
        numberEdit.maxValue = maxValue;
        numberEdit.floatingNumber = allowDecimals;
        numberEdit.decimals = decimals;
        numberEdit.SetValue(value);

        slider = GDUtils.FindChild<HSlider>(this);
        slider.Value = value;
        slider.MinValue = minValue;
        slider.MaxValue = maxValue;
        slider.Step = Step;
    }

    public void OnValueChanged(float newValue)
    {
        enableEvents = false;

        slider.Value = newValue;
        EmitSignalValueChanged(newValue);

        enableEvents = true;
    }

    public void OnSliderValueChanged(float newValue)
    {
        if (enableEvents)
        {
            numberEdit.SetValue(newValue);
            EmitSignalValueChanged(newValue);
        }
    }

}
