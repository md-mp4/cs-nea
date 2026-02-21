using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb; // reference to enemy rigidbody2D component
    public Transform player; // reference to a transform component
    public float speed; // variable for enemy's speed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // sets rb to the enemy's rigidbody by itself
    }


    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
}
