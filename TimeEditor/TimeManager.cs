namespace TimeEditor;

public static class TimeManager
{
    public static int CurrentIndex;

    public static Dictionary<string, int> TimePresets = new()
    {
        { "Morning", 0 },
        { "Day", 1 },
        { "Evening", 2 },
        { "Night", 3 },
    };
}
