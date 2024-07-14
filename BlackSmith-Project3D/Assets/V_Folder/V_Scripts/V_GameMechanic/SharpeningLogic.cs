using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpeningLogic : MonoBehaviour
{
    [Header("CoolDown Settings")]
    [SerializeField] private bool SharpeningStarted;
    [SerializeField] private float SharpeningCoolDownTime;

    [Header("Damage")]
    [SerializeField] private int SharpeningDamage;
    [SerializeField] private int SharpenedAmountLimit;
    private ToolStats WeaponStats;

    [Header("Sharpening Settings")]
    [SerializeField] private int SharpenedAmount;
    [SerializeField] private int AmountForEachSharpeningCycle;

    private void Start()
    {
        WeaponStats = GameObject.FindGameObjectWithTag("MechanicManager").GetComponent<ToolStats>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!SharpeningStarted && SharpenedAmount < SharpenedAmountLimit)
        {
            SharpenedAmount += AmountForEachSharpeningCycle;
            WeaponStats.SharpnessOfATool += AmountForEachSharpeningCycle;
            SharpeningStarted = true;
            StartCoroutine("SharpeningCoolDown");
        }
        else if(SharpenedAmount >= SharpenedAmountLimit && !SharpeningStarted)
        {
            WeaponStats.DamageOfATool += SharpeningDamage;
            SharpeningStarted = true;
            StartCoroutine("SharpeningCoolDown");
        }
    }

    IEnumerator SharpeningCoolDown()
    {
        yield return new WaitForSeconds(SharpeningCoolDownTime);
        SharpeningStarted = false;
    }
}
