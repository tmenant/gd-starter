using System;


[AttributeUsage(AttributeTargets.Property)]
public class Setting : Attribute { }


[AttributeUsage(AttributeTargets.Property)]
public class SettingSlider : Setting
{
    public readonly float min;

    public readonly float max;

    public readonly int step;

    public SettingSlider(float min, float max, int step = 1)
    {
        this.min = min;
        this.max = max;
        this.step = step;
    }
}