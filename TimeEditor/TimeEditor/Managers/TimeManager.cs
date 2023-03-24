using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using Utilla;

namespace TimeEditor.Managers
{
    [ModdedGamemode]
    internal class TimeManager : MonoBehaviour
    {
        public static TimeManager Instance;

        public bool RoomValid;
        public bool Enabled;

        public float Lerp;

        public TimeManager()
        {
            Instance = this;
            /*Remove*/
            Enabled = true;
        }
        public void Reset()
        {

        }
        //
        //
        // Set room info
        //
        //
        [ModdedGamemodeJoin]
        private void Joined() => RoomValid = true;
        [ModdedGamemodeLeave]
        private void Left() => RoomValid = false;
    }

    [HarmonyPatch(typeof(BetterDayNightManager))]
    [HarmonyPatch("UpdateTimeOfDay", MethodType.Enumerator)]
    public class TimePatch
    {
        public static string TimeOfDay;

        private static bool _Enabled;
        public static void Postfix()
        { // 5 index
            if (!TimeManager.Instance.Enabled && _Enabled)
            {
                _Enabled = false;
                TimeManager.Instance.Reset();
            }
            //if (!TimeManager.Instance.Enabled) return;
            _Enabled = true;

            int TimeIndex = TimePresets[TimeOfDay];
            AccessExtensions.call(BetterDayNightManager.instance, "ChangeLerps", new object[] { 3600 % (TimeIndex == 0 ? 1 : TimeIndex) });
            AccessExtensions.call(BetterDayNightManager.instance, "ChangeMaps", new object[] { (TimeIndex - 1), TimeIndex });
        }

        public static Dictionary<string, int> TimePresets = new Dictionary<string, int>()
        {
            { "Dawn", 3 }, // 3
            { "Day", 4 }, // 4
            { "Dusk", 2 },
            { "Night", 1 },
            { "Midnight", 0 },
        };
    }
    public static class AccessExtensions // https://stackoverflow.com/questions/135443/how-do-i-use-reflection-to-invoke-a-private-method
    {
        public static object call(this object o, string methodName, params object[] args)
        {
            var mi = o.GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (mi != null)
            {
                return mi.Invoke(o, args);
            }
            return null;
        }
    }
}
