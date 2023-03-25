using ComputerInterface;
using ComputerInterface.ViewLib;
using System.Text;

namespace TimeEditor.UI.Views
{
    internal class Credits : ComputerView
    {
        public override void OnShow(object[] args)
        {
            base.OnShow(args);

            StringBuilder stringBuilder = Universal.Header(SCREEN_WIDTH, "Credits", "", 1);
            stringBuilder
                .BeginCenter()
                .AppendSize("Time Editor is a simple mod for the game Gorilla Tag that allows you to change the time of day through 5 presets.", 70)
                .AppendSize("This product is not affiliated with Gorilla Tag or Another Axiom LLC and is not endorsed or otherwise sponsored by Another Axiom LLC. Portions of the materials contained herein are property of Another Axiom LLC. :copyright: 2021 Another Axiom LLC.\r\n", 60);
            SetText(stringBuilder);
        }

        public override void OnKeyPressed(EKeyboardKey key)
        {
            base.OnKeyPressed(key);
            ShowView<MainView>();
        }
    }
}
