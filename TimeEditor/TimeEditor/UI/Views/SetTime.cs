using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Linq;
using System.Text;
using TimeEditor.Managers;

namespace TimeEditor.UI.Views
{
    internal class SetTime : ComputerView
    {
        private UISelectionHandler selectionHandler;

        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            selectionHandler.ConfigureSelectionIndicator("<color=#ed6540>></color> ", "", "  ", "");
            selectionHandler.MaxIdx = TimePatch.TimePresets.Count + 1;
            selectionHandler.CurrentSelectionIndex = 0;

            selectionHandler.OnSelected += SelectionHandler_OnSelected;
            Build();
        }

        // Green: #09ff00
        // Red: #ff0800

        private void Build()
        {
            StringBuilder stringBuilder = Universal.Header(SCREEN_WIDTH, "Select Time", "You MUST be in a Modded Lobby!", 1);
            stringBuilder.BeginCenter();

            // Middle
            var Dictionary = Managers.TimePatch.TimePresets;
            for (int i = 0; i < Dictionary.Count; i++)
            {
                int Index = selectionHandler.CurrentSelectionIndex > Dictionary.Count ? Dictionary.Count : selectionHandler.CurrentSelectionIndex;
                string Text = Index == i ? $"<color=#09ff00>{Dictionary.ElementAt(i).Key}</color>" : Dictionary.ElementAt(i).Key;
                stringBuilder.AppendLine(selectionHandler.GetIndicatedText(i, Text));
            }

            // Divider between presets and custom value
            stringBuilder
                .AppendLines(1)
                .AppendSize("Custom Time", 115)
                .AppendLines(2);

            // Custom value input
            string UseCustom = TimePatch.UseOverride ? "<color=#09ff00>[Enabled]</color>" : "<color=#ff0800>[Disabled]</color>";
            string CustomValue = $"< {TimePatch.IndexOverride} >";
            stringBuilder
                .AppendLine(selectionHandler.GetIndicatedText(Dictionary.Count, UseCustom))
                .AppendLine(selectionHandler.GetIndicatedText(Dictionary.Count + 1, CustomValue));

            SetText(stringBuilder);
        }


        private void SelectionHandler_OnSelected(int obj)
        {
            if (selectionHandler.CurrentSelectionIndex == TimePatch.TimePresets.Count) // Custom value
            {
                TimePatch.UseOverride = !TimePatch.UseOverride;
            }
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            base.OnKeyPressed(key);

            if (selectionHandler.HandleKeypress(key))
            {
                Build();
            }

            switch (key)
            {
                case EKeyboardKey.Up:
                    TimeEditor.Managers.TimePatch.TimeOfDay = TimeEditor.Managers.TimePatch.TimePresets.ElementAt(selectionHandler.CurrentSelectionIndex).Key;
                    break;
                case EKeyboardKey.Down:
                    TimeEditor.Managers.TimePatch.TimeOfDay = TimeEditor.Managers.TimePatch.TimePresets.ElementAt(selectionHandler.CurrentSelectionIndex).Key;
                    break;

                // Custom value switch
                case EKeyboardKey.Left:
                    if (selectionHandler.CurrentSelectionIndex == TimePatch.TimePresets.Count + 1)
                    {
                        TimePatch.IndexOverride--;
                        Build();
                    }
                    break;
                case EKeyboardKey.Right:
                    if (selectionHandler.CurrentSelectionIndex == TimePatch.TimePresets.Count + 1)
                    {
                        TimePatch.IndexOverride++;
                        Build();
                    }
                    break;

                case EKeyboardKey.Back:
                    ShowView<MainView>();
                    break;
            }
        }
    }
}
