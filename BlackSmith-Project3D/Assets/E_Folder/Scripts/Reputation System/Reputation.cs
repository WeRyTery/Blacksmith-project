using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;

public static class Reputation
{
    private static int CurrentReputation;

    public static int GetCurrentReputation()
    {
        return CurrentReputation;
    }

    public static void AddReputation(int AmountToAdd)
    {
        CurrentReputation += AmountToAdd;
    }

    public static void SubtractReputation(int AmountToSubtract)
    {
        CurrentReputation -= AmountToSubtract;
    }

    public static bool IsReputationEnough(int GoalReputation)
    {
        if (GoalReputation > CurrentReputation)
        {
            return false;
        }
        else
        {
            return true;
        } 
    }
}
