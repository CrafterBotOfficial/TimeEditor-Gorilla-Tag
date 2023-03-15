using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Configuration;
using System.Text;
using TimeEditorGorillaTag.Computer.FileHandling;

namespace TimeEditorGorillaTag.Computer.Views
{
    internal class PresetsView : ComputerView
    {
        private UISelectionHandler SelectionHandler;

        public override void OnShow(object[] Arguments)
        {
            base.OnShow(Arguments);

            SelectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            SelectionHandler.MaxIdx = 4;
            SelectionHandler.OnSelected += SelectionHandler_OnSelected;
            SelectionHandler.ConfigureSelectionIndicator("<color=#ed6540>></color> ", "", "  ", ""); // This line is directly from the ComputerInterface mod ;-;

            Build();
        }

        private void SelectionHandler_OnSelected(int Index)
        {
            SettingsManager.BuilderSettings.Preset = Index;
            SettingsManager.Save();
        }

        

        private void Build()
        {
            StringBuilder MenuStringBuild = new StringBuilder()

                .BeginCenter()

                // Header
                .MakeBar(char.Parse("="), SCREEN_WIDTH / 2, 0, "#808080")
                .AppendClr("\n" + ModInfo.ModName+"\n", "#cc6600")
                .MakeBar(char.Parse("="), SCREEN_WIDTH / 2, 0, "#808080")
                .AppendLine();

            // Body
            int Using = SettingsManager.BuilderSettings.Preset;
            string Warning = SettingsManager.BuilderSettings.ModEnabled ? "" : "<color=#ed6540>The mod is currently disabled</color>";
            
            MenuStringBuild
                .AppendLine(SelectionHandler.GetIndicatedText(0, Using == 0 ? "[Morning]" : "Morning"))
                .AppendLine(SelectionHandler.GetIndicatedText(1, Using == 1 ? "[Day]" : "Day"))
                .AppendLine(SelectionHandler.GetIndicatedText(2, Using == 2 ? "[Evening]" : "Evening"))
                .AppendLine(SelectionHandler.GetIndicatedText(3, Using == 3 ? "[Night]" : "Night"))
                .AppendLines(2)
                .AppendLine(Warning);

            SetText(MenuStringBuild);
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            if (SelectionHandler.HandleKeypress(key))
            {
                Build();
                return;
            }

            switch (key)
            {
                case (EKeyboardKey.Up):
                    SelectionHandler.MoveSelectionUp();
                    Build();
                    break;
                case (EKeyboardKey.Down):
                    SelectionHandler.MoveSelectionDown();
                    Build();
                    break;
                case (EKeyboardKey.Back):
                    ShowView<MainView>();
                    break;
            }
        }
    }
}
