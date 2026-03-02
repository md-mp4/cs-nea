using UnityEngine;
using TMPro;
using System.Data;
using System.IO;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public int gold; // int for how much gold player has
    public TMP_Text goldText; // reference to inventory's text representing current gold amount
    public InventorySlots[] itemSlots; // array to hold all the inventory slots
    public UseItem useItem; // reference to the useItem script
    public GameObject lootPrefab; // reference to game object to allow the script to make one
    public Transform player; // reference to the player's transform

    public ItemSO[] itemDatabase; // reference to ItemSO so they can all be saved

    private void Start() // runs at the start of the game
    {
        foreach (var slot in itemSlots) // loops through each value in itemSlots array
        {
            slot.UpdateUI(); // updates each slot's UI
        }
    }

    private void Update() // runs every frame to check for key presses
    {
        var keyboard = Keyboard.current; // getting a reference to the keyboard
    
        if (keyboard == null) return; // if no keyboard is plugged in doesn't run this
        
        if (keyboard.leftCtrlKey.isPressed && keyboard.hKey.wasPressedThisFrame)
        // checks if the left Ctrl key is held down and if the h key was pressed this frame
        {
            SaveGame(); // saves the game
        }

        if (keyboard.leftCtrlKey.isPressed && keyboard.jKey.wasPressedThisFrame)
        // checks if the left Ctrl key is held down and if the j key was pressed this frame
        {
            LoadGame(); // loads the last save file
        }
    }

    public void SaveGame() // method for saving the game
    {
        InventorySaveData data = new InventorySaveData(); // creating a new save data object to fill up
        data.goldAmount = gold; // grabbing the current gold total

        // save the coordinates from the player transform for later
        data.pX = player.position.x;
        data.pY = player.position.y;
        data.pZ = player.position.z;

        foreach (var slot in itemSlots) // looping through every slot to see what needs saving
        {
            if (slot.itemSO != null) // only saving the slot if it's not empty, otherwise it's a waste of time
            {
                SlotSaveData slotData = new SlotSaveData();
                slotData.itemName = slot.itemSO.itemName; // getting the name string
                slotData.quantity = slot.quantity; // getting the amount
                data.savedSlots.Add(slotData); // adding this slot to our list
            }
        }

        string json = JsonUtility.ToJson(data, true); // turning all the data into a JSON string thats easy to read
        File.WriteAllText(Application.persistentDataPath + "/save.json", json); // writing that string into a file on the computer
        
        Debug.Log("Game saved. File found at: " + Application.persistentDataPath); // debug log to make sure the code worked
    }

    public void LoadGame() // loads the game 
    {
        string path = Application.persistentDataPath + "/save.json"; // the path to pull the file from
        
        if (File.Exists(path)) // checking if the file actually exists so the game doesn't crash if its not there
        {
            string json = File.ReadAllText(path); // reading the text from the file and turning it back into a data object
            InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json); // takes inventory save data from JSON file

            if (player != null) // if there is a player
            {
                player.position = new Vector3(data.pX, data.pY, data.pZ); // sends the player to the saved position
            }
            
            gold = data.goldAmount; // setting the gold back to what it was
            if(goldText != null) goldText.text = gold.ToString(); // updating the UI text

            foreach (var slot in itemSlots) // clearing out the current inventory so the player doesn't get double items
            { 
                slot.itemSO = null; slot.quantity = 0; // clears the slot
            }

            // linear search to find the items by name
            for (int i = 0; i < data.savedSlots.Count; i++) // loop for when i is less than the count as i increments
            {
                foreach (ItemSO so in itemDatabase) // checking item database array one by one for a name match
                {
                    if (so.itemName == data.savedSlots[i].itemName) // if the name in the save matches the name in the database
                    {
                        itemSlots[i].itemSO = so; // put the item back in the slot
                        itemSlots[i].quantity = data.savedSlots[i].quantity; // set the amount
                        break; // found so it can stop looking and move to the next one
                    }
                }
            }
            
            foreach (var slot in itemSlots) // refreshing every slot's UI to reflect changes
            { 
                slot.UpdateUI(); // refreshing slot's UI to reflect changes
            }
            
            Debug.Log("Everything loaded back in properly."); // debug code to make sure the code actually worked
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

[System.Serializable]
public class SlotSaveData // saves item data from slots
{
    public string itemName; // takes the item name
    public int quantity; // takes the item quantity
}

[System.Serializable]
public class InventorySaveData // saves whole inventory data
{
    public int goldAmount; // takes the amount of gold in inventory
    public List<SlotSaveData> savedSlots = new List<SlotSaveData>();
    // takes the inventory slots and saves them to a list

    public float pX; // saves player x position
    public float pY; // saves player y position
    public float pZ; // saves player z position
}