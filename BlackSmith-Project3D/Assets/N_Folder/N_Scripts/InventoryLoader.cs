using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryLoader : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryContainer;

    private List<GameObject> _slots = new List<GameObject>();
    private InventoryManager _inventory;

    private void Awake()
    {
        _inventory = GetComponent<InventoryManager>();

        _slots = GetSlots();
    }

    private void OnEnable()
    {
        Debug.Log("Inventory opened");

        LoadItems();

        DrawInventory();
    }
    private void LoadItems()
    {
        //should be loaded from .json file
    }
    private List<GameObject> GetSlots()
    {
        List<GameObject> slotsList = new List<GameObject>();
        for (int i = 0; i < _inventoryContainer.transform.childCount; i++)
        {
            slotsList.Add(_inventoryContainer.transform.GetChild(i).gameObject);
        }
        return slotsList;
    }
    public InventoryManager GetInventory()
    {
        return _inventory;
    }
    public void SetInventory(InventoryManager inventory)
    {
        _inventory = inventory;
    }
    private void DrawInventory()
    {
        //for (int i = 0; i < _slots.Count; i++)
        //{
        //    if (_slots[i] != null)
        //    {

        //    }
        //    // _slots[i].GetComponent<Image>().image;
        //    //_slots[i].GetComponent<Image>().color = new Color(0, 0, 0, 100);
        //    //_slots[i].GetComponent<Image>().sprite = _inventoryContainer.GetComponent<Image>().sprite;
        //}
        foreach (var weapon in _inventory.GetWeaponsList())
        {
               if (weapon != null)
            {
                _slots[weapon.Index].gameObject.SetActive(true);
            }
        }
        foreach (var metal in _inventory.GetMetalsList())
        {
            if (metal != null)
            {
                _slots[metal.Index + 5].gameObject.SetActive(true);
            }
        }
        foreach (var handle in _inventory.GetHandleList())
        {
            if (handle != null)
            {
                _slots[handle.Index + 10].gameObject.SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        Debug.Log("Inventory closed");
        //should save into a .json file
    }
}
