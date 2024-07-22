using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData //base for creating an item
{
    public string ItemName;
    public string ItemType;
    public int Index;

    public ItemData(string name, string type, int index)
    {
        ItemName = name;
        ItemType = type;
        Index = index;
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

    public NewWeapon(string material, string name, float damage, float sharpness, int index) : base(name, "NewWeapon", index)
    {
        Damage = damage;
        Material = material;
        Sharpness = sharpness;
    }
}

public class Metals : ItemData //materials
{
    public int quantity;

    public Metals(string name, int qty, int index) : base(name, "Metals", index)
    {
        quantity = qty;
    }
}

public class Handle : ItemData //handles
{
    public int quantity;

    public Handle(string name, int qty, int index) : base(name, "Handle", index)
    {
        quantity = qty;
    }
}
