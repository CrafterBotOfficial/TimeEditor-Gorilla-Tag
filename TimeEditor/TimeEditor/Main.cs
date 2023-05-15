using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Net.Http;
using Utilla;

namespace TimeEditor
{
    [BepInPlugin(GUID, NAME, VERSION), BepInDependency("Crafterbot.MonkeStatistics"), BepInDependency("org.legoandmars.gorillatag.utilla")]
    [ModdedGamemode]
    internal class Main : BaseUnityPlugin
    {
        internal const string
            GUID = "crafterbot.gorillatag.timeeditor",
            NAME = "Time Editor",
            VERSION = "1.0.1",
            GITHUB_REPO_VERSION = "https://raw.githubusercontent.com/CrafterBotOfficial/TimeEditor-Gorilla-Tag/main/Version";
        internal static bool RoomValid;
        internal static bool VersionValid = true;
        internal static ManualLogSource manualLogSource;
        private void Awake()
        {
            manualLogSource = Logger;
            $"Init : {GUID}".Log();

            "Comparing online version to local version!".Log();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                    if (new Version(httpClient.GetStringAsync(GITHUB_REPO_VERSION).Result).Build > new Version(VERSION).Build)
                        VersionValid = false;
            }
            catch (Exception ex) { $"Error while checking for updates: {ex.Message}".Log(LogLevel.Error); }
            finally { "Compared online version to local".Log(LogLevel.Message); }

            new Harmony(GUID).PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
            MonkeStatistics.API.Registry.AddAssembly();
        }

        /* Debugging NONE-VR methods */

#if DEBUG
        private int time;
        private void OnGUI()
        {
            //return; // I am done with this. I will leave it in incase Gtag changes it's DayNightManager AGAIN
            time = int.Parse(GUILayout.TextField(time.ToString()));
            RoomValid = true;
            if (GUILayout.Button("Set"))
            {
                TimeController.CurrentTimeOverride = time;
                TimeController.OverrideTime = true;
                TimeController.UpdateCustomTime();
            }

            if (GUILayout.Button("Morning"))
                SetTime((int)TimeOfDay.Morning);
            if (GUILayout.Button("Day"))
                SetTime((int)TimeOfDay.Day);
            if (GUILayout.Button("Evening"))
                SetTime((int)TimeOfDay.Evening);
            if (GUILayout.Button("Night"))
                SetTime((int)TimeOfDay.Night);
            if (GUILayout.Button("Reset"))
                TimeController.ResetTime();
            GUILayout.Label("Version : " + VersionValid);

            void SetTime(int Time)
            {
                TimeController.CurrentTimeOverride = Time;
                TimeController.OverrideTime = true;
                TimeController.UpdateCustomTime();
            }

            if (GUILayout.Button("Log Data"))
            {
                BetterDayNightManager manager = BetterDayNightManager.instance;
                manager.currentTimeIndex.Log();
                manager.overrideIndex.Log();
                manager.timeMultiplier.Log();
                manager.timeOfDayRange.Log();
            }
        }
#endif

        /* Room event handler methods */

        [ModdedGamemodeJoin]
        private void OnJoined(string gamemode)
        {
            RoomValid = true;
            TimeController.UpdateCustomTime();
        }
        [ModdedGamemodeLeave]
        private void OnLeft()
        {
            RoomValid = false;
            TimeController.ResetTime();
        }
    }
}