using Cinemachine;
using Cinemachine.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class E_CameraManagment : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] CinemachineVirtualCamera OrderCounterCamera;
    [SerializeField] CinemachineVirtualCamera ReceptionRoomCamera;
    [SerializeField] CinemachineVirtualCamera SmitheryCamera;
    [SerializeField] Animator TransiotionAnimations;
    [SerializeField] Canvas TransitionCanvas; // Canvas that plays role transition role (animation imitation)

    private CinemachineBrain brain;

    private string TeleportPointTag;
    private bool DoWeNeedTransition;
    private bool DoWeChangeBlendMode; // Currently only changes to "Ease in Out" style from default "Cut"

    private RigidbodyConstraints defaultPlayerConstraints;

    private void Start()
    {
        IsTransitionCanvasEnabled(false);
        defaultPlayerConstraints = Player.GetComponent<Rigidbody>().constraints;
        brain = FindObjectOfType<CinemachineBrain>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ResetAllCamerasSettings();
        switch (other.tag)
        {
            case "OrderCounter":
                DoWeChangeBlendMode = true;
                StartCoroutine(ChangeCurrentCamera(OrderCounterCamera));
                break;

            case "DoorToSmithery":
                TeleportPointTag = "TP_Smithery";
                DoWeNeedTransition = true;
                StartCoroutine(ChangeCurrentCamera(SmitheryCamera));
                break;

            case "DoorToReceptionRoom":
                TeleportPointTag = "TP_ReceptionRoom";
                DoWeNeedTransition = true;
                StartCoroutine(ChangeCurrentCamera(ReceptionRoomCamera));
                break;

            default:
                Debug.Log("Error, room was not found");
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetAllCamerasSettings();
        switch (other.tag)
        {
            case "OrderCounter":
                DoWeChangeBlendMode = true;
                StartCoroutine(ChangeCurrentCamera(ReceptionRoomCamera));
                break;
        }
    }

    IEnumerator ChangeCurrentCamera(CinemachineVirtualCamera cameraToChange)
    {
        switch (DoWeChangeBlendMode)
        {
            case true:
                brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2f);
                break;
        }

        switch (DoWeNeedTransition)
        {
            case true:
                IsTransitionCanvasEnabled(true);

                Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                TransiotionAnimations.SetTrigger("EndAnim");

                yield return new WaitForSeconds(1);

                Player.transform.position = GameObject.FindGameObjectWithTag(TeleportPointTag).transform.position;
                SetAllCamerasPriorityToZero();
                cameraToChange.Priority = 100;

                TransiotionAnimations.SetTrigger("StartAnim");

                yield return new WaitForSeconds(1);
                Player.GetComponent<Rigidbody>().constraints = defaultPlayerConstraints;

                IsTransitionCanvasEnabled(false);

                break;

            default:
                SetAllCamerasPriorityToZero();
                cameraToChange.Priority = 100;

                break;
        }
    }

    private void SetAllCamerasPriorityToZero()
    {
        CinemachineVirtualCamera[] cameras = FindObjectsOfType<CinemachineVirtualCamera>();

        foreach (CinemachineVirtualCamera camera in cameras)
        {
            camera.Priority = 0;
        }
    }

    private void ResetAllCamerasSettings()
    {
        // Sets our blend definition to default, AKA: "Cut" style
        brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0);
        DoWeChangeBlendMode = false;
        DoWeNeedTransition = false;
    }

    private void IsTransitionCanvasEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            TransitionCanvas.enabled = true;
        }
        else
        {
            TransitionCanvas.enabled = false;
        }
    }

    //Unnecessary part lower, check optimisation?

    /*    private void ChangeCameraBlendMode(CinemachineBlendDefinition.Style blendStyle, float e_blendingTime)
        {
            brain.m_DefaultBlend = new CinemachineBlendDefinition(blendStyle, e_blendingTime);
        }*/
}
