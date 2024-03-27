using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class E_OrderDescriptionIndex : MonoBehaviour
{
    private TextMeshProUGUI OrdersDescriptionText;

    private int orderID = 0;

    private string _weaponType;
    private string _material;
    private int _budget;

    private void Start()
    {
        OrdersDescriptionText = GameObject.FindGameObjectWithTag("OrderDescriptionText").GetComponent<TextMeshProUGUI>();
    }

    public int GetOrderIndex()
    {
        return orderID;
    }

    public void SetOrderIndex(int Index)
    {
        orderID = Index;
    }

    public void ShowOrderDetails()
    {
        _weaponType = E_OrderingLogic.ordersList[gameObject.GetComponent<E_OrderDescriptionIndex>().GetOrderIndex()].weaponType; // Getting all variables from our List, that was created in E_OrderingLogic
        _material = E_OrderingLogic.ordersList[gameObject.GetComponent<E_OrderDescriptionIndex>().GetOrderIndex()].material;
        _budget = E_OrderingLogic.ordersList[gameObject.GetComponent<E_OrderDescriptionIndex>().GetOrderIndex()].budget;

        OrdersDescriptionText.text = _weaponType + "\n" + _material + "\n" + _budget;
    }
}
