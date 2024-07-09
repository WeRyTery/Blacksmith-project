using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHolderSCriptForObjject : MonoBehaviour
{
    [Header("Object stats")]
    public int TypeOfObject = 0;
    public int SmeltingMaterial = 0;
    public int ObjectSharpening = 0;
    public int ObjectDamage = 0;
    public int ObjectMaterial = 0;

    [Header("Object Visuals")]
    public GameObject MaterialObject;
    public GameObject[] InstrumentObject = new GameObject[4];
}
