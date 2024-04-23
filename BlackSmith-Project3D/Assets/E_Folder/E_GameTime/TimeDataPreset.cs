using System;

[Serializable]
public class TimeDataPreset
{
    public float DayTime;
    public int DayHour;
    public int DayMinute;

    public TimeDataPreset(float dayTime, int dayHour, int dayMinute)
    {
        DayTime = dayTime;
        DayHour = dayHour;
        DayMinute = dayMinute;
    }
}
