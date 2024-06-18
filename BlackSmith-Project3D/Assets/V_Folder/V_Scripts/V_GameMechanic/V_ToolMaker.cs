using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class V_ToolMaker : MonoBehaviour
{
    [Header("Model Settings")]
    [SerializeField] private GameObject[] Models;
    [SerializeField] private int[] LevelToChangeModel;
    [SerializeField] private int CurrentLevelOfModel;
    [SerializeField] private int CurrentModel;
    [Space]
    [Header("ClickSettings")]
    [SerializeField] private int ClicksMade;
    [SerializeField] private int[] ClicksAmountPerPower;
    [SerializeField] private float[] TimeForClickPower;
    [SerializeField] private int ClicksIfDamaged;
    private bool ButtonDown = false;
    private float StartHoldTime;
    private float PowerOfClick;
    [Space]

    [Header("Damege Settings")] 

    [SerializeField] private int[] DamagePerPower;
    [SerializeField] private float[] TimeToGetDamagePerPower;
    [SerializeField] private int DamageAtFinish;
    [SerializeField] private bool DamageHasBeenMade;

    [Space]
    [Header("CoolDown settings")]
    [SerializeField] private bool AlreadyClicked = false;
    [SerializeField] private float TimeToWait;

    [Space]
    [Header("other Scripts")]
    public ToolStats DamageOfAnyTool;
    private void Start()
    {
        Models[0].active = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && AlreadyClicked == false)
        {
            ButtonDown = true;
            StartHoldTime = Time.time;
            StartCoroutine("ClickHoldTime");
        }
        if (Input.GetMouseButtonUp(0) && AlreadyClicked == false)
        {
            ButtonDown = false;
            StopCoroutine("ClickHoldTime");
            Debug.Log(PowerOfClick);
            ReactionToClick();
            AlreadyClicked = true;
            StartCoroutine("ClickCoolDown");
        }
    }
    void ReactionToClick()
    {
        if (PowerOfClick < TimeToGetDamagePerPower[0])
        {
            for (int i = 0; i < TimeForClickPower.Length; i++)
            {
                if (PowerOfClick <= TimeForClickPower[i])
                {
                    Debug.Log("Works1");
                    ClicksMade += ClicksAmountPerPower[i];
                    break;

                }
            }
        }
        if (PowerOfClick >= TimeToGetDamagePerPower[0])
        {
            for (int i = 0; i < TimeToGetDamagePerPower.Length; i++)
            {
                if (PowerOfClick <= TimeToGetDamagePerPower[i])
                {
                    DamageOfAnyTool.ToolDamage += DamagePerPower[i];
                    break;
                }
                else
                {
                    DamageOfAnyTool.ToolDamage += DamageAtFinish;
                    ClicksMade += ClicksIfDamaged;
                    DamageHasBeenMade = true;

                    break;
                }
            }
        }

        if (CurrentLevelOfModel + 1 > LevelToChangeModel.Length) { }
        else if (ClicksMade >= LevelToChangeModel[CurrentLevelOfModel])
        {
            //Change Model-----------------
            Models[CurrentModel].active = false;
            CurrentModel++;
            Models[CurrentModel].active = true;
            //_____________________________
            CurrentLevelOfModel++;
            ClicksMade = 0;
        }
    }
    IEnumerator ClickHoldTime()
    {
        while (ButtonDown == true)
        {
            PowerOfClick = Time.time - StartHoldTime;
            Debug.Log(PowerOfClick);
            yield return null;
        }
    }
    IEnumerator ClickCoolDown()
    {
        yield return new WaitForSeconds(TimeToWait);
        AlreadyClicked = false;
    }
}

