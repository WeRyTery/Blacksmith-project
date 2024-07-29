using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SmeltingLogic : MonoBehaviour
{
    [SerializeField] GameObject FurnaceLights;
    
    Renderer renderer;
    [SerializeField] private float TimeToSmeltMaterial = 5f;

    public SmithingCycle ControlOfMechanic;

    public void SmeltingStart()
    {
        StartCoroutine(StartSmelting());
        FurnaceLights.SetActive(true);
    }

    IEnumerator StartSmelting()
    {
        ControlOfMechanic.MaterialGameObject.SetActive(true);
        renderer = ControlOfMechanic.MaterialGameObject.GetComponent<Renderer>();
        renderer.material = ControlOfMechanic.MaterialsC[ControlOfMechanic.MaterialIndex];
        ControlOfMechanic.SmelterStopSmelting = true;

        yield return new WaitForSeconds(TimeToSmeltMaterial);

        ControlOfMechanic.SmelterStopSmelting = false;
        FurnaceLights.SetActive(false);
        ControlOfMechanic.MaterialGameObject.SetActive(false);
        renderer = ControlOfMechanic.Instruments[0].GetComponent<Renderer>();
        renderer.material = ControlOfMechanic.MaterialsC[ControlOfMechanic.MaterialIndex];
        ControlOfMechanic.Instruments[0].SetActive(true);
        ControlOfMechanic.Instruments[0].transform.position = ControlOfMechanic.Positions[0].transform.position;
        Debug.Log("Smelting");

    }
}
