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

    [Header("Sharpening Settings")]
    [SerializeField] private int SharpenedAmount;
    [SerializeField] private int AmountForEachSharpeningCycle;

    public SmithingCycle ControlOfMechanic;


    private void OnTriggerStay(Collider other)
    {
        if (!SharpeningStarted && SharpenedAmount < SharpenedAmountLimit)
        {
            ControlOfMechanic.Sharpness += AmountForEachSharpeningCycle;
            SharpenedAmount += AmountForEachSharpeningCycle;
            SharpeningStarted = true;
            StartCoroutine("SharpeningCoolDown");
        }
        else if(SharpenedAmount >= SharpenedAmountLimit && !SharpeningStarted)
        {
            ControlOfMechanic.Damage++;
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
