using HarmonyLib;

namespace TimeEditor
{
    internal static class Patches
    {
        [HarmonyPatch(typeof(BetterDayNightManager), "ChangeLerps"), HarmonyPrefix]
        private static void BetterDayNightManager_ChangeLerps_Prefix(float newLerp)
        {
            if (TimeManager.Instance.TimeFrozen)
            {
                newLerp = (float)BetterDayNightManager.instance.timeOfDayRange[TimeManager.Instance.CurrentTime as int] * 3600.0;
            }
        }

        [HarmonyPatch(typeof(BetterDayNightManager), "ChangeMaps"), HarmonyPrefix]
        private static bool BetterDayNightManager_ChangeMaps_Prefix()
        {
            return !TimeManager.Instance.TimeFrozen;
        }
    }
}
