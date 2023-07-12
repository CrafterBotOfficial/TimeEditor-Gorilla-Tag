using System.Collections.Generic;

namespace TimeEditor
{
    internal static class TimeManager
    {
        internal static int Current;

        private static int _currentIndex;
        private static bool _isTimeFrozen;

        private static bool _setLerpsOneShot; // This will allow the BetterDayNightManager to set the lerps map once, after that it will no longer be able to update itself
        private static bool _setMapsOneShot;
        private static int _lastWeatherIndex;

        /* Edit time controls */

        internal static void SetTime(ETimePreset timePreset)
        {
            SetTime(TimePresets[timePreset]);
        }

        internal static void SetTime(int index)
        {
            Main.Log("Changing time...");
            Current = index;
            var manager = BetterDayNightManager.instance;
            if (!_isTimeFrozen)
            {
                Main.Log("Saving current time data...");
                _lastWeatherIndex = manager.currentWeatherIndex;
            }
            _isTimeFrozen = true;

            // Not going to use the SetOverrideTime method because it causes issues with saving the old data.
            manager.currentTimeIndex = index;
            manager.currentWeatherIndex = 0;
            _setMapsOneShot = true;

        }

        internal static void Reset()
        {
            Main.Log("Resetting time...");
            if (_isTimeFrozen)
            {
                _isTimeFrozen = false;
            }
        }

        /// <summary>
        /// Refreshes the current time index, and returns whether or not the time is frozen.
        /// </summary>
        internal static bool IsTimeFrozen(bool forMap)
        {
            if (!_isTimeFrozen)
            {
                return false;
            }
            if (forMap && _setMapsOneShot)
            {
                _setMapsOneShot = false;
                return false;
            }
            if (!forMap && _setLerpsOneShot)
            {
                _setLerpsOneShot = false;
                return false;
            }

            var manager = BetterDayNightManager.instance;
            manager.currentTimeIndex = _currentIndex;
            manager.currentWeatherIndex = _lastWeatherIndex;
            return true;
        }

        internal readonly static Dictionary<ETimePreset, int> TimePresets = new Dictionary<ETimePreset, int>()
        {
            { ETimePreset.Morning, 1 },
            { ETimePreset.Noon, 4 },
            { ETimePreset.Evening, 6 },
            { ETimePreset.Night, 8 }
        };
    }
}
