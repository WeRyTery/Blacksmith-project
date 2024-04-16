using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class E_OrdersDescription
{
    public string material;
    public string weaponType;
    public int budget;

    public E_OrdersDescription (string material, string weaponType, int budget)
    {
        this.material = material;
        this.weaponType = weaponType;
        this.budget = budget;
    }
}
