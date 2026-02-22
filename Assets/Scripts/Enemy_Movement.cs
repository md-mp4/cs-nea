using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.XR;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb; // reference to enemy rigidbody2D component
    private Transform player; // reference to a transform component
    private Animator anim; // reference to enemy's animator
    private EnemyState enemyState; // reference to the enum set below
    public float speed; // variable for enemy's speed
    private int facingDirection = -1; // variable for if the enemy is facing right or left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // sets rb to the enemy's rigidbody by itself
        anim = GetComponent<Animator>(); // sets anim to the enemy's animator
        ChangeState(EnemyState.Idle); // sets enemy animation to idle by default
    }


    void Update()
    {
        if(enemyState == EnemyState.Chasing) // only runs if the enemy is chasing
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
            ChangeState(EnemyState.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // only triggers when the enemy leaves the aggro range
    {
        if(collision.gameObject.tag == "Player") 
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        if(enemyState == EnemyState.Idle) // Exits current animation
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);

        enemyState = newState; // Updates the current state

        if(enemyState == EnemyState.Idle) // Updates the new animation
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
    }

}

public enum EnemyState
{
    Idle,
    Chasing,
}