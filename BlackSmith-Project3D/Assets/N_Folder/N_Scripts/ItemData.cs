using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData //base for creating an item
{
    public string ItemName;
    public string ItemType;

    public ItemData(string name, string type)
    {
        ItemName = name;
        ItemType = type;
    }

    public ItemData Clone() 
    { 
        return (ItemData)this.MemberwiseClone(); 
    }
}
public class NewWeapon : ItemData //item that's in process of forging 
{
    public float Damage;
    public float Sharpness;
    public string Material;

    public NewWeapon(string material, string name, float damage, float sharpness) : base(name, "NewWeapon")
    {
        Damage = damage;
        Material = material;
        Sharpness = sharpness;
    }
}

public class Metals : ItemData //materials
{
    public int quantity;

    public Metals(string name, int qty) : base(name, "Metals")
    {
        quantity = qty;
    }
}

public class Handle : ItemData //handles
{
    public int quantity;

    public Handle(string name, int qty) : base(name, "Handle")
    {
        quantity = qty;
    }
}
