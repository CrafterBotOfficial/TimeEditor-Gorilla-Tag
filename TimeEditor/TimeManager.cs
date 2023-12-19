using HarmonyLib;

namespace TimeEditor;

public class TimeManager
{
    public static int CurrentIndex;
    public static bool OverrideTime;

    private static Harmony HarmonyInstance;

    public static void SetTime()
    {
        if (HarmonyInstance is null) HarmonyInstance = new(Main.Instance.Info.Metadata.GUID);
        var instance = BetterDayNightManager.instance;

        UnpatchSelf();
        if (CurrentIndex != -1)
        {
            instance.SetTimeOfDay(CurrentIndex);
            Configuration.SavedTimeIndex.Value = CurrentIndex;
            HarmonyInstance.PatchAll(typeof(Patches)); // Prevents the time from changing again
        }
        else
        {
            instance.currentSetting = TimeSettings.Normal;
        }
    }

    public static void UnpatchSelf()
    {
        HarmonyInstance.UnpatchSelf();
    }

    public static Dictionary<string, int> TimePresets = new()
    {
        { "Disable", -1 },

        { "Morning", 1 },
        { "Day", 4 },
        { "Evening", 6 },
        { "Night", 8 },
    };
}
