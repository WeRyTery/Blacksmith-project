using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class E_OrderBookUI : MonoBehaviour
{
    [SerializeField] Canvas BookCanvas;
    [SerializeField] Canvas Orders_Canvas;

    [SerializeField] ScrollRect OrderMenu;
    [SerializeField] GameObject OrderButtonPrefab;

    private static int OrdersCount = 0;

    private void Awake()
    {
        E_EventBus.NewBookOrder += addNewOrderInBook;
        E_EventBus.LoadSavedData += loadExistingOrders;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform.CompareTag("Book"))
                {
                    BookCanvas.gameObject.SetActive(true);
                    Orders_Canvas.gameObject.SetActive(false); // To avoid UI object conflicts between canvases
                }
            }
        }
    }

    public void CloseOrdersBook()
    {
        BookCanvas.gameObject.SetActive(false);
        Orders_Canvas.gameObject.SetActive(true);
    }

    public void addNewOrderInBook()
    {
        GameObject newButton = Instantiate(OrderButtonPrefab, OrderMenu.content); // Adds order button inside our book, after clicking on it we should be able to see description of order itself

        RectTransform buttonRect = newButton.GetComponent<RectTransform>();
        float OrderButtonDeltaY = OrderButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        float newYButtonPos = (buttonRect.anchoredPosition.y) - ((OrderButtonDeltaY + (OrderButtonDeltaY / 5)) * (OrderMenu.content.childCount - 1)); // We take order button prefab size (hight) and multiply it by amount of children components in our OrderMenu.content, thus will result in spaces between our buttons. After that we decrease our button position by already calculated Y, that represents our space. I can be manually increased if you'll multiply sizeDelta of button on any number (bigger number == bigger space)

        buttonRect.anchoredPosition = new Vector3(buttonRect.anchoredPosition.x, newYButtonPos, 0); // Changes position of our button by adding space between them.

        newButton.GetComponent<E_OrderIndex>().SetOrderIndex(OrdersCount); // Sets unique index to each order button, so that we could distinguish them one from another
        OrdersCount++;

        newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Commision for a new " + E_OrderingLogic.finalOrderMaterial + " forged " + E_OrderingLogic.finalOrderWeaponType; // Changes text of our buttons
    }

    public void loadExistingOrders()
    {

        for (int i = 0; i < E_OrderingLogic.ordersList.Count; i++)
        {
            GameObject loadButton = Instantiate(OrderButtonPrefab, OrderMenu.content); // Adds order button inside our book, after clicking on it we should be able to see description of order itself

            RectTransform buttonRect = loadButton.GetComponent<RectTransform>();
            float OrderButtonDeltaY = OrderButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
            float newYButtonPos = (buttonRect.anchoredPosition.y) - ((OrderButtonDeltaY + (OrderButtonDeltaY / 5)) * (OrderMenu.content.childCount - 1)); // We take order button prefab size (hight) and multiply it by amount of children components in our OrderMenu.content, thus will result in spaces between our buttons. After that we decrease our button position by already calculated Y, that represents our space. I can be manually increased if you'll multiply sizeDelta of button on any number (bigger number == bigger space)

            buttonRect.anchoredPosition = new Vector3(buttonRect.anchoredPosition.x, newYButtonPos, 0); // Changes position of our button by adding space between them.

            loadButton.GetComponent<E_OrderIndex>().SetOrderIndex(OrdersCount); // Sets unique index to each order button, so that we could distinguish them one from another
            OrdersCount++;



            loadButton.GetComponentInChildren<TextMeshProUGUI>().text = "Commision for a new " + E_OrderingLogic.ordersList[i].material + " forged " + E_OrderingLogic.ordersList[i].weaponType; // Changes text of our buttons
        }
    }
}
