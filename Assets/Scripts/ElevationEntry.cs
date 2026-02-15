using UnityEngine;

public class ElevationEntry : MonoBehaviour
{
    public Collider2D[] mountaincolliders; // array to hold collision layers

    void OnTriggerEnter2D(Collider2D collision) // only activates if triggered by stair box colliders
    {
        if(collision.gameObject.tag == "Player") // check if player is triggering
        {
            foreach (Collider2D mountain in mountaincolliders)
            {
                mountain.enabled = false; // disables each collider given
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
             // player sorting order higher than any other
        }
    }

}
