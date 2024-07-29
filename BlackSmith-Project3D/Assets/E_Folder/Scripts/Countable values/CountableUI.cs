using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountableUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MoneyAmountTXT;
    [SerializeField] TextMeshProUGUI ReputationAmountTXT;

    private void Start()
    {
        E_EventBus.UpdateMoneyUI += UpdateMoneyUI;
        E_EventBus.UpdateReputationUI += UpdateReputationUI;

        UpdateReputationUI();
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        MoneyAmountTXT.text = "Gold: " + Money.GetCurrentMoney().ToString(); 
    }

    public void UpdateReputationUI()
    {
        ReputationAmountTXT.text = "Rep: " + Reputation.GetCurrentReputation().ToString();
    }
}
