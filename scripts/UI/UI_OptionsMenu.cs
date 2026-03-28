using Godot;


public partial class UI_OptionsMenu : UI_BaseWindow
{
    public override bool Modal => true;

    private TabContainer tabContainer;

    public override void _Ready()
    {
        tabContainer = GDUtils.FindChild<TabContainer>(this);

        foreach (var group in GameManager.settings.GetSettingsGroupDatas())
        {
            tabContainer.AddChild(CreateTabBar(group));
        }

        tabContainer.CurrentTab = Session.SettingsTabIndex;
    }

    private TabBar CreateTabBar(SettingsGroupDatas groupDatas)
    {
        var tab = new TabBar() { Name = groupDatas.Name };

        var settingsTab = UIManager.Instance.LoadUiComponent<UIC_SettingsTab>();
        settingsTab.AnchorLeft = 0;
        settingsTab.AnchorRight = 0.5f;
        settingsTab.SettingsContainer.AddChild(new Label()
        {
            Text = groupDatas.Description,
            SizeFlagsHorizontal = Control.SizeFlags.ExpandFill,
        });

        foreach (var setting in groupDatas.GetSettingDatas())
        {
            var settingNode = settingsTab.CreateSetting(setting);
            settingsTab.SettingsContainer.AddChild(settingNode);
        }

        tab.AddChild(settingsTab);

        return tab;
    }

    public void OnButtonBackPressed()
    {
        QueueFree();
    }

    public void OnTabClicked(int tabIndex)
    {
        Session.SettingsTabIndex = tabIndex;
    }
}
