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
        private int DefaultIndex = -1;
        private double[] Morning;
        private double[] Day; // I know this isn't the best way to do this, but I am not reading through all of the BetterDayNightManager code:(
        private double[] Evening;
        private double[] Night;

        public void SetTime()
        {
            if (!Main.Instance.ModAllowed) return;
            if (DefaultIndex == -1) DefaultIndex = BetterDayNightManager.instance.currentTimeIndex;

            var BT = BetterDayNightManager.instance;

            Morning = new double[] { ModeInfo.Morning.Value };
            Day = new double[] {
                0.89999999,
                1,
                1,
                0.89999999,
                0.3,
                0.649999,
                0.3,
            };
            Evening = new double[] { ModeInfo.Evening.Value };
            Night = new double[] { ModeInfo.Night.Value };

            int Mode = SelectedMode.Instance.Mode;
            switch (Mode)
            {
                case 0:
                    BT.timeOfDayRange = Morning;
                    break;
                case 1:
                    BT.currentTimeIndex = 4;
                    BT.timeOfDayRange = Day;
                    break;
                case 2:
                    Reset();
                    break;
                case 3:
                    BT.timeOfDayRange = Evening;
                    break;
                case 4:
                    BT.timeOfDayRange = Night;
                    break;
            }

            GorillaTagger.Instance.StartVibration(true, 50, 0.5f);
        }
        public void Reset()
        {
            BetterDayNightManager.instance.currentTimeIndex = DefaultIndex;
            DefaultIndex = -1;
            BetterDayNightManager.instance.timeOfDayRange = Default;
        }
    }
}

/*
0.65, 
0.3, 
0.89999999, 
1, 
1, 
0.89999999,
0.3, 
0.649999, 
0.3, 
1.5, 
 
*/