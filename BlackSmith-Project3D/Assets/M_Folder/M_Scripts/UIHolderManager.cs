using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHolderManager : MonoBehaviour
{
    public GameObject HolderPanel;
    public GameObject ShowHolderPanel;
    public GameObject ShopCanvas;
    
    public void ShowPanel()
    {
        HolderPanel.SetActive(!HolderPanel.active);
        ShowHolderPanel.SetActive(!ShowHolderPanel.active);
    }

    public void OpenShop()
    {
        ShopCanvas.SetActive(true);
    }

    public void ClosePanel()
    {
        if (ShowHolderPanel != null)
        {
            ShowHolderPanel.SetActive(false);

            if (HolderPanel != null)
            {
                HolderPanel.SetActive(true);
            }
        }
    }

}