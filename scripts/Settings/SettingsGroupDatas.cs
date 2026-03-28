using System.Collections.Generic;
using System.Reflection;


public class SettingsGroupDatas
{
    private readonly PropertyInfo propertyInfo;

    private readonly object instance;

    private readonly SettingsGroup groupSettingsAttr;

    public string Identifier => propertyInfo.PropertyType.Name;

    public string Name => Localization.Get(Identifier);

    public string Description => Localization.Get($"{Identifier}.desc");

    public SettingsGroupDatas(PropertyInfo propertyInfo, SettingsGroup groupSettingsAttr, object instance)
    {
        this.propertyInfo = propertyInfo;
        this.groupSettingsAttr = groupSettingsAttr;
        this.instance = instance;
    }

    public IEnumerable<SettingDatas> GetSettingDatas()
    {
        foreach (var prop in propertyInfo.PropertyType.GetProperties())
        {
            foreach (var attr in prop.GetCustomAttributes())
            {
                if (attr is Setting settingAttr)
                {
                    yield return new SettingDatas(propertyInfo, prop, settingAttr, instance);
                }
            }
        }
    }
}
