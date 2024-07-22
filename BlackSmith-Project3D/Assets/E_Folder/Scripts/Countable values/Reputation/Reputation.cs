using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEngine;

public static class Reputation
{
    //Max gain rep - 100,     Max lose rep - 30
    // 1*: -HIGH rep;   2*: -rep;      3*: +low rep;       4*: +Norm rep;     5*: +HIGH rep;
    private static int minimalPossibleReputation = -300;

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
        if (CurrentReputation <= minimalPossibleReputation)
        {
            CurrentReputation = -300;
        }

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

    public static void CheckWeaponRatingForReputation(float WeaponStarRating)
    {
        switch (WeaponStarRating)
        {
            case 1:
                SubtractReputation(40);
                break;

            case 2:
                SubtractReputation(20);
                break;

            case 3:
                AddReputation(20);
                break;

            case 4:
                AddReputation(60);
                break;

            case 5:
                AddReputation(100);
                break;
            default:
                Debug.Log("Error, inserted star rating was invalid");
                break;
        }
    }
}
