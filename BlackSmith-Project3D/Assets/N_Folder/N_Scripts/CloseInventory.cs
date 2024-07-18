using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    public void CloseInventoryPanel()
    {
        _inventoryPanel.SetActive(false);
    }
}
