using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class E_OrderIndex : MonoBehaviour
{
    private TextMeshProUGUI Weapon;
    private TextMeshProUGUI Material;
    private TextMeshProUGUI Budget;

    public int orderID = 0;

    private string _weaponType;
    private string _material;
    private int _budget;

    private int _selectedOrderIndex;

    private void Start()
    {
        Weapon = GameObject.FindGameObjectWithTag("WeaponText").GetComponent<TextMeshProUGUI>();
        Material = GameObject.FindGameObjectWithTag("MaterialText").GetComponent<TextMeshProUGUI>();
        Budget = GameObject.FindGameObjectWithTag("BudgetText").GetComponent<TextMeshProUGUI>();
    }

    public int GetOrderIndex()
    {
        return orderID;
    }

    public void SetOrderIndex(int id)
    {
        orderID = id;
    }

    public void ShowOrderDetails()
    {
        _selectedOrderIndex = gameObject.GetComponent<E_OrderIndex>().GetOrderIndex();

        _weaponType = E_OrderingLogic.ordersList[_selectedOrderIndex].weaponType; // Getting all variables from our List, that was created in E_OrderingLogic
        _material = E_OrderingLogic.ordersList[_selectedOrderIndex].material;
        _budget = E_OrderingLogic.ordersList[_selectedOrderIndex].budget;

        Weapon.text = _weaponType;
        Material.text = _material;
        Budget.text = _budget.ToString();

        CurrentOrderSelected.currentIndex = _selectedOrderIndex; // We save selected orders index in another variable to be able manipulate our list from another scripts
    }
}
