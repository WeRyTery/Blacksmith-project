using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class V_ToolMaker : MonoBehaviour
{
    [Header("Model Settings")]
    [SerializeField] private GameObject SwordColliderHolder;
    [SerializeField] private GameObject[] Models;
    [SerializeField] private int[] LevelToChangeModel;
    public int CurrentLevelOfModel;
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
    public int DamageOverall;

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
    private bool wasClickOnSword;
    private BoxCollider swordCollider;
    public SmithingCycle smitingCycle;
    Renderer renderer;
    private void Start()
    {
        Models[0].SetActive(true);
        renderer = Models[0].gameObject.GetComponent<Renderer>();
        renderer.material = smitingCycle.MaterialsC[smitingCycle.MaterialIndex];

        swordCollider = SwordColliderHolder.GetComponent<BoxCollider>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, 100.0f); 
        

        if (Input.GetMouseButtonDown(0) && AlreadyClicked == false && hit.collider == swordCollider)
        {
            ButtonDown = true;
            StartHoldTime = Time.time;
            StartCoroutine("ClickHoldTime");
        }
        if (Input.GetMouseButtonUp(0) && AlreadyClicked == false && hit.collider == swordCollider)
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

                    DamageOverall += DamagePerPower[i];
                    break;
                }
                else
                {
                    DamageOverall += DamageAtFinish;
                    ClicksMade += ClicksIfDamaged;

                    DamageHasBeenMade = true;

                    break;
                }
            }
            if (!DamageHasBeenMade)
            {
            }

            DamageHasBeenMade = false;
        }

        
        else if (LevelToChangeModel.Length > CurrentLevelOfModel)
        {
            if(ClicksMade >= LevelToChangeModel[CurrentLevelOfModel])
            {
                Debug.Log("Checkpoint");
                Models[CurrentModel].SetActive(false);
                CurrentModel++;

                Models[CurrentModel].SetActive(true);
                CurrentLevelOfModel++;
                renderer = Models[CurrentModel].gameObject.GetComponent<Renderer>();
                renderer.material = smitingCycle.MaterialsC[smitingCycle.MaterialIndex];


                Debug.Log("Checkpoint2");
                Debug.Log("CheckPoint3");

                ClicksMade = 0;
                Debug.Log("Reseted");
            }
        }
        else
        {
            ClicksMade = 0;
            DamageOverall = 0;
            CurrentLevelOfModel = 0;
            CurrentModel = 0;
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
    private void OnDisable()
    {
        ButtonDown = false;
    }
}

