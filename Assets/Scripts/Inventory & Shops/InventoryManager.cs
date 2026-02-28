using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public int gold; // int for how much gold player has
    public TMP_Text goldText; // reference to inventory's text representing current gold amount
    public InventorySlots[] itemSlots; // array to hold all the inventory slots

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
        else // if its any other item
        {
            foreach (var slot in itemSlots) // loops through each value in the itemSlots array
            {
                if (slot.itemSO == null) // runs if the item slot is empty
                {
                    slot.itemSO = itemSO; // fills itemSO with new item data
                    slot.quantity = quantity; // fills quantity with new item data
                    slot.UpdateUI(); // updates the slots UI
                    return; // terminates loop
                }
            }
        }
    }

    public void UseItem(InventorySlots slot) // uses an item from one of the InventorySlots
    {
        
    } 

}
