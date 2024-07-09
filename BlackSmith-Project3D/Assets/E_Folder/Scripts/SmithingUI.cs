using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithingUI : MonoBehaviour
{
    [SerializeField] GameObject Close; // Button
    [SerializeField] GameObject Next; // Button
    public E_CameraManagment CameraStateScript;
    private void Start()
    {
        E_EventBus.EnableSmithingMechanicUI += EnableSmithingMechanicUI;
    }

    public void CloseMechanicProcess()
    {
        E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
        CameraStateScript.IsSmelting = false;
        CameraStateScript.IsSmiting = false;
        CameraStateScript.IsSharpening = false;
        Close.SetActive(false);
        Next.SetActive(false);
    }

    public void EnableSmithingMechanicUI()
    {
        Close.SetActive(true);
        Next.SetActive(true);
    }
}
