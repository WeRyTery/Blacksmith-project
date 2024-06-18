using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNextLogic : MonoBehaviour
{
    [SerializeField] private int CurrentPeriode = 0;
    [SerializeField] private GameObject FakeSwordObject;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject GameMechanicControl;
    [SerializeField] private GameObject[] CamerasSmithing = new GameObject[3];
    private void Start()
    {
        GameMechanicControl.GetComponent<V_ToolMaker>().enabled = false;
    }
    public void TheNextTask()
    {
        switch (CurrentPeriode)
        {
            case 0:
                FakeSwordObject.SetActive(false);
                CamerasSmithing[0].SetActive(false);
                CamerasSmithing[1].SetActive(true);
                CamerasSmithing[2].SetActive(false);
                CurrentPeriode++;
                break;
            case 1:
                CamerasSmithing[0].SetActive(false);
                CamerasSmithing[1].SetActive(false);
                CamerasSmithing[2].SetActive(true);
                CurrentPeriode++;
                break;
            case 2:
                CamerasSmithing[0].SetActive(false);
                CamerasSmithing[1].SetActive(false);
                CamerasSmithing[2].SetActive(false);
                MainCamera.SetActive(true);
                Player.SetActive(true);
                CurrentPeriode = 0;
                break;
        }
    }
}
