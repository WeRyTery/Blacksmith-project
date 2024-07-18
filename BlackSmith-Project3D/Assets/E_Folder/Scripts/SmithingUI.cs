using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithingUI : MonoBehaviour
{
    public E_CameraManagment CameraStateScript;
    private Canvas smithingCanvas;

    private void Start()
    {
        E_EventBus.EnableSmithingMechanicUI += EnableSmithingMechanicUI;
        smithingCanvas = gameObject.GetComponent<Canvas>();
    }

    public void CloseMechanicProcess()
    {
        E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
        CameraStateScript.IsSmelting = false;
        CameraStateScript.IsSmiting = false;
        CameraStateScript.IsSharpening = false;

        smithingCanvas.enabled = false;
    }

    public void EnableSmithingMechanicUI()
    {
        smithingCanvas.enabled = true;
    }
}
