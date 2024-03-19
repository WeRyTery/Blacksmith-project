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
    
    private int commonTextVariationsAmount = 7; // normal and cheeky dialogues variations
    private int rareTextVariationsAmount = 3; // elegant dialogues variations

    private int dialogueTypeIndexHolder;
    private int dialogueIndexHolder;
    private string finalDialogueText;

    private int weaponTypeIndexHolder;
    private int materialIndexHolder;
    private int[] midWeaponBudgetRange = { 100, 400 };
    private int[] lightWeaponBudgetRange = { 50, 150 };
    private int finalOrderBudget;


    private string[] normalDialogue =
    {
        "Hello, I need a new weapon. As you can see I'm unarmed right now, guess why... Here's short description of what I need",
        "Hi, you are a blacksmith, right? I need you to forge me a new weapon, I'll pay, of course. Just let me write down about the details",
        "Hello, can you make me a new weapon please? I forgot where I left my previous one after getting drunk. Anyways, here's the description of my desired new weapon",
        "Hey, I'm in the market for a new weapon. Mine's seen better days. Can you craft me something strong?",
        "Hi there! Need a new weapon. The old one's worn out. Can you help me out?",
        "Hey, I'm in dire need of a weapon. Lost mine in a bet. Could you whip something up for me?",
        "Hello! Looking for a new weapon. Mine's not cutting it anymore. Can you forge me something powerful?"
    };

    private string[] cheekyDialogue =
    {
        "Hey brat. I need a weapon. You make it, right? Description is on the paper",
        "Hi. Your cheap booth doesn't inspire any confidence, but I'm in a rush, so make a favour and do your best and try to make at least something better than a scrap of metal. Some details I wrote on the paper, here.",
        "Hey, fella. Make a weapon for me. Description and your budget is on the paper, and for instance, I'm not going to pay any more than that.",
        "Hey, hammer swinger! Need a weapon that doesn't crumble at the first swing. Got the specs right here, and don't try to swindle me for more than what's on this scrap of paper.",
        "Hey, you! Yeah, you, with the hammer. Need a weapon, and I'm not in the mood for games. Here's what I want. Don't make me regret it.",
        "Listen up, metal shaper. Need a weapon, and I've got better things to do than stand here all day. Here's what I want, and don't think I'll be too pleased if it's not up to scratch.",
        "Alright, forge-dweller. Need a weapon, and I've got places to be. Here's what I need, and don't think I'll be handing over a purse full of gold for anything less."
    };

    private string[] elegantDialogue =
    {
        "Hello dear craftsman. I was wandering around those places, and stumbled across your humble blacksmith.\nBy a faithful coincidence, it happens that I need a new weapon for my fellow student. I wrote down crucial information about the weapon, here take it.",
        "Greetings young blacksmith. I need a new weapon, and if vision of my is still sharp, I can see the makings of a true blacksmith in you. Why not trying to impress one by a work, that could only be described as an art",
        "Pleased to meet you, young craftsman. I will go straight to the point. I am no master of wielding any weapon, but books wont save me from a swing of a sword or an axe. With thus being said, I look for a weapon to begin with. Here, take this. Description conveys my general desires regarding the weapon"
    };

    private string[] weaponTypes = { "Sword", "Spear", "Battle Axe", "Dagger" };
    private string[] materials = { "Bronze", "Brass", "Iron", "Steel" }; //Brass - Î‡ÚÛÌ¸

    public void NewCustomerOrder() // Calls all functions needed to choose weapon type, budget and dialogue, after that transfers data into text window in the game scene
    {
        DialogueTypeChooser();
        OrderDescriptionChooser();
        UpdateUIText();
    }

    private void DialogueTypeChooser() // Chooses dialogue that a customer will say
    {
        dialogueTypeIndexHolder = Random.Range(0, 100); // Randomly chooses between 3 (currently) types of dialogues

        if (dialogueTypeIndexHolder <= 65) // —hance: 65% for normal dialogue
        {
            dialogueIndexHolder = Random.Range(0, commonTextVariationsAmount); // Chooses dialogue inside dialogue type array
            finalDialogueText = normalDialogue[dialogueIndexHolder];
        }
        else if (dialogueTypeIndexHolder > 65 && dialogueTypeIndexHolder <= 90) // —hance: 25% for 
        {
            dialogueIndexHolder = Random.Range(0, commonTextVariationsAmount);
            finalDialogueText = cheekyDialogue[dialogueIndexHolder];
        }
        else if (dialogueTypeIndexHolder > 90) // —hance: 10%
        {
            dialogueIndexHolder = Random.Range(0, rareTextVariationsAmount);
            finalDialogueText = elegantDialogue[dialogueIndexHolder];
        }

    }

    private void OrderDescriptionChooser()
    {
        weaponTypeIndexHolder = Random.Range(0, 4); // Randomly chooses weapon type
        materialIndexHolder = Random.Range(0, 4); // Randomly chooses material for weapon

        if (weaponTypes[weaponTypeIndexHolder] == "Sword" || weaponTypes[weaponTypeIndexHolder] == "Battle Axe") // Sword and Battle Axe are currently considered as mid priced weapons, the price can be changed in upper variables
        {
            finalOrderBudget = Random.Range(midWeaponBudgetRange[0], midWeaponBudgetRange[1]); // Decides mid budget by using range of 2 values that should be written manualy in upper variables
        }
        else // Dagger and spear are consider as cheap weapons, their prices can be changed in upper variables
        {
            finalOrderBudget = Random.Range(lightWeaponBudgetRange[0], lightWeaponBudgetRange[1]); // Decides cheap budget by using range of 2 values that should be written manualy in upper variables
        }

        switch (materials[materialIndexHolder])
        {
            case "Iron":
                finalOrderBudget += 20;
                break;
            case "Steel":
                finalOrderBudget += 30;
                break;
            case "Bronze":
                finalOrderBudget += 40;
                break;
            case "Brass":
                finalOrderBudget += 80;
                break;
        }
    }

    private void UpdateUIText()
    {
        dialoguesWindow.text = finalDialogueText;

        ordersBook.text = weaponTypes[weaponTypeIndexHolder] +
                        "\n\n" + materials[materialIndexHolder] +
                        "\n\n" + finalOrderBudget + " coins";
    }
}
