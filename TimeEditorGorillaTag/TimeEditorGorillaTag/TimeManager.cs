using HarmonyLib;
using System;
using System.Collections.Generic;
using TimeEditorGorillaTag.Computer.FileHandling;
using UnityEngine;

namespace TimeEditorGorillaTag
{
    internal class TimeManager : MonoBehaviour
    {
        public static void Reset()
        {
            
        }
    }

    [HarmonyPatch(typeof(BetterDayNightManager))]
    [HarmonyPatch("UpdateTimeOfDay", MethodType.Enumerator)]
    internal class TimePatch
    {
        public static void Postfix(BetterDayNightManager __instance)
        {
            Debug.Log(__instance.timeOfDayRange);
            if (SettingsManager.BuilderSettings.ModEnabled)
            {
                __instance.timeOfDayRange = SettingsManager.BuilderSettings.Preset;
            }
        }

        private static List<string> Times = new List<string>()
        {
            "Morning",
            "Day",
            "Evening",
            "Night"
        }; //-1675646784
    }
}