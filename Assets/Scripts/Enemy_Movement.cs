using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.XR;

public class Enemy_Movement : MonoBehaviour
{
    private Rigidbody2D rb; // reference to enemy rigidbody2D component
    private Transform player; // reference to a transform component
    private Animator anim; // reference to enemy's animator
    private EnemyState enemyState; // reference to the enum set below
    private int facingDirection = -1; // variable for if the enemy is facing right or left
    private float attackCooldownTimer; // counts the time between attacks
    
    public float speed; // variable for enemy's speed
    public float attackRange = 2; // variable for how far the enemy attacks
    public float attackCooldown = 2; // variable for time between enemy attacks
    public float playerDetectRange = 5; // variable for how far the enemy detects player
    public Transform detectionPoint; // reference to a transform for detecting player
    public LayerMask playerLayer; // used to detect players only

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // sets rb to the enemy's rigidbody by itself
        anim = GetComponent<Animator>(); // sets anim to the enemy's animator
        ChangeState(EnemyState.Idle); // sets enemy animation to idle by default
    }


    void Update()
    {
        CheckForPlayer();
        
        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if(enemyState == EnemyState.Chasing) // only runs if the enemy is chasing
        {
            Chase();
        }
        if(enemyState == EnemyState.Attacking) // only runs if the enemy is attacking
        {
            rb.linearVelocity = Vector2.zero; // zeroes velocity
        }
    }

    void Chase() // code for what to do if the enemy is chasing the player
    {
        if(player.position.x > transform.position.x && facingDirection == -1 ||
             player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }
        
        Vector2 direction = (player.position - transform.position).normalized; // finds direction to move to the player
        rb.linearVelocity = direction * speed; 
    }

    void Flip() // flips the player
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer() // only triggers when the enemy enters the aggro range
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer); // array for detecting player
        
        if (hits.Length > 0)
        {
            player = hits[0].transform;
       
            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            // if player is in attack range and cooldown is ready
            {
                attackCooldownTimer = attackCooldown; // resets attack cooldown timer
                ChangeState(EnemyState.Attacking); 
                // changes state to attacking if the distance between player and enemy is less than attack range
            }
        
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            // if enemy sees player but can't attack and they aren't in the attacking state
            {
                ChangeState(EnemyState.Chasing);
            }
        }

        else // if the enemy can't see or attack player
        {
            rb.linearVelocity = Vector2.zero; // set velocity to 0
            ChangeState(EnemyState.Idle);
        }

    }

    void ChangeState(EnemyState newState)
    {
        if(enemyState == EnemyState.Idle) // Exits current animation
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        enemyState = newState; // Updates the current state

        if(enemyState == EnemyState.Idle) // Updates the new animation
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }

}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
}