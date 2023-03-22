using BepInEx;
using UnityEngine;

namespace TimeEditor
{
    [BepInPlugin(ModInfo.ModGUILD, ModInfo.ModName, ModInfo.ModVersion)]
    [BepInDependency("tonimacaroni.computerinterface")]
    public class Main : BaseUnityPlugin
    {
        private void Awake()
        {
            SetManagers();
        }

        private void SetManagers()
        {
            new GameObject("TimeEditor Managers")
                .AddComponent<Managers.BuildValid>()
            .gameObject
                .AddComponent<Managers.TimeManager>();
        }
    }

    internal class ModInfo
    {
        public const string ModGUILD = "crafterbot.gorillatag.timeeditor";
        public const string ModName = "Time Editor";
        public const string ModVersion = "0.0.4";

        public const int BuildId = 2;
        public const string BuildType = BuildTypes.Debug == BuildTypes.Debug ? "Debug" : "Release";
    }

    public enum BuildTypes
    {
        Release,
        Debug
    }
}
