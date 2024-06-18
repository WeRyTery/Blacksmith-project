using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltingLogic : MonoBehaviour
{
    [SerializeField] private float TimeOfWaiting;
    [SerializeField] private GameObject FirstModel; 
    [SerializeField] private GameObject ButtonNext; 
    private void OnTriggerEnter(Collider other)
    {
        ButtonNext.SetActive(false);
        StartCoroutine("SmeltingTime");
        other.gameObject.SetActive(false);
    }
    IEnumerator SmeltingTime()
    {
        yield return new WaitForSeconds(TimeOfWaiting);
        FirstModel.SetActive(true);
        ButtonNext.SetActive(true);

    }
}
