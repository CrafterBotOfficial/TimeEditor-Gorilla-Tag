using ComputerInterface;
using System.Text;

namespace TimeEditor.UI
{
    internal class Universal
    {
        public static StringBuilder Header(int SCREEN_WIDTH, string Title, string Author, int Offset)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .MakeBar('=', SCREEN_WIDTH / 2, 0)
                .AppendLine(Title)
                .AppendLines(1)
                .AppendClr(Author, "#545454")
                .MakeBar('=', SCREEN_WIDTH / 2, 0)
                .AppendLines(Offset);

            return stringBuilder;
        }
    }
}
