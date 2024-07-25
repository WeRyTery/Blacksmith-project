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
        ActivatePanel(0);
    }

    public void OpenGraphicSettings()
    {
        ActivatePanel(1);
    }

   

    private void ActivatePanel(int index)
    {
        for (int i = 0; i < SettingsPanels.Length; i++)
        {
            if (i == index)
            {
                SettingsPanels[i].SetActive(true);
            }
            else
            {
                SettingsPanels[i].SetActive(false);
            }
        }
    }
}