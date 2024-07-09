using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
    [Header("Other")]

    public TestHolderSCriptForObjject ObjectStats;
    public SmeltingLogic SmeltingScript;
    public E_CameraManagment CameraStateScript;

    private void Update()
    {
        if (CameraStateScript.IsSmelting == true && ButtonPressed)
        {
            InstrumentToDisplay[0] = ObjectStats.InstrumentObject[0];
            MaterialToDisplay = ObjectStats.MaterialObject;
            MaterialToDisplay.SetActive(true);
            //TheSmeltingLogic();
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
        SmeltingScript.StartSmelting();
        
    }
    private void TheSmitingLogic()
    {
        Debug.Log("Smiting");
    }
    private void TheSharpeningLogic()
    {
        Debug.Log("Sharpening");
    }
    public void PutObject()
    {
        ButtonPressed = true;
    }
}
