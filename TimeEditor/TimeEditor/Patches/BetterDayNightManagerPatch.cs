using HarmonyLib;

namespace TimeEditor.Patches
{
    [HarmonyPatch(typeof(BetterDayNightManager))]
    internal class BetterDayNightManagerPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("ChangeLerps")]
        private static void HookChangeLerps(BetterDayNightManager __instance, float newLerp)
        {
            ("Lerp attempting to change to : " + newLerp).Log();
            if (TimeController.OverrideTime && Main.RoomValid)
            {
                __instance.SetOverrideIndex(TimeController.CurrentTimeOverride);
                return;
            }
        }
    }
}
