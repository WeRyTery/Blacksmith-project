using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SmeltingLogic : MonoBehaviour
{
    [SerializeField] private Button Put_StartSmelting;
    [SerializeField] private float TimeToSmeltMaterial;
    [SerializeField] private bool PutMaterialIn = false;
    [SerializeField] private GameObject SwordObject;
    [SerializeField] private GameObject MaterialObject;
    [SerializeField] private GameObject MeltedMaterialObject;


    public void StartEverything()
    {
        if (PutMaterialIn)
        {
            MaterialObject.SetActive(false);
            MeltedMaterialObject.SetActive(true);
            StartCoroutine("StartSmelting");
        }
        else
        {
            PutMaterialIn = true;
        }
    }
    IEnumerator StartSmelting()
    {
        yield return new WaitForSeconds(TimeToSmeltMaterial);
        MeltedMaterialObject.SetActive(false);
        SwordObject.SetActive(true);

    }
}
