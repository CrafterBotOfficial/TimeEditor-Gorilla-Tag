using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using BepInEx.Configuration;
using Utilla;
using TMPLoader;
using TMPro;

namespace TimeEditorGorillaTag.UI
{
    internal class SelectedMode : MonoBehaviour
    {
        public static SelectedMode Instance { get; private set; }

        public int Mode;

        private TextMeshPro ModeText;
        private void Awake()
        {
            Instance = this;
            Mode = (ModeInfo.Modes.Count-1) / 2;
            ModeText = UIConstructor.Instance.MenuTransform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        }

        public void ChangeMode(int Amount)
        {
            int NewMode = Mode + Amount;

            if (NewMode >= ModeInfo.Modes.Count || NewMode < 0) return;

            Mode = NewMode;

            try
            {
                ModeText.text = ModeInfo.Modes.ElementAt(Mode).Key;

                string SpriteName = ModeInfo.Modes.ElementAt(Mode).Value;
                var BackgroundImage = LoadSprite(SpriteName);
                UIConstructor.Instance.MenuTransform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().sprite = BackgroundImage;
            }
            catch
            {
                // Do nothing
            }
            GorillaTagger.Instance.StartVibration(true, 25, 0.2f);
        }

        private Sprite LoadSprite(string SpriteName)
        {
            var Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ModInfo.InternalAssetBundle);
            AssetBundle AssetBundle = AssetBundle.LoadFromStream(Stream);
            var Sprite = AssetBundle.LoadAsset<Sprite>(SpriteName);

            AssetBundle.Unload(false);
            return Sprite;
        }
    }

    internal class ModeInfo
    {
        public static Dictionary<string, string> Modes = new Dictionary<string, string>()
        {
            { "Morning", "Morning" },
            { "Day", "DayEnabled" },
            { "None", "Deactive" },
            { "Evening", "Evening" },
            { "Night", "NightEnabled" },
        };

        // Configuration entrys

        public static ConfigEntry<double> Morning;
        public static ConfigEntry<double> Day;
        public static ConfigEntry<double> Evening;
        public static ConfigEntry<double> Night;
    }
}
