using HarmonyLib;

namespace TimeEditor
{
    internal static class Patches
    {
        [HarmonyPatch(typeof(BetterDayNightManager), "ChangeMaps"), HarmonyPrefix]
        private static bool BetterDayNightManager_ChangeMaps_Prefix()
        {
            return !TimeManager.IsTimeFrozen();
        }
        [HarmonyPatch(typeof(BetterDayNightManager), "ChangeLerps"), HarmonyPrefix]
        private static bool BetterDayNightManager_ChangeLerps_Prefix()
        {
            return !TimeManager.IsTimeFrozen();
        }
    }
}
