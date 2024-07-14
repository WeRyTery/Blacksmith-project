using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class SmithingCycle : MonoBehaviour
{
    [Header("UI")]
    public bool ButtonPressed;
    [Space] 

    [Header("Object stats")]

    [Space]

    [SerializeField] private int TypeOfObject;
    [SerializeField] private int SmeltingMaterial;
    [SerializeField] private int ObjectSharpening;
    [SerializeField] private int ObjectDamage;
    [SerializeField] private int ObjectMaterial;
    [Space]


    [Header("Display objects")]
    public GameObject[] InstrumentToDisplay = new GameObject[2];//0 = 1 level of instrument, 1 = 4 level of instrument
    public GameObject[] PositionsInstrumentToDisplay = new GameObject[3];//0 = Smelting, 1 = Smithing, 2 = Sharpening
    public GameObject MaterialToDisplay;


    [Header("Sword & scripts")]
    [SerializeField] private GameObject sword;
    public V_ToolMaker smithing;
    public ControlsForSharpening sharpening;
    [Space]


    [Header("Other")]
    public TestHolderSCriptForObjject ObjectStats;
    public SmeltingLogic SmeltingScript;
    public E_CameraManagment CameraStateScript;

    private void Start()
    {
        sharpening = sword.GetComponentInChildren<ControlsForSharpening>();
        smithing = gameObject.GetComponent<V_ToolMaker>();
    }



    private void Update()
    {
        if (CameraStateScript.IsSmelting == true && ButtonPressed)
        {
            InstrumentToDisplay[0] = ObjectStats.InstrumentObject[0];
            MaterialToDisplay = ObjectStats.MaterialObject;
            MaterialToDisplay.SetActive(true);
            TheSmeltingLogic();
            ButtonPressed = false;
        }
        else if (CameraStateScript.IsSharpening == true && ButtonPressed)
        {
            TheSharpeningLogic();
            ButtonPressed = false;
        }
        else if (CameraStateScript.IsSmiting == true && ButtonPressed)
        {
            TheSmitingLogic();
            ButtonPressed = false;
        }
        else
        {
            
        }
    }
    private void TheSmeltingLogic()
    {
        SmeltingScript.SmeltingStart();
        
    }
    private void TheSmitingLogic()
    {
        smithing.enabled = true;
    }
    private void TheSharpeningLogic()
    {
        sharpening.enabled = true;

    }
    public void PutObject()
    {
        ButtonPressed = true;
    }
}
