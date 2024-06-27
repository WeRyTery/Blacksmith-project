using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithingUI : MonoBehaviour
{
    [SerializeField] GameObject Close; // Button

    private void Start()
    {
        E_EventBus.EnableSmithingMechanicUI += EnableSmithingMechanicUI;
    }

    public void CloseMechanicProcess()
    {
        E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
        Close.SetActive(false);
    }

    public void EnableSmithingMechanicUI()
    {
        Close.SetActive(true);
    }
}
