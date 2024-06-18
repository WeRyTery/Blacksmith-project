using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways] // Executes even when in editor mode, before starting the game
public class TimeManager : MonoBehaviour
{
    // BE CAREFULL WHEN YOU CHANGE TIME VARIABLES WITH CTRL + R + R, IF YOU CHANGE IT TO "TIME", WHOLE GAME WILL BE BROKEN!
    [Header("Preset variables")]
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset LightPreset;
    [SerializeField] private string DataPath;
    [Space]

    [Header("Time related")]
    [SerializeField] private float RealSecondsInGameHour = 60; // if value == 60, than 24h ingame = 24h in real life
    [SerializeField] private int SaveTimeDataEveryInSeconds; // Saves time every X seconds

    [SerializeField, Range(0, 24)] private float TimeOfDay;
    [SerializeField, Range(0, 24)] private int HourOfDay;
    [SerializeField, Range(0, 60)] private int MinuteOfHour;

    [SerializeField] TextMeshProUGUI CurrentTimeText;

    private float minuteHandler; // To make time control through inspector smoother

    public static List<TimeDataPreset> timeDataList = new List<TimeDataPreset>();

    private void Start()
    {
        LoadTime();

        InvokeRepeating("SaveTime", SaveTimeDataEveryInSeconds, SaveTimeDataEveryInSeconds);
    }

    private void Update()
    {
        if (LightPreset == null)
        {
            return;
        }

        if (Application.isPlaying)
        {
            minuteHandler += UnityEngine.Time.deltaTime;

            if (minuteHandler >= 1)
            {
                minuteHandler = 0;
                MinuteOfHour += 1;
            }

            if (MinuteOfHour >= RealSecondsInGameHour)
            {
                MinuteOfHour = 0;
                HourOfDay++;
            }

            if (HourOfDay >= 24)
            {
                HourOfDay = 0;
            }

            TimeOfDay = HourOfDay + (MinuteOfHour / RealSecondsInGameHour);
            TimeOfDay %= 24;
            UpdateLighting(TimeOfDay / 24f);
            UpdateTimeUI();
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = LightPreset.AmbientColor.Evaluate(timePercent); // Evaluate - calculates color
        RenderSettings.fogColor = LightPreset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = LightPreset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, -170, 0));
        }
    }

    //Tries to find directional light source, if can't - searches for any directional lights on the scene 
    private void OnValidate()
    {
        if (DirectionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    private void UpdateTimeUI()
    {
        string hourString = (HourOfDay < 10) ? "0" + HourOfDay.ToString() : HourOfDay.ToString();
        string minuteString = (MinuteOfHour < 10) ? "0" + MinuteOfHour.ToString() : MinuteOfHour.ToString();

        CurrentTimeText.text = hourString + " : " + minuteString;
    }

    private void SaveTime()
    {
        Debug.Log("Saving time...");
        timeDataList.Clear();
        timeDataList.Add(new TimeDataPreset(TimeOfDay, HourOfDay, MinuteOfHour));

        FileHandler.SaveToJSON<TimeDataPreset>(timeDataList, DataPath);
        Debug.Log("Time saved");
    }

    private void LoadTime()
    {
        Debug.Log("Loading time...");
        if (timeDataList is null)
        {
            return;
        }

        timeDataList = FileHandler.ReadListFromJSON<TimeDataPreset>(DataPath);

        if (timeDataList.Count > 0)
        {
            TimeOfDay = timeDataList[timeDataList.Count - 1].DayTime;
            HourOfDay = timeDataList[timeDataList.Count - 1].DayHour;
            MinuteOfHour = timeDataList[timeDataList.Count - 1].DayMinute;
            Debug.Log("Time loaded");
        }

    }

    private void OnApplicationQuit()
    {
        SaveTime();
    }
}
