using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Money
{
    private static int CurrentMoney = 100;

    public static int GetCurrentMoney()
    {
        return CurrentMoney;
    }

    public static void AddRMoney(int AmountToAdd)
    {
        CurrentMoney += AmountToAdd;
    }

    public static void SubtractMoney(int AmountToSubtract)
    {
        CurrentMoney -= AmountToSubtract;
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
}
