using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField]

    public string itemName; // Bronze, Silver, Gold | only 3 options | 
    public int ObjectPrice;

    private bool isButtonEnabled = true;
    private int currentItemsBought = 0;

    private Button buyButton;
    private TextMeshProUGUI NotEnoughMoneyText;

    public InventoryLoader inventoryLoader;
    public InventoryManager inventoryManager;

    private Metals metalToSell;

    private void Start()
    {
        inventoryLoader = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InventoryLoader>();
        inventoryManager = inventoryLoader.GetInventory();

        if (itemName == "Bronze" || itemName == "Silver" || itemName == "Gold")
        {
            metalToSell = new Metals(itemName, 4, 10);
        }

    }

    public void Buy()
    {
        if (Money.GetCurrentMoney() >= ObjectPrice)
        {
            Money.SubtractMoney(ObjectPrice);

            inventoryManager.AddItem(metalToSell);
            inventoryManager.PrintInventory();
            return;
        }

        NotEnoughMoney();
    }

    private void DisableButton()
    {
        isButtonEnabled = false;
        gameObject.SetActive(false);
    }

    private void NotEnoughMoney()
    {
        buyButton = GetComponentInChildren<Button>();

        NotEnoughMoneyText = GameObject.FindGameObjectWithTag("NotEnoughMoney").GetComponentInChildren<TextMeshProUGUI>();

        StartCoroutine(TemporaryTextDisplay());
    }

    IEnumerator TemporaryTextDisplay()
    {
        buyButton.interactable = false;
        NotEnoughMoneyText.enabled = true;


        yield return new WaitForSeconds(2);

        buyButton.interactable = true;
        NotEnoughMoneyText.enabled = false;
    }
}
