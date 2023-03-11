using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TimeEditorGorillaTag.UI.Behaviours
{
    internal class RightArrow : MonoBehaviour
    {
        public static RightArrow Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
        }

        private float _time;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name != "FingerTrigger") return;
            if (_time > Time.realtimeSinceStartup) return;
        
            _time = Time.realtimeSinceStartup + 0.5f;

            SelectedMode.Instance.ChangeMode(1);
        }
    }
}
