using ComputerPlusPlus;
using GorillaNetworking;
using System.Reflection;
using System.Text;

namespace TimeEditor.Screens;

public class ChangeTimeScreen : IScreen
{
    public string Title => "TimeEditor";
    public string Description => $"Use W/S to navigate\nPress enter to select\nCurrent Time[{TimeManager.CurrentIndex}]";

    private int HoveringIndex;

    public string GetContent()
    {
        // Body
        StringBuilder stringBuilder = new("Select a time:\n");
        int timePresetCount = TimeManager.TimePresets.Count;
        for (int i = 0; i < timePresetCount; i++)
        {
            var timePreset = TimeManager.TimePresets.ElementAt(i);
            string message =
                (i == HoveringIndex ? "> " : "  ") // Line hovering arrow thingy
                + $"<color={(timePreset.Value == TimeManager.CurrentIndex ? "green" : "white")}>" // Selected colors
                + timePreset.Key
                + "</color>";
            stringBuilder.AppendLine(message);
        }

        // Footer
        const int screenLines = 6;
        int footerOffset = screenLines - (timePresetCount + 1);
        stringBuilder.Append(new string('\n', footerOffset));
        stringBuilder.Append(Main.InModdedRoom ? "All changes made will be reverted back as soon as you leave the room." : "<color=red>You must be in a modded room to use this</color>");

        return stringBuilder.ToString();
    }

    public void OnKeyPressed(GorillaKeyboardButton button)
    {
        switch (button.characterString.ToLower())
        {
            case "enter":
                if (Main.InModdedRoom)
                {
                    TimeManager.CurrentIndex = TimeManager.TimePresets.ElementAt(HoveringIndex).Value;
                    TimeManager.SetTime();
                    return;
                }

                // Incase the user has my notification mod it will tell them they cannot do this.
                if (BepInEx.Bootstrap.Chainloader.PluginInfos.TryGetValue("crafterbot.notificationlib", out BepInEx.PluginInfo notifcationPluginInfo))
                {
                    var monkeNotificationLib = notifcationPluginInfo.Instance.GetType().Assembly;
                    Type notificationControllerType = monkeNotificationLib.GetType("MonkeNotificationLib.NotificationController");
                    MethodInfo method = notificationControllerType.GetMethod("AppendMessage", new Type[] { typeof(string), typeof(string), typeof(bool), typeof(float) });
                    method.Invoke(null, new object[] { "TimeEditor", "<color=#ff0800>You must be in a modded room to do this!</color>", null, null });
                }
                break;

            // Up/Down
            case "w":
                if (HoveringIndex != 0) HoveringIndex--;
                break;
            case "s":
                if (HoveringIndex < TimeManager.TimePresets.Count - 1) HoveringIndex++;
                break;
        }
    }

    void IScreen.Start()
    {
        throw new NotImplementedException();
    }
}