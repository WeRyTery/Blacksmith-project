using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartOfMechanicCycle : MonoBehaviour
{
    [SerializeField] private Vector3 EndPositionForPlayer;
    [SerializeField] private GameObject NextButton;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject FirstSmithingCamera;
    [SerializeField] private GameObject MaterialObject;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = EndPositionForPlayer;
        other.gameObject.SetActive(false);
        MainCamera.SetActive(false);

        NextButton.SetActive(true);
        MaterialObject.SetActive(true);
        FirstSmithingCamera.SetActive(true);
    }
}
