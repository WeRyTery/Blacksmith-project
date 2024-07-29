using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Money
{
    private static int CurrentMoney = 100;

    public static int GetCurrentMoney()
    {
        return CurrentMoney;
    }

    public static void AddMoney(int AmountToAdd)
    {
        CurrentMoney += AmountToAdd;
        UpdateUI();
    }

    public static void SubtractMoney(int AmountToSubtract)
    {
        CurrentMoney -= AmountToSubtract;
        UpdateUI();
    }

    public static bool IsMoneyEnough(int GoalReputation)
    {
        if (GoalReputation > CurrentMoney)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void UpdateUI()
    {
        E_EventBus.UpdateMoneyUI?.Invoke();
    }
}
