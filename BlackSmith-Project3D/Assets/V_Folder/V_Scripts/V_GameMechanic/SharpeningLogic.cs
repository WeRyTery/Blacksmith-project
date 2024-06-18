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
    public ToolStats DamageOnTool;
    [Header("Sharpening Settings")]
    [SerializeField] private int SharpenedAmount;
    [SerializeField] private int AmountForEachSharpeningCycle;
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Sharpening");
        if (!SharpeningStarted && SharpenedAmount < SharpenedAmountLimit)
        {
            SharpenedAmount += AmountForEachSharpeningCycle;
            SharpeningStarted = true;
            StartCoroutine("SharpeningCoolDown");
        }
        else if(SharpenedAmount >= SharpenedAmountLimit && !SharpeningStarted)
        {
            DamageOnTool.ToolDamage += SharpeningDamage;
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
