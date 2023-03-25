using BepInEx;
using Bepinject;
using HarmonyLib;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace TimeEditor
{
    [BepInPlugin(ModInfo.ModGUILD, ModInfo.ModName, ModInfo.ModVersion)]
    [BepInDependency("tonimacaroni.computerinterface")]
    [BepInDependency("dev.auros.bepinex.bepinject")]

    [ModdedGamemode]
    public class Main : BaseUnityPlugin
    {
        private void Awake()
        {
            SetManagers();

            Harmony harmony = new Harmony(ModInfo.ModGUILD);
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Zenjector.Install<UI.MainInstaller>().OnProject();
        }
        private void SetManagers()
        {
            new GameObject("TimeEditor Managers")
                .AddComponent<Managers.BuildValid>()
            .gameObject
                .AddComponent<Managers.TimeManager>();
        }

        /// <summary>
        /// Modded Gamemode
        /// </summary>
        [ModdedGamemodeJoin]
        private void Joined() => Managers.TimeManager.Instance.RoomValid = true;
        [ModdedGamemodeLeave]
        private void Left()
        {
            Managers.TimeManager.Instance.RoomValid = false;
            Managers.TimeManager.Instance.Enabled = false;
        }
    }

    internal class ModInfo
    {
        public const string ModGUILD = "crafterbot.gorillatag.timeeditor";
        public const string ModName = "Time Editor";
        public const string ModVersion = "0.0.4";

        public const int BuildId = 1;
        public const string BuildType = "Debug";
    }
}
