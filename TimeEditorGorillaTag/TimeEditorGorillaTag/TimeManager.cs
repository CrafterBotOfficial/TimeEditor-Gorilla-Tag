using TimeEditorGorillaTag.UI;
using UnityEngine;

namespace TimeEditorGorillaTag
{
    internal class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Default = BetterDayNightManager.instance.timeOfDayRange;
        }

        private double[] Default;
        private double[] Morning = { ModeInfo.Morning.Value };
        private double[] Day = { ModeInfo.Day.Value }; // I know this isn't the best way to do this, but I am not reading through all of the BetterDayNightManager code:(
        private double[] Evening = { ModeInfo.Evening.Value };
        private double[] Night = { ModeInfo.Night.Value };

        public void SetTime()
        {
            if (!Main.Instance.ModAllowed) return;

            int Mode = SelectedMode.Instance.Mode;
            switch (Mode)
            {
                case 0:
                    BetterDayNightManager.instance.timeOfDayRange = Morning;
                    break;
                case 1:
                    BetterDayNightManager.instance.timeOfDayRange = Day;
                    break;
                case 2:
                    Reset();
                    Debug.Log(BetterDayNightManager.instance.timeOfDayRange[BetterDayNightManager.instance.currentTimeIndex]);
                    break;
                case 3:
                    BetterDayNightManager.instance.timeOfDayRange = Evening;
                    break;
                    case 4:
                   BetterDayNightManager.instance.timeOfDayRange = Night;
                    break;
            }

            GorillaTagger.Instance.StartVibration(true, 50, 0.5f);
        }

        public void Reset()
        {
            BetterDayNightManager.instance.timeOfDayRange = Default;
        }
    }
}
