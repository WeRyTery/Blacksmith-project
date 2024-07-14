using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SmeltingLogic : MonoBehaviour
{

    [SerializeField] private float TimeToSmeltMaterial = 5f;
    public SmithingCycle ControlOfMechanic;

    public void SmeltingStart()
    {
        StartCoroutine(StartSmelting());
    }

    IEnumerator StartSmelting()
    {
        yield return new WaitForSeconds(TimeToSmeltMaterial);
        ControlOfMechanic.MaterialToDisplay.SetActive(false);
        ControlOfMechanic.InstrumentToDisplay[0].SetActive(true);
        Debug.Log("Smelting");

    }
}
