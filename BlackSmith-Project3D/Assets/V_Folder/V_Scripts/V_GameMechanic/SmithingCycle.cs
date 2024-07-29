using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using TMPro;
using System.Linq;

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
    [SerializeField] private TextMeshProUGUI[] TextAmounts = new TextMeshProUGUI[3];
    [Space]
    [SerializeField] private GameObject TextNoSpace;
    [Header("Other Variables")]
    [SerializeField] private float TimeForText;
    public bool SmelterStopSmelting = false;
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

        int[] WeaponTypes = new int[3];
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
                WeaponTypes = InventoryManager.CountWeaponTypes(InventoryManager.GetWeaponsList());
                TextAmounts[0].text = $"Amount: {WeaponTypes[0]}";
                TextAmounts[1].text = $"Amount: {WeaponTypes[1]}";
                TextAmounts[2].text = $"Amount: {WeaponTypes[2]}";
                SmithingInventory.SetActive(true);

            }

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
            if(SmelterStopSmelting == true || PlayerStarted == true)
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
                if (InventoryManager.CheckForSpaceInInventory() == true && SmelterStopSmelting == false)
                {
                    InventoryManager.AddItem(Sword);
                    Instruments[0].SetActive(false);
                    E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
                    PlayerStarted = false;
                    Smelting.enabled = false;
                    InventoryManager.PrintInventory();
                }
                else if(InventoryManager.CheckForSpaceInInventory() == false && SmelterStopSmelting == false)
                {
                    StartCoroutine("HoldDownTextNoSpace");
                    E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
                    PlayerStarted = false;
                    Smelting.enabled = false;
                    InventoryManager.PrintInventory();
                }
                else
                {
                    E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
                    PlayerStarted = false;
                    Smelting.enabled = false;
                }

            }
            else { }
        }
    }
    public void ProgressButtonSmithing()
    {
        NewWeapon WeaponToDestroy = InventoryManager.CreateNewWeapon();
        NewWeapon WeaponToAdd = InventoryManager.CreateNewWeapon();
        if (PlayerStarted == false)
        {
            if (MaterialIndex == 0)
            {
                WeaponToDestroy.Material = "Bronze";
            }
            else if (MaterialIndex == 1)
            {
                WeaponToDestroy.Material = "Silver";
            }
            else if (MaterialIndex == 2)
            {
                WeaponToDestroy.Material = "Gold";
            }
            WeaponToDestroy.ItemName = "Sword";
            WeaponToDestroy.Stage = 0;

            InventoryManager.PrintInventory();

            WeaponToDestroy = InventoryManager.WeaponSmithingCheck(WeaponToDestroy);

            Debug.Log(WeaponToDestroy.Material);
            InventoryManager.RemoveItem(WeaponToDestroy);
            InventoryManager.PrintInventory();

            MainSword.SetActive(true);
            MainSword.transform.position = Positions[1].transform.position;
            MainSword.transform.rotation = Quaternion.Euler(0,180,0);
            PlayerStarted = true;

            smithing.enabled = true;
        }
        else
        {
            if (MaterialIndex == 0)
            {
                WeaponToAdd.Material = "Bronze";
            }
            else if (MaterialIndex == 1)
            {
                WeaponToAdd.Material = "Silver";
            }
            else if (MaterialIndex == 2)
            {
                WeaponToAdd.Material = "Gold";
            }
            WeaponToAdd.ItemName = "Sword";


            WeaponToAdd.Stage = 4;

            PlayerChosedMaterial = false;
            SmithingProgressButton.SetActive(false);

            MainSword.SetActive(false);
            E_EventBus.ResetUXafterSmithingMechanic?.Invoke();
            PlayerStarted = false;

            WeaponToAdd.DamagedState = smithing.DamageOverall;


            smithing.enabled = false;
            InventoryManager.AddItem(WeaponToAdd);
            InventoryManager.PrintInventory();
        }
    }
    IEnumerator HoldDownTextNoSpace() 
    {
        TextNoSpace.SetActive(true);
        yield return new WaitForSeconds(TimeForText);
        TextNoSpace.SetActive(false);
    }
}
