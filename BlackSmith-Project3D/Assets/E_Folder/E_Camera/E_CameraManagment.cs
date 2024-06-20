using Cinemachine;
using Cinemachine.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class E_CameraManagment : MonoBehaviour
{
    [Header("Room Cameras:")]
    public static GameObject Player;
    [SerializeField] CinemachineVirtualCamera OrderCounterCamera;
    [SerializeField] CinemachineVirtualCamera ReceptionRoomCamera;
    [SerializeField] CinemachineVirtualCamera SmitheryCamera;
    [Space]

    [Header("Smithing process cameras:")]
    [SerializeField] CinemachineVirtualCamera MeltingFurnaceCamera;
    [SerializeField] CinemachineVirtualCamera SmithingCamera;
    [SerializeField] CinemachineVirtualCamera SharpeningStoneCamera;
    [Space]

    [Header("Transition animation components:")]
    [SerializeField] Animator TransiotionAnimations;
    [SerializeField] Canvas TransitionCanvas; // Canvas that plays role transition role (animation imitation)

    private CinemachineBrain brain;

    private string TeleportPointTag;
    private bool DoWeNeedTransition; // Do we need Animation transition when going from one camera to another
    private bool DoWeChangeBlendMode; // Currently only changes to "Ease in Out" style from default "Cut"
    private bool DoWeFreezePlayerMovement;

    private bool PlayerInTriggerZone;
    private string CameraZoneTag;

    private RigidbodyConstraints defaultPlayerConstraints; // We need to freeze player rotation or otherwise we wont be able to move normally

    [SerializeField] string DefaultCullingMask = "Ignore reception room"; // Name of the culling mask that will be ignored in the main scene

    private string LayerMaskName; // Name of the mask that SHOULD be INGORED by main camera

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        IsTransitionCanvasEnabled(false);
        defaultPlayerConstraints = Player.GetComponent<Rigidbody>().constraints;
        brain = FindObjectOfType<CinemachineBrain>();

        ChangeCullingMask(DefaultCullingMask);
    }

    private void Update()
    {
        if (PlayerInTriggerZone && Input.GetKeyDown(KeyCode.E) && CameraZoneTag != "")
        {
            switch (CameraZoneTag)
            {
                case "OrderCounter":
                    EnableOrderCounterCamera();
                    break;

                case "DoorToSmithery":
                    EnableDoorToSmitheryCamera();
                    break;

                case "DoorToReceptionRoom":
                    EnableReceptionRoomCamera();
                    break;

                case "StartMelting":
                    EnableMeltingFurnaceCamera();
                    break;

                case "StartSmithing":
                    EnableSmithingCamera();
                    break;

                case "StartSharpening":
                    EnableSharpeningStoneCamera();
                    break;

                default:
                    Debug.Log("Error, room was not found");
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ResetAllCamerasSettings();
        PlayerInTriggerZone = true;
        CameraZoneTag = other.tag;
    }

    private void EnableOrderCounterCamera()
    {
        DoWeChangeBlendMode = true;
        StartCoroutine(ChangeCurrentCamera(OrderCounterCamera));
    }

    private void EnableReceptionRoomCamera()
    {
        TeleportPointTag = "TP_ReceptionRoom";
        DoWeNeedTransition = true;
        LayerMaskName = "Ignore reception room";
        StartCoroutine(ChangeCurrentCamera(ReceptionRoomCamera));

    }

    private void EnableDoorToSmitheryCamera()
    {
        TeleportPointTag = "TP_Smithery";
        DoWeNeedTransition = true;
        LayerMaskName = "Ignore smithing room";
        StartCoroutine(ChangeCurrentCamera(SmitheryCamera));
    }

    ////////////////////// SMITHING MECHANIC CAMERAS BELOW ///////////////////////
    
    private void EnableMeltingFurnaceCamera()
    {
        DoWeFreezePlayerMovement = true;
        StartCoroutine(ChangeCurrentCamera(MeltingFurnaceCamera));
        SubscribeAndCallEvents();
    }

    private void EnableSmithingCamera()
    {
        DoWeFreezePlayerMovement = true;
        StartCoroutine(ChangeCurrentCamera(SmithingCamera));
        SubscribeAndCallEvents();
    }

    private void EnableSharpeningStoneCamera()
    {
        DoWeFreezePlayerMovement = true;
        StartCoroutine(ChangeCurrentCamera(SharpeningStoneCamera));
        SubscribeAndCallEvents();
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
        switch (DoWeFreezePlayerMovement)
        {
            case true:
                Player.GetComponent<FirstPersonMovement>().enabled = false;
                break;
        }

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

                Vector3 CameraRotation = new Vector3(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.y);

                Player.transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.y);

                Player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                TransiotionAnimations.SetTrigger("EndAnim");

                yield return new WaitForSeconds(1);

                Player.transform.position = GameObject.FindGameObjectWithTag(TeleportPointTag).transform.position;
                SetAllCamerasPriorityToZero();
                cameraToChange.Priority = 100;

                TransiotionAnimations.SetTrigger("StartAnim");

                ChangeCullingMask(LayerMaskName);

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

    private void ChangeCullingMask(string LayerMaskName) // Changes culling mask of which object will be rendered on scene
    {
        if (LayerMaskName != "")
        {
            Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer(LayerMaskName)); // ~ = NOT, so we render everything EXCEPT value in LayerMaskName string
        }
        else
        {
            Camera.main.cullingMask = default; // Not sure this works
            Debug.Log("Culling mask was not set");
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

    public void SubscribeAndCallEvents()
    {
        E_EventBus.EnableSmithingMechanicUI?.Invoke();
        E_EventBus.ResetUXafterSmithingMechanic += SetCamerasBackAfterSmithingMechanic;
    }

    public void SetCamerasBackAfterSmithingMechanic()
    {
        Player.GetComponent<FirstPersonMovement>().enabled = true;
        StartCoroutine(ChangeCurrentCamera(SmitheryCamera));
    }
}
