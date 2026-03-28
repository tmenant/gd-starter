using System.Reflection;
using Godot;


public class SettingDatas
{
    public readonly PropertyInfo parentInfos;

    public readonly PropertyInfo propertyInfo;

    public readonly Setting attribute;

    public readonly object instance;

    public string Identifier => $"{parentInfos.PropertyType.Name}.{propertyInfo.Name}";

    public string Name => Localization.Get(Identifier);

    public string Description => Localization.Get($"{Identifier}.desc");

    public object Value
    {
        get => propertyInfo.GetValue(instance);
        set => propertyInfo.SetValue(instance, value);
    }

    public SettingDatas(PropertyInfo parentInfos, PropertyInfo propertyInfo, Setting settingAttr, object instance)
    {
        this.parentInfos = parentInfos;
        this.propertyInfo = propertyInfo;
        this.attribute = settingAttr;
        this.instance = instance;
    }

    public void SetValue<T>(T value)
    {
        Value = value;
    }
}

