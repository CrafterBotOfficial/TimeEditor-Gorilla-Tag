using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Text;

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
            selectionHandler.MaxIdx = 0;
            selectionHandler.CurrentSelectionIndex = 0;

            selectionHandler.OnSelected += SelectionHandler_OnSelected;
            Build();
        }

        // Green: #09ff00
        // Red: #ff0800

        private void Build()
        {
            string Enabled = Managers.TimeManager.Instance.Enabled && Managers.TimeManager.Instance.RoomValid ? "<color=#09ff00>[Enabled]</color>" : "<color=#ff0800>[Disabled]</color>";
            string SetTime = Managers.TimeManager.Instance.RoomValid ? "Set Time" : "<color=#ff0800>Set Time</color>";
            string Credit_And_Info = "Credits & Info";

            string VersionValid = Managers.BuildValid.VersionValid ? "<color=#09ff00>Version up to date</color>" : $"<color=#ff0800>There is a new version of {ModInfo.ModName}\n {Managers.BuildValid.Version} \n {Managers.BuildValid.VersionDescription}";

            StringBuilder stringBuilder = Universal.Header(SCREEN_WIDTH, "Time Editor", "By Crafterbot", 2);
            stringBuilder.BeginCenter();
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
            throw new System.NotImplementedException();
        }
    }
}
