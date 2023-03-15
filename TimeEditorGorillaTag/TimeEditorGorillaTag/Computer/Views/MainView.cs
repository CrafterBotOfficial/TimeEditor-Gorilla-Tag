using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Text;
using TimeEditorGorillaTag.Computer.FileHandling;

namespace TimeEditorGorillaTag.Computer.Views
{
    internal class MainView : ComputerView
    {
        private UISelectionHandler SelectionHandler;

        public override void OnShow(object[] Arguments)
        {
            base.OnShow(Arguments);

            SelectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            SelectionHandler.MaxIdx = 3;
            SelectionHandler.OnSelected += SelectionHandler_OnSelected;
            SelectionHandler.ConfigureSelectionIndicator("<color=#ed6540>></color> ", "", "  ", ""); // This line is directly from the ComputerInterface mod ;-;

            Build();
        }

        private void SelectionHandler_OnSelected(int Index)
        {
            switch (Index)
            {
                case 0:
                    SettingsManager.BuilderSettings.ModEnabled = !SettingsManager.BuilderSettings.ModEnabled;
                    SettingsManager.Save();
                    TimeManager.Reset();
                    Build();
                    break;
                case 1:
                    TimeManager.Reset();
                    break;
                case 2:
                    ShowView<PresetsView>();
                    break;
            }
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
                    ReturnToMainMenu();
                    break;
            }
        }

        private void TickButtonUpdater()
        {
            // This script wll make the buttons orange for 0.5 seconds. For a future update
        }

        private void Build()
        {
            StringBuilder MenuStringBuild = new StringBuilder()

                .BeginCenter()

                // Header
                .MakeBar(char.Parse("="), SCREEN_WIDTH / 2, 0, "#808080")
                .AppendClr("\n" + ModInfo.ModName, "#cc6600")
                .AppendLine("\nBy Crafterbot")
                .MakeBar(char.Parse("="), SCREEN_WIDTH / 2, 0, "#808080")
                .AppendLine();// For some reason append line isn't chanding the line correctly:(

            // Body

            string ModEnabled_Text = SettingsManager.BuilderSettings.ModEnabled ? "Enabled" : "Disabled";

            MenuStringBuild
                .AppendLine(SelectionHandler.GetIndicatedText(0, ModEnabled_Text))
                .AppendLine(SelectionHandler.GetIndicatedText(1, "Default"))
                .AppendLine(SelectionHandler.GetIndicatedText(2, "Presets"));

            SetText(MenuStringBuild);
        }
    }
}
/*
Index list:
    0: Enable/Disable
    1: Set time to default
    2: Presets
    3: Custom
*/