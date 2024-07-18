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

        LoadItems();

        _slots = GetSlots();

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
    private void DrawInventory()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            // _slots[i].GetComponent<Image>().image;
            _slots[i].GetComponent<Image>().color = new Color(0, 0, 0, 100);
            _slots[i].GetComponent<Image>().sprite = _inventoryContainer.GetComponent<Image>().sprite;
        }
    }
}
