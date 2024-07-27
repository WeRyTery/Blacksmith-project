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
        ControlOfMechanic.Materials[ControlOfMechanic.MaterialIndex].SetActive(true);

        yield return new WaitForSeconds(TimeToSmeltMaterial);

        FurnaceLights.SetActive(false);
        ControlOfMechanic.Materials[ControlOfMechanic.MaterialIndex].SetActive(false);
        ControlOfMechanic.Instruments[0].SetActive(true);


        ControlOfMechanic.Materials[ControlOfMechanic.MaterialIndex].SetActive(false);

        ControlOfMechanic.Instruments[0].SetActive(true);
        ControlOfMechanic.Instruments[0].transform.position = ControlOfMechanic.Positions[0].transform.position;
        Debug.Log("Smelting");

    }
}
