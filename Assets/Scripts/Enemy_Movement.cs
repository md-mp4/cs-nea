using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb; // reference to enemy rigidbody2D component
    private Transform player; // reference to a transform component
    public float speed; // variable for enemy's speed
    private bool isChasing; // variable for if the enemy sees and chases the player
    private int facingDirection = -1; // variable for if the enemy is facing right or left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // sets rb to the enemy's rigidbody by itself
    }


    void Update()
    {
        if(isChasing == true) // only runs if the player is in the aggro range
        {
            if(player.position.x > transform.position.x && facingDirection == -1 ||
             player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed; 
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision) // only triggers when the enemy enters the aggro range
    {
        if(collision.gameObject.tag == "Player") 
        {
            if(player == null) // checks for if player already has a value
            {
                player = collision.transform; // gets the collided player's transform
            }
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // only triggers when the enemy leaves the aggro range
    {
        if(collision.gameObject.tag == "Player") 
        {
            rb.linearVelocity = Vector2.zero;
            isChasing = false;
        }
    }

}
