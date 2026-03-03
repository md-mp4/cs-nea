using UnityEngine;

public class ElevationExit : MonoBehaviour
{
    public Collider2D[] mountaincolliders; // array to hold collision layers to turn off
    public Collider2D[] boundarycolliders; // array to hold collision layers to turn on

    void OnTriggerEnter2D(Collider2D collision) // only activates if triggered by stair box colliders
    {
        if(collision.gameObject.tag == "Player") // check if player is triggering
        {
            foreach (Collider2D mountain in mountaincolliders)
            {
                mountain.enabled = true; // disables each collider given
            }

            foreach (Collider2D boundary in boundarycolliders)
            {
                boundary.enabled = false; // enables each collider given
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
             // player sorting order higher than any other
            InventoryManager inventoryManager = GameObject.FindAnyObjectByType<InventoryManager>();
            // finds the inventory manager by searching through the whole scene
            inventoryManager.currentElevation = 0; // sets current elevation to 0
        }
    }

    public void RestoreGroundState() // restores colliders to if player was on ground
    {
        foreach (Collider2D mountain in mountaincolliders) // loops through mountain colliders
        {
            mountain.enabled = true; // enables each collider
        }
        foreach (Collider2D boundary in boundarycolliders) // loops through boundary colliders
        {
            boundary.enabled = false; // disables each collider
        }
        GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder = 5; // sets player on the ground
    }
}
