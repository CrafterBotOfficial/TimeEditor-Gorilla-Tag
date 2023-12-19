using HarmonyLib;

namespace TimeEditor;

public static class Patches
{
    [HarmonyPatch(typeof(BetterDayNightManager), nameof(BetterDayNightManager.SetTimeOfDay)), HarmonyPrefix]
    private static bool BetterDayNightManager_SetTimeOfDay_Prefix()
    {
        return !TimeManager.OverrideTime;
    }
}