using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SmeltingLogic : MonoBehaviour
{
    [SerializeField] GameObject FurnaceLights;
    [SerializeField] private float TimeToSmeltMaterial = 5f;
    public SmithingCycle ControlOfMechanic;

    public void SmeltingStart()
    {
        StartCoroutine(StartSmelting());
        FurnaceLights.SetActive(true);
    }

    IEnumerator StartSmelting()
    {
        yield return new WaitForSeconds(TimeToSmeltMaterial);
        FurnaceLights.SetActive(false);
        ControlOfMechanic.MaterialToDisplay.SetActive(false);
        ControlOfMechanic.InstrumentToDisplay[0].SetActive(true);
        Debug.Log("Smelting");

    }
}
