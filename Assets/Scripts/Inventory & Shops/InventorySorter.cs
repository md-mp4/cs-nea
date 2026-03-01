using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySorter : MonoBehaviour
{
    public InventoryManager inventoryManager; // reference to the manager that holds the actual data

    private void Update() // runs every frame
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        // checks if the p key was pressed this frame, then runs if true
        {
            SortByName(); // calls the method to sort the inventory
        }
    }

    public void SortByName() // sorts inventory by name alphabetically
    {
        InventorySlots[] slots = inventoryManager.itemSlots; // creates a reference to the array of slots

        for (int i = 0; i < slots.Length - 1; i++) // loops through the array
        {
            for (int j = 0; j < slots.Length - i - 1; j++) // nested loop to compare items
            {
                // if the current slot is empty and the next one has an item, move the item up
                if (slots[j].itemSO == null && slots[j + 1].itemSO != null)
                {
                    Swap(slots, j, j + 1); // swaps the empty slot with the item
                }
                // if both slots have items, check their names
                else if (slots[j].itemSO != null && slots[j + 1].itemSO != null)
                {
                    // compares names - if current is "higher" than next, swap them
                    if (string.Compare(slots[j].itemSO.itemName, slots[j + 1].itemSO.itemName) > 0)
                    {
                        Swap(slots, j, j + 1); // performs the swap
                    }
                }
            }
        }

        foreach (var slot in slots) // loops through each slot in the array
        {
            slot.UpdateUI(); // updates the UI to reflect the new order
        }
    }

    private void Swap(InventorySlots[] slots, int a, int b) // helper method to swap two slots
    {
        ItemSO tempSO = slots[a].itemSO; // temporarily holds the itemSO from the first slot
        int tempQty = slots[a].quantity; // temporarily holds the quantity from the first slot

        slots[a].itemSO = slots[b].itemSO; // moves the second slot's item into the first
        slots[a].quantity = slots[b].quantity; // moves the second slot's quantity into the first

        slots[b].itemSO = tempSO; // puts the temporary item back into the second slot
        slots[b].quantity = tempQty; // puts the temporary quantity back into the second slot
    }
}
