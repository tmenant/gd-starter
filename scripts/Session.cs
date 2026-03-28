using System.Collections.Generic;

public static class Session
{
    public static int SettingsTabIndex { get; set; } = 0;

    public static List<string> CommandsHistory = new();
}