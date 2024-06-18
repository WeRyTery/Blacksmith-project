using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class InventoryTest : MonoBehaviour
{
    private InventoryManager _inventory;

    void Start()
    {
        _inventory = GetComponent<InventoryManager>();

        // Add some items to the inventory
        _inventory.AddItem(new NewWeapon("melt", "One_Hand", 50));
        _inventory.AddItem(new Metals("Iron Ore", 5));
        _inventory.AddItem(new Handle("Wooden Handle", 2));

        // Print the inventory contents
        _inventory.PrintInventory();

        // Add more items to the inventory
        _inventory.AddItem(new Metals("Iron Ore", 7));
        _inventory.AddItem(new Handle("Wooden Handle", 10));

        // Print the inventory contents again
        _inventory.PrintInventory();

        // Remove some items from the inventory
        _inventory.RemoveItem(new Metals("Iron Ore", 3));

        // Print the inventory contents again
        _inventory.PrintInventory();
    }
}
