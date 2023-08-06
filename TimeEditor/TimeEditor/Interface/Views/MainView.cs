using ComputerInterface;
using ComputerInterface.Interfaces;
using ComputerInterface.ViewLib;
using System;
using System.Text;

namespace TimeEditor.Interface.Views
{
    internal class MainView : ComputerView
    {
        private UISelectionHandler _selectionHandler;

        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            _selectionHandler = new UISelectionHandler(EKeyboardKey.Left, EKeyboardKey.Right, EKeyboardKey.Enter);
            _selectionHandler.OnSelected += SelectionHandler_OnSelected;
            _selectionHandler.MaxIdx = Enum.GetValues(typeof(ETimePreset)).Length - 1;
            _selectionHandler.CurrentSelectionIndex = (int)TimeManager.Instance.CurrentTime;

            DrawPage();
        }

        private void DrawPage()
        {
            var stringBuilder = new StringBuilder();

            //// Header
            stringBuilder
                .BeginCenter().MakeBar('=', SCREEN_WIDTH, 0)
                .AppendLine("TimeEditor")
                .AppendSize("Select a time of day to set the current time to. Press option1 to unfreeze the time/\n", 70)
                .MakeBar('=', SCREEN_WIDTH, 0).EndAlign()
            ;

            //// Body
            stringBuilder
                .AppendLines(2) // seperator
                .AppendLine("Current Selected Time:" + Enum.GetNames(typeof(ETimePreset))[_selectionHandler.CurrentSelectionIndex])
                ;

            SetText(stringBuilder);
        }

        /* Handlers */

        private void SelectionHandler_OnSelected(int obj)
        {
            ETimePreset newPreset = (ETimePreset)Enum.GetValues(typeof(ETimePreset)).GetValue(obj);
            TimeManager.Instance.SetTime(newPreset);
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            if (_selectionHandler.HandleKeypress(key))
            {
                DrawPage();
                return;
            }

            switch (key)
            {
                case EKeyboardKey.Option1:
                    TimeManager.Instance.UnfreezeTime();
                    break;
                case EKeyboardKey.Back:
                    ReturnToMainMenu();
                    break;
            }
        }

        /* Models & entry */

        internal class Entry : IComputerModEntry
        {
            string IComputerModEntry.EntryName => "TimeEditor";
            Type IComputerModEntry.EntryViewType => typeof(MainView);
        }
    }
}
