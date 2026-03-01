using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour, IPointerClickHandler
{
    public ItemSO itemSO; // itemSO reference
    public Image itemImage; // reference to the item's image
    public TMP_Text quantityText; // reference to tmp text for item's quantity
    private InventoryManager inventoryManager; // reference to inventory manager
    
    public int quantity; // int for amount of the item

    private void Start() // runs on start
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
        // dictates inventoryManager as the one from InventoryManager by itself
    }

    public void OnPointerClick(PointerEventData eventData)
    // on click it sends eventData to the script to be used in the code below
    {
        if (quantity > 0) // if the player actually has an item
        {
            if (eventData.button == PointerEventData.InputButton.Left) // on left click
            {
                if (itemSO.currentHealth > 0 && StatsManager.Instance.currentHealth >= StatsManager.Instance.maxHealth)
                // if the item heals and the player already has full health
                {
                    return; // stops the item being used
                }
                inventoryManager.UseItem(this); // uses the item in the slot
            }

            if (eventData.button == PointerEventData.InputButton.Right)  // on right click
            {
                inventoryManager.DropItem(this); // drops the item
            }
        }
    }

    public void UpdateUI() // updates the UI when called
    {
        if (itemSO != null) // only runs if there is an item 
        {
            itemImage.sprite = itemSO.icon; // updates the slot image to the item's
            itemImage.gameObject.SetActive(true); // activates the slot's image
            quantityText.text = quantity.ToString(); // sets the item quantity text to reflect the new one
        }
        else // runs at any other time
        {
            itemImage.gameObject.SetActive(false); // disables the slot's image
            quantityText.text = ""; // disables the item quantity text
        }
    }

}
