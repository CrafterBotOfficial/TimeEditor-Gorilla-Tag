using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace TimeEditorGorillaTag.UI
{
    internal class Input : MonoBehaviour
    {
        private void Update()
        {
            // Mod allowed set

            Main.Instance.ModAllowed = Main.Instance._modEnabled && Main.Instance._roomModded;

            // Input manager

            if (!Main.Instance.ModAllowed) return;

            // XR right controller
            List<InputDevice> InputDeviceStatusList = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, InputDeviceStatusList);
            if (InputDeviceStatusList.Count == 0) return;
            
            InputDeviceStatusList[0].TryGetFeatureValue(CommonUsages.grip, out float Grip);

            GameObject MenuObj = UIConstructor.Instance.MenuTransform.gameObject;

            if (Grip <= 0.5f) 
            { 
                if (MenuObj.active) Main.Instance.CloseMenu();
                return; 
            }

            if (!MenuObj.active) Main.Instance.OpenMenu();
        }
    }
}
