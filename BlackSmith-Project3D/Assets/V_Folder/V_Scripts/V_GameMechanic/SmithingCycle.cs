using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class SmithingCycle : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject SmeltingCanvas;
    [SerializeField] private GameObject SmeltingProgressButton;
    [SerializeField] private GameObject SmeltingInventory;

    [Header("Scripts")]
    public V_ToolMaker smithing;
    public ControlsForSharpening sharpening;
    public SmeltingLogic SmeltingScript;
    public E_CameraManagment CameraStateScript;
    [Header("PlayerInput")]
    [SerializeField] private bool PlayerChosedMaterial = false;
    [SerializeField] private bool PlayerStartedSmelting = false;
    [Header("IndexsForObjects")]
    public int MaterialIndex;
    [Header("PositionsForObjects")]
    public GameObject[] Positions = new GameObject[3];

    [Header("ObjectsForDisplay")]
    public GameObject[] Materials = new GameObject[3];
    public GameObject[] Instruments = new GameObject[2];

    private void Update()
    {
        if(CameraStateScript.IsSmelting == true)
        {
            SmeltingCanvas.SetActive(true);

            if(PlayerChosedMaterial == true)
            { 
                SmeltingInventory.SetActive(false);
                SmeltingProgressButton.SetActive(true);
            }
            else
            {
                SmeltingInventory.SetActive(true);
            }
        }
    }


    private void TheSmeltingLogic(bool startOrStop) // true == start, false == stop
    {

        if (startOrStop)
        {
            
            
        }
        else
        {
           
            SmeltingScript.enabled = false;
        }
    }

    private void TheSmitingLogic(bool startOrStop)
    {

        if (startOrStop)
        {
            
            smithing.enabled = true;
        }
        else
        {
          
            smithing.enabled = false;
        }
    }

    private void TheSharpeningLogic(bool startOrStop)
    {

        if (startOrStop)
        {
           
            sharpening.enabled = true;
        }
        else
        {
            
            sharpening.enabled = false;
        }
    }

    public void Exit()
    {

    }
    public void ChoseSlotForSmelting(int Index)
    {
        PlayerChosedMaterial = true;
        MaterialIndex = Index;
    } 
    public void ProgressButtonSmelting()
    {
        if(PlayerStartedSmelting == false)
        {
            PlayerStartedSmelting = true;
            SmeltingScript.SmeltingStart();
        }
        else
        {
            PlayerChosedMaterial = false;
            SmeltingProgressButton.SetActive(false);
            Instruments[0].SetActive(false);
            E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
            PlayerStartedSmelting = false;
        }
    }

}
