using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Utilla;

namespace TimeEditor;

[BepInPlugin("crafterbot.timeeditor", "Time Editor", "2.0.0"), BepInDependency("org.legoandmars.gorillatag.utilla"), BepInDependency("com.kylethescientist.gorillatag.computerplusplus")]
[BepInProcess("Gorilla Tag.exe")]
[ModdedGamemode]
public class Main : BaseUnityPlugin
{
    private static Main Instance;

    public static bool InModdedRoom;
    private readonly Harmony HarmonyInstance;

    public Main()
    {
        Instance = this;

        HarmonyInstance = new(Info.Metadata.GUID);
        HarmonyInstance.PatchAll(typeof(Patches));
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
