using BepInEx;
using Utilla;

namespace TimeEditor
{
    [BepInPlugin("crafterbot.timeeditor", "Time Editor", "1.2.0"), BepInDependency("org.legoandmars.gorillatag.utilla")]
    [ModdedGamemode]
    internal class Main : BaseUnityPlugin
    {
        private static Main _instance;
        internal static bool InRoom;

        internal Main()
        {
            _instance = this;
            Log("Initializing...");

            Bepinject.Zenjector.Install<Interface.TimeEditorInstaller>().OnProject();
        }

        #region Utilla callbacks
        [ModdedGamemodeJoin]
        private void OnJoin() =>
            InRoom = true;
        [ModdedGamemodeLeave]
        private void OnLeave()
        {
            InRoom = false;
            TimeManager.Reset();
        }
        #endregion

        internal static void Log(object message, BepInEx.Logging.LogLevel logLevel = BepInEx.Logging.LogLevel.Info) =>
            _instance.Logger.Log(logLevel, message);
    }
}
