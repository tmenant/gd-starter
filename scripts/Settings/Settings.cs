using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Godot;

public class Settings
{
    private const string configPath = "res://config/settings.json";

    public class ControlsSettings
    {
        [Setting]
        public float MaxPitch { get; set; } = 75;

        [Setting]
        public float MouseSensitivity { get; set; } = 0.005f;

        [SettingSlider(1, 100, step: 5)]
        public float MoveSpeed { get; set; } = 5f;

        [Setting]
        public float JumpSpeed { get; set; } = 4.5f;

        [Setting]
        public float SprintMulitplier { get; set; } = 2.5f;
    }

    public class PhysicsSettings
    {
        [Setting]
        public float Gravity { get; set; } = 9.81f;
    }

    [SettingsGroup]
    public ControlsSettings Controls { get; set; } = new();

    [SettingsGroup]
    public PhysicsSettings Physics { get; set; } = new();

    public static Settings Load()
    {
        return Load(ProjectSettings.GlobalizePath(configPath));
    }

    public static Settings Load(string path)
    {
        using (var reader = new StreamReader(path))
        {
            return JsonSerializer.Deserialize<Settings>(reader.ReadToEnd());
        }
    }

    public void Save()
    {
        var absolutePath = ProjectSettings.GlobalizePath(configPath);

        using (var writer = new StreamWriter(absolutePath))
        {
            writer.Write(JsonSerializer.Serialize(this));
        }
    }

    public IEnumerable<SettingsGroupDatas> GetSettingsGroupDatas()
    {
        var type = typeof(Settings);

        foreach (PropertyInfo propInfos in type.GetProperties())
        {
            var instance = propInfos.GetValue(this);

            foreach (Attribute attr in propInfos.GetCustomAttributes())
            {
                if (attr is SettingsGroup settingsGroupAttr)
                {
                    yield return new SettingsGroupDatas(propInfos, settingsGroupAttr, instance);
                }
            }
        }
    }

}
