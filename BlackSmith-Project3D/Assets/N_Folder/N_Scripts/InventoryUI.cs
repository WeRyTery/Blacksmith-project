using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private GameObject _gameManager;

    public void CloseInventoryPanel()
    {
        _gameManager.GetComponent<InventoryLoader>().enabled = false;
        _inventoryPanel.SetActive(false);
    }
    
    public void OpenInventory()
    {
        _gameManager.GetComponent<InventoryLoader>().enabled = true;
        _inventoryPanel.SetActive(true);
    }
}
