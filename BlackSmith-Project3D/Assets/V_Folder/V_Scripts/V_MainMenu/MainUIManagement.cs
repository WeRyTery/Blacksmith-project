using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManagement : MonoBehaviour
{
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject[] SettingsPanels;

    public void PlayGame(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ShowSettings()
    {
        Settings.SetActive(!Settings.active);
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void OpenAudioSettings()
    {
        SettingsPanels[0].SetActive(!SettingsPanels[0].active);
    } 
    public void OpenGraphicSettings()
    {
        SettingsPanels[1].SetActive(!SettingsPanels[1].active);
    } 
    public void OpenKEyBindSettings()
    {
        SettingsPanels[2].SetActive(!SettingsPanels[2].active);
    }
}
