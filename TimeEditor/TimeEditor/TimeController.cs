namespace TimeEditor
{
    internal static class TimeController
    {
        internal static bool OverrideTime;
        internal static int CurrentTimeOverride;
        internal static void UpdateCustomTime()
        {
            if (OverrideTime)
            {
                "Changing time...".Log(BepInEx.Logging.LogLevel.Message);
                BetterDayNightManager.instance.SetOverrideIndex(CurrentTimeOverride);
                BetterDayNightManager manager = BetterDayNightManager.instance;
                manager.overrideIndex = -1;
                manager.timeMultiplier = 1;
            }
        }

        internal static void ResetTime()
        {
            OverrideTime = false;
            BetterDayNightManager.instance.SetOverrideIndex(BetterDayNightManager.instance.currentTimeIndex);
        }
    }

    internal enum TimeOfDay
    {
        Morning = 1,
        Day = 4,
        Evening = 6,
        Night = 8
    }
}
