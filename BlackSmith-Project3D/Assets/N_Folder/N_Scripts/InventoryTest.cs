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
        NewWeapon weapon = new NewWeapon("1", "One_Hand", 1, 50, 100, 10);
        _inventory.AddItem(new NewWeapon("bronze", "Sword", 4, 20, 15, 12));
        _inventory.AddItem(weapon);
        _inventory.AddItem(new NewWeapon("2", "One_Hand", 1, 50, 100, 10));
        _inventory.AddItem(new NewWeapon("3", "One_Hand", 1, 50, 100, 10));
        _inventory.AddItem(new NewWeapon("4", "One_Hand", 1, 50, 100, 10));
        _inventory.AddItem(new NewWeapon("5", "One_Hand", 1, 50, 100, 10));
        _inventory.AddItem(new Metals("Iron Ore", 5, 10));
        _inventory.AddItem(new Handle("Wooden Handle", 2, 10));

        // Print the inventory contents
        _inventory.PrintInventory();

        // Add more items to the inventory
        _inventory.AddItem(new Metals("Iron Ore", 7, 10));
        _inventory.AddItem(new Handle("Wooden Handle", 10, 10));

        // Print the inventory contents again
        _inventory.PrintInventory();

        // Remove some items from the inventory
        _inventory.RemoveItem(new Metals("Iron Ore", 3, 10));
        _inventory.RemoveItem(weapon);
        _inventory.AddItem(new NewWeapon("6", "One_Hand", 1, 50, 100, 10));

        // Print the inventory contents again
        _inventory.PrintInventory();
    }
}
