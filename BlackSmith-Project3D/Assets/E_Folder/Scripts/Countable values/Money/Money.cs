using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        UpdateUIAndSave();
    }

    public static void SubtractMoney(int AmountToSubtract)
    {
        CurrentMoney -= AmountToSubtract;
        UpdateUIAndSave();
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

    public static void LoadMoney(List<CountableDataPreset> countableDataPreset)
    {
        Debug.Log("Loading money: " + countableDataPreset.Last().Money);
        CurrentMoney = countableDataPreset[countableDataPreset.Count - 1].Money;
        UpdateUIAndSave() ;
    }

    public static void UpdateUIAndSave()
    {
        E_EventBus.UpdateMoneyUI?.Invoke();
        
        E_EventBus.SaveCountableValues?.Invoke();
    }
}
