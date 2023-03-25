using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace TimeEditor.Managers
{
    internal class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;

        private Traverse traverse;

        public bool RoomValid;
        public bool Enabled;

        public TimeManager()
        {
            Instance = this;
            traverse = Traverse.Create(BetterDayNightManager.instance);
        }
        public void Reset()
        { 
            // this.ChangeMaps(this.currentTimeIndex, (this.currentTimeIndex + 1) % this.dayNightLightmaps.Length);
            BetterDayNightManager BT = BetterDayNightManager.instance;
            
            BT.overrideIndex = -1;
            traverse.Field("lastIndex").SetValue(-1);

            int TargetIndex = TimePatch.OldTimeOfDayIndex;

            float TargetLerp = BT.currentLerp;

            BetterDayNightManager.instance.currentTimeIndex = TargetIndex;
            AccessExtensions.call(BetterDayNightManager.instance, "ChangeLerps", new object[] { TargetLerp });
            //AccessExtensions.call(BetterDayNightManager.instance, "ChangeMaps", new object[] { NewTimeIndexSwitch });
        }
    }

    [HarmonyPatch(typeof(BetterDayNightManager))]
    [HarmonyPatch("UpdateTimeOfDay", MethodType.Enumerator)]
    public class TimePatch
    {
        //public static Traverse traverse;

        public static string TimeOfDay = "Dawn";
        public static int OldTimeOfDayIndex;

        // override
        public static int IndexOverride;
        public static bool UseOverride;

        private static bool _Enabled;
        public static void Postfix()
        {
            if (!TimeManager.Instance.Enabled && _Enabled)
            {
                _Enabled = false;
                TimeManager.Instance.Reset();
            }
            else if (TimeManager.Instance.Enabled && !_Enabled)
            {
                _Enabled = true;
                OldTimeOfDayIndex = BetterDayNightManager.instance.currentTimeIndex;
            }

            if (!TimeManager.Instance.Enabled || !TimeManager.Instance.RoomValid || TimePresets[TimeOfDay] == null) return;

            if (UseOverride)
            {
                AccessExtensions.call(BetterDayNightManager.instance, "ChangeLerps", new object[] { 3600 % (IndexOverride <= 0 ? 1 : IndexOverride) });
                BetterDayNightManager.instance.overrideIndex = IndexOverride;
            }
            else
            {
                int TimeIndex = TimePresets[TimeOfDay];
                AccessExtensions.call(BetterDayNightManager.instance, "ChangeLerps", new object[] { 3600 % (TimeIndex == 0 ? 1 : TimeIndex) });
                BetterDayNightManager.instance.overrideIndex = TimeIndex;
            }
        }

        public static Dictionary<string, int> TimePresets = new Dictionary<string, int>()
        {
            { "Dawn", 6 },
            { "Day", 4 },
            { "Dusk", 2 },
            { "Night", 0 },
        };
    }
    public static class AccessExtensions // https://stackoverflow.com/questions/135443/how-do-i-use-reflection-to-invoke-a-private-method
    {
        public static object call(this object o, string methodName, params object[] args)
        {
            try
            {
                var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (mi != null)
                {
                    return mi.Invoke(o, args);
                }
                return null;
            }
            catch { return null; }
        }
    }
}
