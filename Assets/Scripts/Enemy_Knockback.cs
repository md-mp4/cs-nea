using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb; // reference to a rigidbody
    private Enemy_Movement enemy_Movement; // reference to the enemy movement script

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // sets rb to player's rigidbody on start
        enemy_Movement = GetComponent<Enemy_Movement>(); // sets enemy_movement to the enemy's Enemy_Movement script
    }

    public void Knockback(Transform playerTransform, float knockbackForce, float knockbackTime, float stunTime) // does knockback to enemy
    {
        enemy_Movement.ChangeState(EnemyState.Knockback); // changes the enemy's state from the current to Knockback
        StartCoroutine(StunTimer(knockbackTime, stunTime)); // uses the StunTimer coroutine after stunTime is completed and resets everything
        Vector2 direction = (transform.position - playerTransform.position).normalized; // calculates direction of kb
        rb.linearVelocity = direction * knockbackForce; 
        // creates a knockback force of magnitude knockbackForce in direction direction
    }

    IEnumerator StunTimer(float knockbackTime, float stunTime) // coroutine to run after a certain stunTime is completed
    {
        yield return new WaitForSeconds(knockbackTime); // waits for knockbackTime seconds before running the code ahead
        rb.linearVelocity = Vector2.zero; // sets enemy's velocity to 0 after the stunTime ends
        yield return new WaitForSeconds(stunTime); // waits for stunTime seconds before running the code ahead
        enemy_Movement.ChangeState(EnemyState.Idle); // sets enemy's state back to idle after being knocked back
    }

}
