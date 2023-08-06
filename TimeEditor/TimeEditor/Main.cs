using BepInEx;
using HarmonyLib;
using Utilla;

namespace TimeEditor
{
    [BepInPlugin("crafterbot.timeeditor", "Time Editor", "1.2.0"), BepInDependency("org.legoandmars.gorillatag.utilla")]
    [ModdedGamemode]
    internal class Main : BaseUnityPlugin
    {
        private static Main _instance;
        private Harmony _harmony;

        internal Main()
        {
            _instance = this;
            _harmony = new Harmony(Info.Metadata.GUID);

            Bepinject.Zenjector.Install<Interface.MainInstaller>().OnProject();
            TimeManager.Instance = new TimeManager();
        }

        #region Utilla callbacks
        [ModdedGamemodeJoin]
        private void OnJoin() =>
            _harmony.PatchAll(typeof(Patches));
        [ModdedGamemodeLeave]
        private void OnLeave()
        {
            TimeManager.Instance.UnfreezeTime();
            _harmony.UnpatchSelf();
        }
        #endregion

        internal static void Log(object message, BepInEx.Logging.LogLevel logLevel = BepInEx.Logging.LogLevel.Info)
        {
            if (_instance is object)
            {
                _instance.Logger.Log(logLevel, message);
                return;
            }
            UnityEngine.Debug.Log("TimeEditor : " + message);
        }
    }
}
