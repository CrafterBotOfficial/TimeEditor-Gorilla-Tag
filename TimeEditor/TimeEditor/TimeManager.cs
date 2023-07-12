namespace TimeEditor
{
    internal static class TimeManager
    {
        internal static int Current;

        // private static int _currentIndex;
        private static bool _isTimeFrozen;
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
                BetterDayNightManager.instance.SetOverrideIndex(-1);
                BetterDayNightManager.instance.currentWeatherIndex = _lastWeatherIndex;
            }
        }

        /*/// <summary>
        /// Refreshes the current time index, and returns whether or not the time is frozen.
        /// </summary>
        internal static bool IsTimeFrozen()
        {
            if (!_isTimeFrozen)
            {
                return false;
            }

            var manager = BetterDayNightManager.instance;
            manager.currentTimeIndex = Current;
            manager.currentWeatherIndex = 0;
            return true;
        }*/
    }
}
