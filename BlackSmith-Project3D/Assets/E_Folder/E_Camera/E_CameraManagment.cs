using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class E_CameraManagment : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] CinemachineVirtualCamera ReceptionRoomCamera;
    [SerializeField] CinemachineVirtualCamera SmitheryCamera;
    [SerializeField] Animator TransiotionAnimations;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "DoorToSmithery":
                StartCoroutine(SmitheringRoom());
                break;

            case "DoorToReceptionRoom":
                StartCoroutine(ReceptionRoom());
                break;

            default:
                Debug.Log("Error, room was not found");
                break;
        }

    }

    IEnumerator ReceptionRoom()
    {
        TransiotionAnimations.SetTrigger("EndAnim");

        yield return new WaitForSeconds(1);

        Player.transform.position = GameObject.FindGameObjectWithTag("TP_ReceptionRoom").transform.position;
        ReceptionRoomCamera.Priority = 100;
        SmitheryCamera.Priority = 0;

        TransiotionAnimations.SetTrigger("StartAnim");
    }
    IEnumerator SmitheringRoom()
    {
        TransiotionAnimations.SetTrigger("EndAnim");

        yield return new WaitForSeconds(1);

        Player.transform.position = GameObject.FindGameObjectWithTag("TP_Smithery").transform.position;
        SmitheryCamera.Priority = 100;
        ReceptionRoomCamera.Priority = 0;

        TransiotionAnimations.SetTrigger("StartAnim");
    }
}
