using UnityEngine;
using TMPro;
using System.Data;

public class InventoryManager : MonoBehaviour
{
    public int gold; // int for how much gold player has
    public TMP_Text goldText; // reference to inventory's text representing current gold amount
    public InventorySlots[] itemSlots; // array to hold all the inventory slots
    public UseItem useItem; // reference to the useItem script
    public GameObject lootPrefab; // reference to game object to allow the script to make one
    public Transform player; // reference to the player's transform

    private void Start() // runs at the start of the game
    {
        foreach (var slot in itemSlots) // loops through each value in itemSlots array
        {
            slot.UpdateUI(); // updates each slot's UI
        }
    }

    private void OnEnable() // when game first begins and object activated
    {
        Loot.OnItemLooted += AddItem; // subs to loot
    }

    private void OnDisable() // when game ends or object deactivated
    {
        Loot.OnItemLooted -= AddItem; // unsubs to loot
    }

    private void AddItem(ItemSO itemSO, int quantity)
    // adds itemSO of amount quantity to the inventory
    {
        if (itemSO.isGold) // if picked item is gold
        {
            gold += quantity; // adds quantity picked up to gold amount
            goldText.text = gold.ToString(); // sets gold text to amount of gold currently
            return; // ends method
        }
        
        foreach (var slot in itemSlots) // loops through each slot
        {
            if(slot.itemSO == itemSO && slot.quantity < itemSO.stackSize)
            // if it finds a slot with the same item and not a full stack
            {
                int availableSpace = itemSO.stackSize - slot.quantity; // int for how much space is left in the stack
                int amountToAdd = Mathf.Min(availableSpace, quantity); // int for how much to add to the stack

                slot.quantity += amountToAdd; // adds the amount to be added to the slot's quantity
                quantity -= amountToAdd; //  reduces the item's quantity by the amount added to the slot

                slot.UpdateUI(); // updates the UI to reflect changes

                if (quantity <= 0) // if the quantity of the item is 0
                {
                    return; // exits the method
                }
            }
        }

        foreach (var slot in itemSlots) // loops through each value in the itemSlots array
        {
            if (slot.itemSO == null) // runs if the item slot is empty
            {
                int amountToAdd = Mathf.Min(itemSO.stackSize, quantity); // int for how much to add to the stack
                slot.itemSO = itemSO; // fills itemSO with new item data
                slot.quantity = quantity; // fills quantity with new item data
                slot.UpdateUI(); // updates the slots UI
                return; // terminates loop
            }
        }
        
        if (quantity > 0) // if theres still items left
        {
            DropLoot(itemSO, quantity); // drops loot
        }

    }

    public void DropItem(InventorySlots slot) // drops the item from the inventory
    {
        DropLoot(slot.itemSO, 1); // drops 1 of the item in the slot
        slot.quantity --; // reduces the slot item quantity by 1
        if (slot.quantity <= 0) // if there are no items left
        {
            slot.itemSO = null; // removes the item from the slot
        }
        slot.UpdateUI(); // updates the UI to reflect all changes
    }

    private void DropLoot(ItemSO itemSO, int quantity) // drops the items in the slot
    {
        Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>(); 
        // creates a new game object for the dropped item and saves it as loot
        loot.Initialise(itemSO, quantity); // initialises the loot item
    }

    public void UseItem(InventorySlots slot) // uses an item from one of the InventorySlots
    {
        if (slot.itemSO != null && slot.quantity > 0) // if there is an item to be used
        {
            useItem.ApplyItemEffects(slot.itemSO); // applies effects
            slot.quantity --; // reduces quantity
            if (slot.quantity <= 0) // if theres no more of the item left
            {
                slot.itemSO = null; // reomevs the item from the slot
            }
            slot.UpdateUI(); // updates the UI to reflect changes
        }
        

    } 

}
