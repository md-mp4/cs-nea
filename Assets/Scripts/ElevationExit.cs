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
        }
    }

}
