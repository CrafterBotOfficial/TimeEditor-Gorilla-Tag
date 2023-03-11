using System.Reflection;
using TimeEditorGorillaTag.UI.Behaviours;
using UnityEngine;

namespace TimeEditorGorillaTag.UI
{
    internal class UIConstructor : MonoBehaviour
    {
        public static UIConstructor Instance { get; private set; }

        public Transform MenuTransform { get; private set; }

        private void Awake()
        {
            Instance = this;

            // Configure menu below

            GameObject MenuObj = LoadAsset("MenuObj");
            MenuTransform = MenuObj.transform;
            MenuTransform.parent = GorillaLocomotion.Player.Instance.rightHandTransform;
            MenuTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            MenuTransform.localPosition = new Vector3(-0.05f, 0.0f, 0.02f); // Menu offset
            MenuTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            // append button behaviours

            AddTrigger(MenuTransform.GetChild(2).GetChild(0).gameObject);
            AddTrigger(MenuTransform.GetChild(2).GetChild(1).gameObject);
            AddTrigger(MenuTransform.GetChild(3).gameObject);
        }

        private void AddTrigger(GameObject Button)
        {
            BoxCollider BoxCollider = Button.AddComponent<BoxCollider>();
            BoxCollider.isTrigger = true;

            switch (BoxCollider.gameObject.name)
            {
                case "Left":
                    Button.AddComponent<LeftArrow>();
                    break;
                case "Right":
                    Button.AddComponent<RightArrow>();
                    break;
                case "Preview":
                    Button.AddComponent<PreviewButton>();
                    break;
            }
        }

        public GameObject LoadAsset(string Name)
        {
            var Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ModInfo.InternalAssetBundle);
            AssetBundle AssetBundle = AssetBundle.LoadFromStream(Stream);
            var GameObjectPrefab = AssetBundle.LoadAsset<GameObject>(Name);
            GameObject Asset = Instantiate(GameObjectPrefab);

            AssetBundle.Unload(false);
            return Asset;
        }
    }
}
