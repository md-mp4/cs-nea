using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO; // ItemSO script reference
    public Animator anim; // animator reference
    public SpriteRenderer sr; // sprite renderer reference

    public int quantity; // defines the amount of the loot item given

    private void OnValidate() // runs whenever the Unity inspector is used to edit the loot
    {
        if (itemSO == null) // if there is no itemSO dictated
            return; // exit method

        sr.sprite = itemSO.icon; // sets the loot object sprite to the itemSo sprite
        this.name = itemSO.itemName; // sets the loot object name to the name of the itemSO
    }
}
