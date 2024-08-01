using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.SubpixelMorphologicalAntialiasing;

public class InventoryManager : MonoBehaviour
{
    // announcing inventory settings

    [SerializeField] private int _maxStackSize = 10;
    [SerializeField] private int _maxSlots = 5;
    private int _temporaryIndexWeapons = 0;
    private int _temporaryIndexMetals = 0;
    private int _temporaryIndexHandles = 0;

    private List<NewWeapon> _newWeapons;
    private List<Metals> _metals;
    private List<Handle> _handles;

    private void Awake()
    {
        _newWeapons = new List<NewWeapon>(_maxSlots);
        _metals = new List<Metals>(_maxSlots);
        _handles = new List<Handle>(_maxSlots);
    }
    public void AddItem(ItemData item)
    {
        if (item is NewWeapon newWeapon) //adds blades and ready weapons
        {
            AddWeapon(newWeapon);
        }
        else if (item is Metals metal) //adds materials
        {
            AddStackableItem(metal, _metals);

        }
        else if (item is Handle handle) //adds handles
        {
            AddStackableItem(handle, _handles);

        }

    }
    public bool CheckForSpaceInInventory()
    {
        int emptySlotIndex = CheckForEmptySlot(_newWeapons);
        if (_newWeapons.Count >= _maxSlots)
        {
            Debug.Log(" NO Space");
            return false;
        }
        else
        {
            Debug.Log("Space");
            return true;
        }
    }
    private void AddWeapon(NewWeapon newWeapon)
    {
        int emptySlotIndex = CheckForEmptySlot(_newWeapons);
        if (_newWeapons.Count < _maxSlots)
        {
            _newWeapons.Add(newWeapon);
            newWeapon.Index = _temporaryIndexWeapons;
            _temporaryIndexWeapons++;
            Debug.Log("Weapon added: " + newWeapon.ItemName + " " + newWeapon.Material);

        }
        else if (emptySlotIndex != 10)
        {
            _newWeapons[emptySlotIndex] = newWeapon;
            _newWeapons[emptySlotIndex].Index = emptySlotIndex;
            Debug.Log("Weapon added: " + newWeapon.ItemName + " " + newWeapon.Material);

        }
        else
        {
            Debug.Log("No available slots for weapons");

        }
    }
    private void AddStackableItem<T>(T item, List<T> inventory) where T : ItemData
    {
        //adds a possible number of items to the stack

        foreach (var existingItem in inventory)
        {
            if (existingItem.ItemName == item.ItemName && ((dynamic)existingItem).quantity < _maxStackSize)
            {
                int addableQuantity = Mathf.Min(_maxStackSize - ((dynamic)existingItem).quantity, ((dynamic)item).quantity);
                ((dynamic)existingItem).quantity += addableQuantity;
                ((dynamic)item).quantity -= addableQuantity;
                Debug.Log("Added " + addableQuantity + " to existing stack of " + item.ItemName);

                if (((dynamic)item).quantity <= 0) return;
            }
        }

        //adds a number of stackable items to a new slot

        while (((dynamic)item).quantity > 0)
        {

            int emptySlotIndex = CheckForEmptySlot(inventory);

            if (inventory.Count < _maxSlots)
            {

                //adds a full new slot to inventory and decreases amount of items yet to add

                if (((dynamic)item).quantity > _maxStackSize)
                {
                    T newItem = (T)item.Clone();
                    ((dynamic)newItem).quantity = _maxStackSize;
                    inventory.Add(newItem);
                    CheckForItemTypeToAdd(newItem);
                    ((dynamic)item).quantity -= _maxStackSize;
                    Debug.Log("Added new stack of " + _maxStackSize + " " + item.ItemName);
                }

                //adds non-full new slot to inventory

                else
                {
                    T newItem = (T)item.Clone();
                    ((dynamic)newItem).quantity = ((dynamic)item).quantity;
                    inventory.Add(newItem);
                    CheckForItemTypeToAdd(newItem);
                    ((dynamic)item).quantity = 0;
                    CheckForItemTypeToAdd(item);

                    Debug.Log("Added new stack of " + ((dynamic)item).quantity + " " + item.ItemName);
                }
            }
            else if (emptySlotIndex != 10)
            {
                inventory[emptySlotIndex] = item;
                inventory[emptySlotIndex].Index = emptySlotIndex;
                Debug.Log("Added new stack of " + ((dynamic)item).quantity + " " + item.ItemName);
            }

            //no slots available behavior

            else
            {
                ((dynamic)item).quantity = 0;
                Debug.Log("No available slots for " + item.ItemName);
            }
        }
    }

    public void RemoveItem(ItemData item)
    {
        //removes a weapon

        if (item is NewWeapon newWeapon)
        {
            if (_newWeapons[newWeapon.Index] != null)
            {
                _newWeapons[newWeapon.Index] = null;
                Debug.Log("Weapon removed: " + newWeapon.ItemName);
            }
            else
            {
                Debug.Log("Weapon not found: " + newWeapon.ItemName);
            }
        }

        //removes materials

        else if (item is Metals material)
        {
            RemoveStackableItem(material, _metals);
        }

        //removes handles

        else if (item is Handle handle)
        {
            RemoveStackableItem(handle, _handles);
        }

        else
        {
            Debug.Log("Item not identified");
        }
    }

    private void RemoveStackableItem<T>(T item, List<T> inventory) where T : ItemData
    {
        //checking the amount of these items in inventory

        int totalAvailableToRemove = 0;
        foreach (var existingItem in inventory)
        {
            if (existingItem.ItemName == item.ItemName)
            {
                totalAvailableToRemove += ((dynamic)existingItem).quantity;
            }
        }
        if (totalAvailableToRemove >= ((dynamic)item).quantity)
        {
            foreach (var existingItem in inventory)
            {
                //finding out how much to remove by taking the smaller number
                int removableQuantity = Mathf.Min(((dynamic)existingItem).quantity, ((dynamic)item).quantity);
                ((dynamic)existingItem).quantity -= removableQuantity;
                ((dynamic)item).quantity -= removableQuantity;
                Debug.Log("Removed " + removableQuantity + " of " + item.ItemName);

                if (((dynamic)existingItem).quantity <= 0)
                {
                    inventory[existingItem.Index] = null;
                    Debug.Log(item.ItemName + " stack is empty and removed from _inventory");
                }

                if (((dynamic)item).quantity <= 0) return;
            }
        }

        Debug.Log(item.ItemName + " not found in _inventory");
    }

    private int CheckForEmptySlot<T>(List<T> inventory) where T : ItemData
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                return i;
            }
        }
        return 10;
    }
    private void CheckForItemTypeToAdd<T>(T item) where T : ItemData
    {
        if (item is Metals metal)
        {
            ((dynamic)item).Index = _temporaryIndexMetals;
            _temporaryIndexMetals++;
        }
        else if (item is Handle handle)
        {
            ((dynamic)item).Index = _temporaryIndexHandles;
            _temporaryIndexHandles++;
        }
    }
    public NewWeapon WeaponReadyCheck(NewWeapon readyWeapon)
    {
        foreach (var weapon in _newWeapons)
        {
            if (weapon != null)
            {
                if (weapon.ItemName == readyWeapon.ItemName && weapon.Stage == 4
                                // weapon.Material == readyWeapon.Material
                                )
                {
                    return weapon;
                }
            }
        }

        return null;
    }
    public NewWeapon WeaponSmithingCheck(NewWeapon readyWeapon)
    {
        foreach (var weapon in _newWeapons)
        {
            if (weapon != null)
            {
                if (weapon.ItemName == readyWeapon.ItemName
                                // weapon.Material == readyWeapon.Material
                                )
                {
                    return weapon;
                }
            }
        }

        return null;
    }
    public NewWeapon WeaponSharpeningCheck(NewWeapon readyWeapon)
    {
        foreach (var weapon in _newWeapons)
        {
            if (weapon != null)
            {
                if (weapon.Index == readyWeapon.Index
                                // weapon.Material == readyWeapon.Material
                                )
                {
                    return weapon;
                }
            }
        }

        return null;
    }
    public List<NewWeapon> GetWeaponsList()
    {
        return _newWeapons;
    }
    public List<Metals> GetMetalsList()
    {
        return _metals;
    }
    public List<Handle> GetHandleList()
    {
        return _handles;
    }
    public void PrintInventory()
    {
        Debug.Log("Weapons:");
        foreach (var newWeapon in _newWeapons)
        {
            if (newWeapon != null)
            {
                Debug.Log(newWeapon.ItemName + " - Damage: " + newWeapon.DamagedState + " - Number: " + newWeapon.Material + " " + newWeapon.Index + " Stage: " + newWeapon.Stage + " Sharpness: " + newWeapon.Sharpness);
            }
        }

        Debug.Log("Materials:");
        foreach (var material in _metals)
        {
            Debug.Log(material.ItemName + " - Quantity: " + material.quantity + " " + material.Index);
        }

        Debug.Log("Handles:");
        foreach (var handle in _handles)
        {
            Debug.Log(handle.ItemName + " - Quantity: " + handle.quantity + " " + handle.Index);
        }
    }

    public NewWeapon CreateNewWeapon()
    {
        return new NewWeapon("", "", 0, 0, 0, 0);
    }
    public int[] CountWeaponTypes(List<NewWeapon> weapons)
    {
        int countBronze = 0;
        int countSilver = 0;
        int countGold = 0;
        foreach (var weapon in weapons)
        {
            if (weapon != null)
            {
                if (weapon.Stage == 0)
                {
                    if (weapon.Material == "Bronze")
                    {
                        countBronze++;
                    }
                    else if (weapon.Material == "Silver")
                    {
                        countSilver++;
                    }
                    else if (weapon.Material == "Gold")
                    {
                        countGold++;
                    }
                }
            }
        }
        return new int[3] { countGold, countSilver, countBronze };
    }
    public string[] WriteStatsOfWeapon()
    {
        int i = 0;
        string[] Stats = new string[5];
        foreach (NewWeapon weapon in _newWeapons)
        {
            if (weapon != null)
            {
                if (weapon.Stage == 4)
                {

                    Stats[i] = $"Damage: {weapon.DamagedState} " +
                            $"Stage: {weapon.Stage} " +
                            $"Material: {weapon.Material} ";

                }
            }
            i++;
        }
        return Stats;
    }
}
