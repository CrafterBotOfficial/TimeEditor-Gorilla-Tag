using ComputerInterface;
using ComputerInterface.Interfaces;
using ComputerInterface.ViewLib;
using System;
using System.Linq;
using System.Text;

namespace TimeEditor.Interface.Views
{
    internal class EditTimeView : ComputerView
    {
        private UISelectionHandler _selectionHandler;
        private UISelectionHandler _arrowSelectionHandler;

        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            _selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down);
            _selectionHandler.ConfigureSelectionIndicator("<color=#ed6540>> </color>", "", "  ", "");
            _selectionHandler.MaxIdx = TimeManager.TimePresets.Count - 1;
            _selectionHandler.OnSelected += OnSelected;

            _arrowSelectionHandler = new UISelectionHandler(EKeyboardKey.Left, EKeyboardKey.Right);
            _arrowSelectionHandler.ConfigureSelectionIndicator("< ", " >", "", "");
            _arrowSelectionHandler.MaxIdx = BetterDayNightManager.instance.timeOfDayRange.Length;
            _arrowSelectionHandler.OnSelected += OnArrowSelected;

            DrawPage();
        }

        private void DrawPage()
        {
            StringBuilder stringBuilder = new StringBuilder()
                .MakeBar('=', SCREEN_WIDTH, 0)
                .Append("\nTime Editor\nBy Crafterbot\n")
                .MakeBar('=', SCREEN_WIDTH, 0)
                .AppendLines(1)
                .AppendLine($"Current Index:{TimeManager.Current}")
                ;

            var dictionary = TimeManager.TimePresets;
            for (int i = 0; i < dictionary.Count; i++)
            {
                string name = Enum.GetName(typeof(ETimePreset), dictionary.ElementAt(i).Key);
                stringBuilder.AppendLine(_selectionHandler.GetIndicatedText(i, name));
            }

            SetText(stringBuilder);
        }

        /* Handlers */

        public override void OnKeyPressed(EKeyboardKey key)
        {
            if (_selectionHandler.HandleKeypress(key) || _arrowSelectionHandler.HandleKeypress(key))
            {
                DrawPage();
                return;
            }
            ReturnToMainMenu();
        }

        private void OnSelected(int obj)
        {
            TimeManager.SetTime(obj);
        }

        private void OnArrowSelected(int obj)
        {
            TimeManager.SetTime(obj);
        }

        /* Models & Entry */

        internal class Entry : IComputerModEntry
        {
            string IComputerModEntry.EntryName => "Time Editor";
            Type IComputerModEntry.EntryViewType => typeof(EditTimeView);
        }
    }
}
