using System;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO; // ItemSO script reference
    public Animator anim; // animator reference
    public SpriteRenderer sr; // sprite renderer reference

    public bool canBePickedUp = true; // bool for whether the player can pick up the item or not
    public int quantity; // defines the amount of the loot item given
    public static event Action<ItemSO, int> OnItemLooted;
    // global alert which passes parameters to subscribed scripts which are listening for it

    private void OnValidate() // runs whenever the Unity inspector is used to edit the loot
    {
        if (itemSO == null) // if there is no itemSO dictated
            return; // exit method

        sr.sprite = itemSO.icon; // sets the loot object sprite to the itemSo sprite
        this.name = itemSO.itemName; // sets the loot object name to the name of the itemSO
    }

    public void Initialise(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO; // sets the object's itemSO to the one passed in
        this.quantity = quantity; // sets the object's quantity to the one passed in
        canBePickedUp = false; // blocks the player from picking it back up

        sr.sprite = itemSO.icon; // sets the loot object sprite to the itemSo sprite
        this.name = itemSO.itemName; // sets the loot object name to the name of the itemSO
    }

    private void OnTriggerEnter2D(Collider2D collision) // runs when the player enters the trigger collider
    {
        if(collision.CompareTag("Player") && canBePickedUp == true) 
        // runs code if the one who triggers the collider has the player tag and the item can be picked up
        {
            anim.Play("Item Pickup Animation"); // plays the pickup animation
            OnItemLooted?.Invoke(itemSO, quantity);
            // global alert passing itemSO and quantity to listeners, only if there are subscribed listeners
            Destroy(gameObject, 0.5f); // destroys the item object after 0.5s so it can finish the animation first
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // runs when the player leaves the trigger collider
    {
        if(collision.CompareTag("Player")) // runs code if the triggering's collider has the player tag
        {
            canBePickedUp = true; // lets the item be picked up again
        }
    }

}
