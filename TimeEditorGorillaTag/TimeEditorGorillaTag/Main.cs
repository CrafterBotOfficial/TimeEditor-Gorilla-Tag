using BepInEx;
using Bepinject;
using HarmonyLib;
using System;
using System.ComponentModel;
using System.Reflection;
using TimeEditorGorillaTag.Computer;
using TimeEditorGorillaTag.Computer.FileHandling;
using UnityEngine;
using Utilla;

namespace TimeEditorGorillaTag
{
    [BepInPlugin(ModInfo.ModGUILD, ModInfo.ModName, ModInfo.ModVersion)]
    [BepInDependency(ModInfo.UtillaGUILD)]
    [BepInDependency(ModInfo.ComputerInterfaceGUILD)]

    [ModdedGamemode]
    [Description("This is a simple mod that adds the ability to edit the time of day in gorillatag | HauntedModMenu")]
    public class Main : BaseUnityPlugin
    {
        public static bool ModAllowed;

        // Starting events

        private void Awake()
        {
            SettingsManager.Load();

            Zenjector.Install<MainInstaller>().OnProject();
            DontDestroyOnLoad(new GameObject().AddComponent<TimeManager>());

            var harmony = new Harmony(ModInfo.ModGUILD);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        // Mod enabled

        [ModdedGamemodeJoin]
        private void OnJoin() => ModAllowed = true;
        [ModdedGamemodeLeave]
        private void OnLeave()
        {
            ModAllowed = false;
            TimeManager.Reset();
        }
    }

    internal class ModInfo
    {
        public const string ModGUILD = "crafterbot.editor.time";
        public const string ModName = "Time Editor";
        public const string ModVersion = "0.0.4";

        public const string UtillaGUILD = "org.legoandmars.gorillatag.utilla";
        public const string ComputerInterfaceGUILD = "tonimacaroni.computerinterface";
    }
}
