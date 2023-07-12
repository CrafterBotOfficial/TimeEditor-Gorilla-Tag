namespace TimeEditor
{
    internal static class TimeManager
    {
        internal static int Current;

        private static bool _isTimeFrozen;
        private static int _lastTimeIndex;
        private static int _lastWeatherIndex;

        /* Edit time controls */

        internal static void SetTime(ETimePreset timePreset)
        {
            SetTime((int)timePreset);
        }

        internal static void SetTime(int index)
        {
            if (!Main.InRoom)
            {
                Main.Log("You must be in a modded room to change the time.", BepInEx.Logging.LogLevel.Warning);
                return;
            }
            Main.Log("Changing time...");
            Current = index;
            var manager = BetterDayNightManager.instance;
            if (!_isTimeFrozen)
            {
                Main.Log("Saving current time data...");
                _lastWeatherIndex = manager.currentWeatherIndex;
                _lastTimeIndex = manager.currentTimeIndex;
            }
            _isTimeFrozen = true;

            BetterDayNightManager.instance.SetOverrideIndex(Current);
        }

        internal static void Reset()
        {
            Main.Log("Resetting time...");
            if (_isTimeFrozen)
            {
                _isTimeFrozen = false;
                var manager = BetterDayNightManager.instance;
                manager.overrideIndex = -1;
                manager.currentTimeIndex = _lastTimeIndex;
                manager.currentWeatherIndex = _lastWeatherIndex;
            }
        }
    }
}
