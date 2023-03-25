using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Text;
using TimeEditor.Managers;
using UnityEngine;

namespace TimeEditor.UI.Views
{
    internal class MainView : ComputerView
    {
        private UISelectionHandler selectionHandler;

        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            selectionHandler.ConfigureSelectionIndicator("<color=#ed6540>></color> ", "", "  ", "");
            selectionHandler.MaxIdx = 2;
            //selectionHandler.CurrentSelectionIndex = 0;

            selectionHandler.OnSelected += SelectionHandler_OnSelected;
            Build();
        }

        // Green: #09ff00
        // Red: #ff0800

        private void Build()
        {
            string Enabled = Managers.TimeManager.Instance.Enabled ? "<color=#09ff00>[Enabled]</color>" : "<color=#ff0800>[Disabled]</color>";
            string SetTime = "Set Time";
            string Credit_And_Info = "Credits & Info";

            string VersionValid = Managers.BuildValid.VersionValid ? "<color=#09ff00>Version up to date</color>" : $"<color=#ff0800>There is a new version of {ModInfo.ModName}\n {Managers.BuildValid.Version} \n {Managers.BuildValid.VersionDescription}";

            StringBuilder stringBuilder = Universal.Header(SCREEN_WIDTH, "Time Editor", "By Crafterbot", 2);
            // Middle
            stringBuilder
                .AppendLine(selectionHandler.GetIndicatedText(0, Enabled))
                .AppendLine(selectionHandler.GetIndicatedText(1, SetTime))
                .AppendLine(selectionHandler.GetIndicatedText(2, Credit_And_Info));
            // Bottom
            stringBuilder
                .AppendLines(1)
                .AppendSize(VersionValid, 75);

            SetText(stringBuilder);
        }

        private void SelectionHandler_OnSelected(int obj)
        {
            switch (obj)
            {
                case 0:
                    if (!TimeManager.Instance.RoomValid) return;
                    Managers.TimeManager.Instance.Enabled = !Managers.TimeManager.Instance.Enabled;
                    Build();
                    break;
                case 1:
                    ShowView<SetTime>();
                    break;
                case 2:
                    ShowView<Credits>();
                    break;
            }
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            base.OnKeyPressed(key);

            if (selectionHandler.HandleKeypress(key))
            {
                Build();
                return;
            }
            
            switch (key)
            {
                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
