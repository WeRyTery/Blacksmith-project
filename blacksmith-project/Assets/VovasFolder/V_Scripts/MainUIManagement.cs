using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManagement : MonoBehaviour
{
    public GameObject buttonsPanel;
    public GameObject settingsPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowSettings()
    {
        buttonsPanel.SetActive(!buttonsPanel.active);
        settingsPanel.SetActive(!settingsPanel.active);
    }
    public void CloseSettingsPanel()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);

            if (buttonsPanel != null)
            {
                buttonsPanel.SetActive(true);
            }
        }
    }
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
