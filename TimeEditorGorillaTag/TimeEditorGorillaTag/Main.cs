/*
    I made this mod in the span of 2 days so do not expect it to be very organized.
*/


using BepInEx;
using BepInEx.Configuration;
using System.ComponentModel;
using TimeEditorGorillaTag.UI;
using UnityEngine;
using Utilla;
using System.Reflection;

namespace TimeEditorGorillaTag
{
    [BepInPlugin(ModInfo.ModGUILD, ModInfo.ModName, ModInfo.ModVersion)]
    [BepInDependency(ModInfo.UtillaGUILD)]
    [BepInDependency(ModInfo.TMProGUILD)]
    [ModdedGamemode]
    [Description("HauntedModMenu")]
    public class Main : BaseUnityPlugin
    {
        public static Main Instance { get; private set; }

        public bool ModAllowed;
        private GameObject FingerTrigger;

        public void Awake()
        {
            Instance = this;
            Utilla.Events.GameInitialized += Events_GameInitialized;

            // BepInEx config

            ModeInfo.Morning = Config.Bind<double>("Time Presets", "Morning time set", 5);
            ModeInfo.Evening = Config.Bind<double>("Time Presets", "Evening time set", 25);
            ModeInfo.Night = Config.Bind<double>("Time Presets", "Night time set", 360);

        }

        private void Events_GameInitialized(object sender, System.EventArgs e)
        {
            DontDestroyOnLoad(new GameObject().AddComponent<UI.UIConstructor>());
            DontDestroyOnLoad(new GameObject().AddComponent<UI.SelectedMode>());
            DontDestroyOnLoad(new GameObject().AddComponent<UI.Input>());
            DontDestroyOnLoad(new GameObject().AddComponent<TimeManager>());

            // Menu interactable trigger

            FingerTrigger = UI.UIConstructor.Instance.LoadAsset("FingerTrigger"); // Loads from asset loader
            FingerTrigger.name = "FingerTrigger";
            Transform FingerTriggerTransform = FingerTrigger.transform;
            FingerTriggerTransform.parent = GorillaLocomotion.Player.Instance.leftHandTransform;
            FingerTriggerTransform.localPosition = new Vector3(-0.01f, 0.0f, 0.1f);

            FingerTrigger.AddComponent<MeshRenderer>().material.color = Color.white;

            Main.Instance.CloseMenu();
        }

        // Menu open/close

        public void OpenMenu()
        {
            if (!ModAllowed) return;
            FingerTrigger.SetActive(true);
            UI.UIConstructor.Instance.MenuTransform.gameObject.SetActive(true);
        }

        public void CloseMenu()
        {
            if (!FingerTrigger || !UIConstructor.Instance) return; 
            FingerTrigger.SetActive(false);
            UI.UIConstructor.Instance.MenuTransform.gameObject.SetActive(false);
        }

        // Mod allowed/enabled

        public bool _modEnabled;
        public bool _roomModded;

        public void OnEnable() => _modEnabled = true;
        public void OnDisable()
        {
            _modEnabled = false;
            CloseMenu();
        }
        [ModdedGamemodeJoin]
        private void OnModdedGamemodeJoin() => _roomModded = true;
        [ModdedGamemodeLeave]
        private void OnModdedGamemodeLeave()
        {
            _roomModded = false;
            CloseMenu();
            TimeManager.Instance.Reset();
        }
    }

    internal class ModInfo
    {
        public const string ModGUILD = "crafterbot.timeeditor.gorillatag.menu";
        public const string ModName = "Time Editor";
        public const string ModVersion = "0.0.3";

        public const string UtillaGUILD = "org.legoandmars.gorillatag.utilla";
        public const string TMProGUILD = "com.ahauntedarmy.gorillatag.tmploader";

        public const string InternalAssetBundle = "TimeEditorGorillaTag.AssetBundle.timeeditor";
    }
}
