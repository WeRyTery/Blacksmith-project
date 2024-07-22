using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _gameManager;
    public void CloseInventoryPanel()
    {
        _gameManager.GetComponent<InventoryLoader>().enabled = false;
        _inventoryPanel.SetActive(false);
    }
}
