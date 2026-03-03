using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class ElevationEntry : MonoBehaviour
{
    public Collider2D[] mountaincolliders; // array to hold collision layers to turn off
    public Collider2D[] boundarycolliders; // array to hold collision layers to turn on

    void OnTriggerEnter2D(Collider2D collision) // only activates if triggered by stair box colliders
    {
        if(collision.gameObject.tag == "Player") // check if player is triggering
        {
            foreach (Collider2D mountain in mountaincolliders)
            {
                mountain.enabled = false; // disables each collider given
            }

            foreach (Collider2D boundary in boundarycolliders)
            {
                boundary.enabled = true; // enables each collider given
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            // player sorting order higher than any other
            InventoryManager inventoryManager = GameObject.FindAnyObjectByType<InventoryManager>();
            // finds the inventory manager by searching through the whole scene
            inventoryManager.currentElevation = 1; // sets current elevation to 1
        }
    }

    public void RestoreMountainState() // restores colliders to if player was on mountain
    {
        foreach (Collider2D mountain in mountaincolliders) // loops through mountain colliders
        {
            mountain.enabled = false; // disables each collider
        }
        foreach (Collider2D boundary in boundarycolliders) // loops through boundary colliders
        {
            boundary.enabled = true; // enables each collider
        }
        GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder = 15; // sets player on top of everything
    }

}
