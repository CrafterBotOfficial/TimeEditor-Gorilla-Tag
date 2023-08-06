namespace TimeEditor
{
    internal class TimeManager
    {
        internal static TimeManager Instance;

        internal ETimePreset CurrentTime = ETimePreset.Morning; // The current time that was set
        private int _lastTime = -1; // The time before the time was frozen
        internal bool TimeFrozen;

        internal void SetTime(ETimePreset newTime)
        {
            if (_lastTime == -1)
            {
                int currentTime = BetterDayNightManager.instance.currentTimeIndex;
                Main.Log("Saving current time as " + currentTime);
                _lastTime = currentTime;
            }
            CurrentTime = newTime;
            TimeFrozen = true;
        }

        internal void UnfreezeTime()
        {
            if (_lastTime != -1)
            {
                Main.Log("Unfreezing time");
                BetterDayNightManager.instance.currentTimeIndex = _lastTime;
                _lastTime = -1;
            }
            TimeFrozen = false;
        }
    }
}
