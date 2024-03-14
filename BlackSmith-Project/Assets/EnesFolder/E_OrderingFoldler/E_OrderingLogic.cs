using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class E_OrderingLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialoguesWindow;
    [SerializeField] TextMeshProUGUI ordersBook;

    private int dialogueTypeIndexHolder;
    private int dialogueIndexHolder;
    private string finalDialogueText;

    private int weaponTypeIndexHolder;
    private int[] midWeaponBudgetRange = { 100, 400 };
    private int[] lightWeaponBudgetRange = { 50, 150 };
    private int finalOrderBudget;


    private string[] normalDialogue =
    {
        "Hello, I need a new weapon. As you can see I'm unarmed right now, guess why... Here's short description of what I need",
        "Hi, you are a blacksmith, right? I need you to forge me a new weapon, I'll pay, of course. Just let me write down about the details",
        "Hello, can you make me a new weapon please? I forgot where I left my previous one after getting drunk. Anyways, here's the description of my desired new weapon"
    };

    private string[] elegantDialogue =
    {
        "Hello dear craftsman. I was wandering around those places, and stumbled across your humble blacksmith.\nBy a faithful coincidence, it happens that I need a new weapon for my fellow student. I wrote down crucial information about the weapon, here take it.",
        "Greetings young blacksmith. I need a new weapon, and if vision of my is still sharp, I can see the makings of a true blacksmith in you. Why not trying to impress one by a work, that could only be described as an art",
        "Pleased to meet you, young craftsman. I will go straight to the point. I am no master of wielding any weapon, but books wont save me from a swing of a sword or an axe. With thus being said, I look for a weapon to begin with. Here, take this. Description conveys my general desires regarding the weapon"
    };

    private string[] cheekyDialogue =
    {
        "Hey brat. I need a weapon. You make it, right? Description is on the paper",
        "Hi. Your cheap booth doesn't inspire any confidence, but I'm in a rush, so make a favour and do your best and try to make at least something better than a scrap of metal. Some details I wrote on the paper, here.",
        "Hey, fella. Make a weapon for me. Description and your budget is on the paper, and for instance, I'm not going to pay any more than that."
    };

    private string[] weaponTypes = { "Sword", "Spear", "Battle Axe", "Dagger" };

    public void NewCustomerOrder() // Calls all functions needed to choose weapon type, budget and dialogue, after that transfers data into text window in the game scene
    {
        DialogueTypeChooser();
        OrderDescriptionChooser();

        dialoguesWindow.text = finalDialogueText;

        ordersBook.text = weaponTypes[weaponTypeIndexHolder] +
                        "\n\n" + finalOrderBudget + " coins";
    }

    private void DialogueTypeChooser() // Chooses dialogue that a customer will say
    {
        dialogueTypeIndexHolder = Random.Range(0, 3); // Randomly chooses between 3 (currently) types of dialogues
        dialogueIndexHolder = Random.Range(0, 3); // Randomly picks a dialogue from the type (normal, elegant, cheecky)

        switch (dialogueTypeIndexHolder) // Combines random types and dialogues into finalDialogue variable value
        {
            case 0:
                finalDialogueText = normalDialogue[dialogueIndexHolder];
                break;
            case 1:
                finalDialogueText = elegantDialogue[dialogueIndexHolder];
                break;
            case 2:
                finalDialogueText = cheekyDialogue[dialogueIndexHolder];
                break;
            default:
                Debug.Log("DialoguesTypeChooser Error has occured");
                finalDialogueText = "Error has occured";
                break;
        }
    }

    private void OrderDescriptionChooser()
    {
        weaponTypeIndexHolder = Random.Range(0, 4); // Randomly chooses weapon type

        if (weaponTypes[weaponTypeIndexHolder] == "Sword" || weaponTypes[weaponTypeIndexHolder] == "Battle Axe") // Sword and Battle Axe are currently considered as mid priced weapons, the price can be changed in upper variables
        {
            finalOrderBudget = Random.Range(midWeaponBudgetRange[0], midWeaponBudgetRange[1]); // Decides mid budget by using range of 2 values that should be written manualy in upper variables
        }
        else // Dagger and spear are consider as cheap weapons, their prices can be changed in upper variables
        {
            finalOrderBudget = Random.Range(lightWeaponBudgetRange[0], lightWeaponBudgetRange[1]); // Decides cheap budget by using range of 2 values that should be written manualy in upper variables
        }
    }
}
