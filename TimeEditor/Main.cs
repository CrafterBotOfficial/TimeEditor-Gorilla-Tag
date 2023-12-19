using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PlayFab.MultiplayerModels;
using Utilla;

namespace TimeEditor;

[BepInPlugin("crafterbot.timeeditor", "Time Editor", "2.0.0"), BepInDependency("org.legoandmars.gorillatag.utilla"), BepInDependency("com.kylethescientist.gorillatag.computerplusplus")]
[BepInProcess("Gorilla Tag.exe")]
[ModdedGamemode]
public class Main : BaseUnityPlugin
{
    public static Main Instance;

    public static bool InModdedRoom;

    public Main()
    {
        Instance = this;
    }

    #region Modded Room
    [ModdedGamemodeJoin]
    private void OnModdedRoomJoined()
    {
        InModdedRoom = true;
    }
    [ModdedGamemodeLeave]
    private void OnModdedRoomLeave()
    {
        InModdedRoom = false;

        TimeManager.CurrentIndex = -1;
        TimeManager.SetTime();
    }
    #endregion

    public static void Log(object data, LogLevel logLevel = LogLevel.Info)
    {
        if (Instance is object)
        {
            Instance.Logger.Log(logLevel, data);
            return;
        }
        UnityEngine.Debug.Log(data);
    }
}
