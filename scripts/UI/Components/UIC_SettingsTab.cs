using Godot;

public partial class UIC_SettingsTab : MarginContainer
{
    [Export]
    public VBoxContainer SettingsContainer;

    public Control CreateSetting(SettingDatas setting)
    {
        var panelContainer = new PanelContainer()
        {
            TooltipText = setting.Description,
            MouseFilter = MouseFilterEnum.Pass,
            CustomMinimumSize = new Vector2(0, 40),
        };

        var hBoxContainer = new HBoxContainer()
        {
            TooltipText = setting.Description,
            MouseFilter = MouseFilterEnum.Pass,
        };

        hBoxContainer.AddChild(new Label()
        {
            Text = setting.Name,
            MouseFilter = MouseFilterEnum.Pass,
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
        });

        Control settingNode = null;

        if (setting.attribute is SettingSlider)
            settingNode = CreateSliderSetting(setting);

        else if (setting.Value is float)
            settingNode = CreateFloatSetting(setting);

        else if (setting.Value is bool)
            settingNode = CreateBoolSetting(setting);

        if (settingNode != null)
            hBoxContainer.AddChild(settingNode);

        panelContainer.AddChild(hBoxContainer);

        return panelContainer;
    }

    private static UIC_Slider CreateSliderSetting(SettingDatas setting)
    {
        var sliderAttr = setting.attribute as SettingSlider;
        var slider = UIManager.Instance.LoadUiComponent<UIC_Slider>();

        slider.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        slider.minValue = sliderAttr.min;
        slider.maxValue = sliderAttr.max;
        slider.Step = sliderAttr.step;
        slider.value = (float)setting.Value;
        slider.ValueChanged += setting.SetValue;

        return slider;
    }

    private static UIC_NumberEdit CreateFloatSetting(SettingDatas setting)
    {
        var numberEdit = UIManager.Instance.LoadUiComponent<UIC_NumberEdit>();

        numberEdit.SizeFlagsHorizontal = SizeFlags.ExpandFill;
        numberEdit.SetValue((float)setting.Value);
        numberEdit.ValueChanged += setting.SetValue;

        return numberEdit;
    }

    private static CheckButton CreateBoolSetting(SettingDatas setting)
    {
        var button = new CheckButton();

        button.AddThemeStyleboxOverride("focus", new StyleBoxEmpty());
        button.ButtonPressed = (bool)setting.Value;
        button.SizeFlagsHorizontal = SizeFlags.Expand;
        button.Toggled += setting.SetValue;

        return button;
    }
}
