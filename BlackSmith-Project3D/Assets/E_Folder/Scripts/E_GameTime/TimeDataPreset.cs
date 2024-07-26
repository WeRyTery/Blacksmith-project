using System;

[Serializable]
public class TimeDataPreset
{
    public float DayTime;
    public int DayHour;
    public int DayMinute;
    public int DayNumber;

    public TimeDataPreset(float dayTime, int dayHour, int dayMinute, int dayNumber)
    {
        DayTime = dayTime;
        DayHour = dayHour;
        DayMinute = dayMinute;
        DayNumber = dayNumber;
    }
}
