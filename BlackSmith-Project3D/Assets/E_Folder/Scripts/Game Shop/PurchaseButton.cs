using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
    [SerializeField] 

    public int ObjectPrice;
    public bool IsMultiBuyable; // Can you buy multiple copy of this item
    public int MaxBuyableItems; // How much copys of an item you can buy

    private bool isButtonEnabled = true;
    private int currentItemsBought = 0;

    private Button buyButton;
    private TextMeshProUGUI NotEnoughMoneyText;

    public void Buy()
    {
        if (ObjectPrice < Money.GetCurrentMoney())
        {
            Money.SubtractMoney(ObjectPrice);

            switch (IsMultiBuyable)
            {
                case true:
                    if (currentItemsBought++ == MaxBuyableItems)
                    {
                        DisableButton();
                        currentItemsBought++;
                        break;
                    }

                    currentItemsBought++;
                    break;

                case false:
                    DisableButton();
                    break;
            }
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
