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
    [Space]
    [SerializeField] private GameObject SmithingCanvas;
    [SerializeField] private GameObject SmithingProgressButton;
    [SerializeField] private GameObject SmithingInventory;

    [Header("Scripts")]
    public V_ToolMaker smithing;
    public ControlsForSharpening sharpening;
    public SmeltingLogic Smelting;
    public E_CameraManagment CameraStateScript;
    [Header("PlayerInput")]
    [SerializeField] private bool PlayerChosedMaterial = false;
    [SerializeField] private bool PlayerStarted = false;
    [Header("IndexsForObjects")]
    public int MaterialIndex;
    [Header("PositionsForObjects")]
    public GameObject[] Positions = new GameObject[3];

    [Header("ObjectsForDisplay")]
    public GameObject MainSword;
    public GameObject MaterialGameObject;
    public GameObject[] Instruments = new GameObject[2];
    public Material[] MaterialsC = new Material[3];
    [Header("Inventory")]
    public InventoryLoader InventoryLoader;

    public InventoryManager InventoryManager;
    private void Start()
    {
        InventoryManager = InventoryLoader.GetInventory();
    }
    private void Update()
    {
        if(CameraStateScript.IsSmelting == true)
        {
            SmeltingCanvas.SetActive(true);
            if (Instruments[0].activeSelf)
            {
                SmeltingProgressButton.SetActive(true);
                PlayerStarted = true;
            }
            else if(PlayerChosedMaterial == true)
            { 
                SmeltingInventory.SetActive(false);
                SmeltingProgressButton.SetActive(true);
            }
            else
            {
                SmeltingInventory.SetActive(true);
            }
        }
        else if(CameraStateScript.IsSmiting == true)
        {
            SmithingCanvas.SetActive(true);
            if(PlayerChosedMaterial == true && PlayerStarted == false)
            { 
                SmithingInventory.SetActive(false);
                SmithingProgressButton.SetActive(true);
            }
            else if(PlayerStarted == true && smithing.CurrentLevelOfModel == 3)
            { 
                SmithingProgressButton.SetActive(true);
            }
            else if(PlayerStarted == true && smithing.CurrentLevelOfModel != 3)
            { 
                SmithingProgressButton.SetActive(false);
            }
            else
            {
                SmithingInventory.SetActive(true);

            }

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
        PlayerChosedMaterial = false;
        SmeltingProgressButton.SetActive(false);
        E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
        PlayerStarted = false;
    }
    public void ChoseSlotForStackble(int Index)
    {
        PlayerChosedMaterial = true;
        MaterialIndex = Index;
    } 
    public void ProgressButtonSmelting()
    {
        if(PlayerStarted == false)
        {
            Smelting.enabled = true;
            PlayerStarted = true;
            Smelting.SmeltingStart();
        }
        else
        {
            PlayerChosedMaterial = false;
            SmeltingProgressButton.SetActive(false);
            NewWeapon Sword = InventoryManager.CreateNewWeapon();
            switch (MaterialIndex) 
            {
                case 0:
                Sword.Material = "Bronze";
                    break;
                case 1:
                Sword.Material = "Silver";
                    break;
                case 2:
                Sword.Material = "Gold";
                    break;
            }

            Sword.ItemName = "Sword";

            InventoryManager.AddItem(Sword);

            Instruments[0].SetActive(false);
            E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
            PlayerStarted = false;
            Smelting.enabled = false;
        }
    }
    public void ProgressButtonSmithing()
    {
        if(PlayerStarted == false)
        {
            MainSword.SetActive(true);
            MainSword.transform.position = Positions[1].transform.position;
            MainSword.transform.rotation = Quaternion.Euler(0,180,0);
            PlayerStarted = true;

            smithing.enabled = true;
        }
        else
        {
            PlayerChosedMaterial = false;
            SmithingProgressButton.SetActive(false);

            MainSword.SetActive(false);
            E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
            PlayerStarted = false;

            smithing.enabled = false;
        }
    }
}
