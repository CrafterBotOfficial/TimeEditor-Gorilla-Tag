using MonkeStatistics.API;

namespace TimeEditor.Pages
{
    [DisplayInMainMenu(Main.NAME)]
    internal class MainPage : Page
    {
        public override void OnPageOpen()
        {
            base.OnPageOpen(); // resets the pages lines :)
            DrawPage();
        }

        private void DrawPage()
        {
            SetTitle(Main.NAME);
            SetAuthor("By Crafterbot || Version:" + (Main.VersionValid ? Main.VERSION : Main.VERSION + " New version aviable"));

            AddLine("Override Time", new ButtonInfo(OnOverrideTimeButtonPress, 0, ButtonInfo.ButtonType.Toggle, TimeController.OverrideTime));

            AddLine(1);
            AddLine("Morning", new ButtonInfo(OnChangeDayButtonPress, (int)TimeOfDay.Morning));
            AddLine("Day", new ButtonInfo(OnChangeDayButtonPress, (int)TimeOfDay.Day));
            AddLine("Evening", new ButtonInfo(OnChangeDayButtonPress, (int)TimeOfDay.Evening));
            AddLine("Night", new ButtonInfo(OnChangeDayButtonPress, (int)TimeOfDay.Night));
            AddLine(1); // End  section

            AddLine("=<Custom Times>=");
            AddLine("Increase Time", new ButtonInfo(OnManualSetTimeButtonPress, 1));
            AddLine("Decrease Time", new ButtonInfo(OnManualSetTimeButtonPress, -1));

            AddLine(1);
            AddLine("Reset", new ButtonInfo((Sender, Args) => TimeController.ResetTime(), 0));

            SetLines();
        }

        private void OnOverrideTimeButtonPress(object sender, object[] Args)
        {
            TimeController.OverrideTime = (bool)Args[1];
        }
        private void OnChangeDayButtonPress(object sender, object[] Args)
        {
            TimeController.CurrentTimeOverride = (int)Args[0];
            TimeController.UpdateCustomTime();
        }

        private void OnManualSetTimeButtonPress(object Sender, object[] Args)
        {
            TimeController.CurrentTimeOverride += (int)Args[0];
            TimeController.CurrentTimeOverride.Log(BepInEx.Logging.LogLevel.Message);
        }
    }
}
