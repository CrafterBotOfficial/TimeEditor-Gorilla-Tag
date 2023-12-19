using BepInEx.Configuration;

namespace TimeEditor;

public static class Configuration
{
    private static ConfigFile Config;

    public static ConfigEntry<bool> AutoSetTime;
    public static ConfigEntry<int> SavedTimeIndex;

    public static void Init(ConfigFile configFile)
    {
        Config = configFile;

        AutoSetTime = Config.Bind("Automatically Set Time", "Enabled", false, "Determines whether or not the time will be automatically changed whenever you join a modded room.");
        SavedTimeIndex = Config.Bind("Automatically Set Time", "Time Index", -1, "This is the last time you set on the computer. Do not edit this!");
    }
}