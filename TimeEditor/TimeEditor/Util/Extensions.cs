using BepInEx.Logging;
using TimeEditor;

/*namespace TimeEditor.Util
{*/
internal static class Extensions
{
    internal static void Log(this object obj, LogLevel logLevel = LogLevel.Info)
    {
#if DEBUG
        Main.manualLogSource.Log(logLevel, obj);
#endif
    }
}
//}
