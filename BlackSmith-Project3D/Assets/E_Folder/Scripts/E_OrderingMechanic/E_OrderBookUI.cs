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

    // Scripts needed to finish order
    public WeaponRating weaponRatingScript;
    public E_OrderingLogic orderLogic;
    public InventoryManager inventoryManager;

    private float weaponDamageState = 0;
    private float weaponRating = 0;

    private void Awake()
    {
        E_EventBus.NewBookOrder += addNewOrderInBook;
        E_EventBus.LoadSavedData += loadExistingOrders;
    }

    private void Start()
    {
        weaponRatingScript = gameObject.GetComponent<WeaponRating>();
        orderLogic = gameObject.GetComponent<E_OrderingLogic>();
        inventoryManager = gameObject.GetComponent<InventoryManager>();
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



        if (OrderMenu.content.childCount > 0)
        {
            RectTransform scrollViewRect = OrderMenu.content.GetComponent<RectTransform>();

            float ButtonUsedSpace = OrderButtonPrefab.GetComponent<RectTransform>().sizeDelta.y * (OrderMenu.content.childCount - 3); // Don't change -3 in the end of equastion, it makes too much empty space at the end (-3 means 3 button size less space)

            if (scrollViewRect.anchoredPosition.y >= ButtonUsedSpace)
            {
                OrderMenu.content.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, ButtonUsedSpace, 0);
            }
            else if (scrollViewRect.anchoredPosition.y < 0)
            {
                OrderMenu.content.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
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
        float newYButtonPos = (buttonRect.anchoredPosition.y) - ((OrderButtonDeltaY + (OrderButtonDeltaY / 5)) * (OrderMenu.content.childCount - 2)); // We take order button prefab size (hight) and multiply it by amount of children components in our OrderMenu.content, thus will result in spaces between our buttons. After that we decrease our button position by already calculated Y, that represents our space. I can be manually increased if you'll multiply sizeDelta of button on any number (bigger number == bigger space)

        buttonRect.anchoredPosition = new Vector3(buttonRect.anchoredPosition.x, newYButtonPos, 0); // Changes position of our button by adding space between them.


        newButton.GetComponent<E_OrderIndex>().SetOrderIndex(E_OrderingLogic.ordersList.Count - 1); // Sets unique index to each order button, so that we could distinguish them one from another

        newButton.GetComponentInChildren<TextMeshProUGUI>().text = "Commision for a new " + E_OrderingLogic.finalOrderMaterial + " forged " + E_OrderingLogic.finalOrderWeaponType; // Changes text of our buttons
    }

    public void FinishCurrentOrder()
    {
        NewWeapon finishedWeapon = inventoryManager.CreateNewWeapon();

        finishedWeapon.ItemName = "Sword";
        finishedWeapon = inventoryManager.WeaponReadyCheck(finishedWeapon);

        if (finishedWeapon != null)
        {
            weaponDamageState = finishedWeapon.DamagedState;

        }
        else
        {
            Debug.Log("Clients order is unfinished");
            return;
        }

        int OrdersIndex = CurrentOrderSelected.currentIndex;
        bool DidWeRemoveOrder = false;

        weaponRating = weaponRatingScript.RateWeapon(weaponDamageState);

        Reputation.CheckWeaponRatingForReputation(weaponRating);
        Money.AddMoney(E_OrderingLogic.ordersList[OrdersIndex].budget);


        E_OrderIndex[] buttons = FindObjectsOfType<E_OrderIndex>();

        foreach (E_OrderIndex button in buttons)
        {
            if (button.GetOrderIndex() == OrdersIndex)
            {
                Debug.Log(button.GetOrderIndex());

                Destroy(button.gameObject);
                orderLogic.UpdateUIText();

                E_OrderingLogic.ordersList.RemoveAt(OrdersIndex);
                DidWeRemoveOrder = true;
                break;
            }
        }

        if (DidWeRemoveOrder)
        {
            foreach (E_OrderIndex button in buttons)
            {
                if (button.GetOrderIndex() > OrdersIndex)
                {
                    Debug.Log("Changed " + button.GetOrderIndex() + " to: " + (button.GetOrderIndex() - 1));
                    button.SetOrderIndex(button.GetOrderIndex() - 1);
                }
            }
        }

        FileHandler.SaveToJSON<E_OrdersDescription>(E_OrderingLogic.ordersList, "OrdersData.json");
    }



    public void loadExistingOrders()
    {

        for (int i = 0; i < E_OrderingLogic.ordersList.Count; i++)
        {
            GameObject loadButton = Instantiate(OrderButtonPrefab, OrderMenu.content); // Adds order button inside our book, after clicking on it we should be able to see description of order itself

            RectTransform buttonRect = loadButton.GetComponent<RectTransform>();
            float OrderButtonDeltaY = OrderButtonPrefab.GetComponent<RectTransform>().sizeDelta.y;
            float newYButtonPos = (buttonRect.anchoredPosition.y) - ((OrderButtonDeltaY + (OrderButtonDeltaY / 5)) * (OrderMenu.content.childCount - 2)); // We take order button prefab size (hight) and multiply it by amount of children components in our OrderMenu.content, thus will result in spaces between our buttons. After that we decrease our button position by already calculated Y, that represents our space. I can be manually increased if you'll multiply sizeDelta of button on any number (bigger number == bigger space)

            buttonRect.anchoredPosition = new Vector3(buttonRect.anchoredPosition.x, newYButtonPos, 0); // Changes position of our button by adding space between them.

            loadButton.GetComponent<E_OrderIndex>().SetOrderIndex(OrdersCount); // Sets unique index to each order button, so that we could distinguish them one from another
            OrdersCount++;


            loadButton.GetComponentInChildren<TextMeshProUGUI>().text = "Commision for a new " + E_OrderingLogic.ordersList[i].material + " forged " + E_OrderingLogic.ordersList[i].weaponType; // Changes text of our buttons
        }
    }
}
