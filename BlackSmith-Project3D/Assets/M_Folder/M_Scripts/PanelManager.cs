using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelManager : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;
    public GameObject panel4;  // Новая панель 4
    public GameObject panel5;  // Новая панель 5

    // Метод для открытия панели 1
    public void OpenPanel1()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);
    }

    // Метод для открытия панели 2
    public void OpenPanel2()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(false);
    }

    // Метод для открытия панели 3
    public void OpenPanel3()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(true);
        panel4.SetActive(false);
        panel5.SetActive(false);
    }

    // Метод для открытия панели 4
    public void OpenPanel4()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(true);
        panel5.SetActive(false);
    }

    // Метод для открытия панели 5
    public void OpenPanel5()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
        panel3.SetActive(false);
        panel4.SetActive(false);
        panel5.SetActive(true);
    }
}

