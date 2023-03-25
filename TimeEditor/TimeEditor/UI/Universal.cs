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
                .BeginCenter()
                .MakeBar('=', SCREEN_WIDTH / 2, 0)
                .AppendLine()
                .AppendLine(Title)
                .AppendClr(Author, "#545454")
                .AppendLine()
                .MakeBar('=', SCREEN_WIDTH / 2, 0)
                .AppendLines(Offset);

            return stringBuilder;
        }
    }
}
