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

            _selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
            _selectionHandler.ConfigureSelectionIndicator("<color=#ed6540>> </color>", "", "  ", "");
            _selectionHandler.MaxIdx = Enum.GetNames(typeof(ETimePreset)).Length;
            _selectionHandler.OnSelected += OnSelected;

            _arrowSelectionHandler = new UISelectionHandler(EKeyboardKey.Left, EKeyboardKey.Right);
            _arrowSelectionHandler.ConfigureSelectionIndicator("< ", " >", "", "");
            _arrowSelectionHandler.MaxIdx = BetterDayNightManager.instance.timeOfDayRange.Length;

            DrawPage();
        }

        private void DrawPage()
        {
            StringBuilder stringBuilder = new StringBuilder()
                .BeginAlign("center")
                .MakeBar('=', SCREEN_WIDTH, 0)
                .Append("\nTime Editor\nBy Crafterbot\n")
                .MakeBar('=', SCREEN_WIDTH, 0)
                .AppendLines(1)
                .AppendLine($"Current Index:{TimeManager.Current}")
                .EndAlign()
                ;

            var dictionary = Enum.GetNames(typeof(ETimePreset));
            foreach (var item in dictionary.Select((value, i) => new { i, value }))
            {
                stringBuilder.AppendLine(_selectionHandler.GetIndicatedText(item.i, item.value));
            }

            stringBuilder.AppendLine(_selectionHandler.GetIndicatedText(dictionary.Length, "Reset"));

            SetText(stringBuilder);
        }
       
        /* Handlers */

        public override void OnKeyPressed(EKeyboardKey key)
        {
            bool arrowSelection = _arrowSelectionHandler.HandleKeypress(key);
            if (_selectionHandler.HandleKeypress(key) || arrowSelection)
            {
                if (arrowSelection)
                    TimeManager.SetTime(_arrowSelectionHandler.CurrentSelectionIndex);
                DrawPage();
                return;
            }

            if (_arrowSelectionHandler.HandleKeypress(key)) 
            {
                DrawPage();
                return;
            }

            ReturnToMainMenu();
        }

        private void OnSelected(int obj)
        {
            if (obj == _selectionHandler.MaxIdx)
            {
                TimeManager.Reset();
                return;
            }
            ETimePreset timePreset = (ETimePreset)Enum.GetValues(typeof(ETimePreset)).GetValue(obj);
            TimeManager.SetTime(timePreset);
        }

        /* Models & Entry */

        internal class Entry : IComputerModEntry
        {
            string IComputerModEntry.EntryName => "Time Editor";
            Type IComputerModEntry.EntryViewType => typeof(EditTimeView);
        }
    }
}
