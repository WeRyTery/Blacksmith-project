using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class V_TheGameMechanicScript : MonoBehaviour
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
    private bool ButtonDown = false;
    private float StartHoldTime;
    private float PowerOfClick;
    [Space]
    [Header("Damege Settings")] 
    [SerializeField] private int DamageOverAll;
    [SerializeField] private int[] DamagePerPower;
    [Space]
    [Header("CoolDown settings")]
    [SerializeField] private bool AlreadyClicked = false;
    [SerializeField] private float TimeToWait;
    private void Start()
    {
        Models[0].active = true;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && AlreadyClicked == false)
        {
            ButtonDown = true;
            StartHoldTime = Time.time;
            StartCoroutine("ClickHoldTime");
        }
        else if(Input.GetMouseButtonUp(0) && AlreadyClicked == false)
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
        if (PowerOfClick <= TimeForClickPower[0]) { 
            ClicksMade += ClicksAmountPerPower[0]; }

        else if (PowerOfClick <= TimeForClickPower[1]) {
            ClicksMade += ClicksAmountPerPower[1]; }

        else if (PowerOfClick <= TimeForClickPower[2]) {
            ClicksMade += ClicksAmountPerPower[2]; }

        else if (PowerOfClick <= TimeForClickPower[3]) {
            ClicksMade += ClicksAmountPerPower[3]; }

        else if (PowerOfClick <= TimeForClickPower[4]) {
            ClicksMade += ClicksAmountPerPower[4]; }
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
        while(ButtonDown == true)
        {
            PowerOfClick = Time.time - StartHoldTime;
            yield return null;
        }
    }
    IEnumerator ClickCoolDown()
    {
        yield return new WaitForSeconds(TimeToWait);
        AlreadyClicked = false;
    }
}

