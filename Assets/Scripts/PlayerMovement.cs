using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5; // variable for the speed of the player
    public float acceleration = 10f; // variable for player acceleration
    public int direction = 1; // variable to define the direction the player is facing
    public Rigidbody2D rb; // reference to the player's RigidBody 2D
    public Animator animator1; // reference to the player's Animator
    public Player_Combat playerCombat; // reference to player combat file

    private bool isKnockedBack;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                playerCombat.Attack();
            }
            
        }
    }

    void FixedUpdate() // Runs 50x a second continuously
    {
        if(isKnockedBack == false) // only runs code if the player isn't taking knockback
        {
            var keyboard = Keyboard.current; // variable to check the curent state of the keyboard
            if (keyboard == null) return; // check for if no keyboard input is recieved
            Vector2 movement = new Vector2(0,0); // initialises "movement" vector + sets to 0 at beginning

            // checks if a certain key is pressed
            if (keyboard.wKey.isPressed)
            {
                movement.y = 1;
            }
            if (keyboard.sKey.isPressed)
            {
                movement.y = -1;
            }
            if (keyboard.dKey.isPressed)
            {
                movement.x = 1;
            }
            if (keyboard.aKey.isPressed)
            {
                movement.x = -1;
            }

            if(movement.x > 0 && transform.localScale.x < 0 || movement.x < 0 && transform.localScale.x > 0) {
                Flip();
            }

            // Send the direction of movement to the animator
            animator1.SetFloat("horizontal", Mathf.Abs(movement.x));
            animator1.SetFloat("vertical", Mathf.Abs(movement.y));

            // defines the player's movement and gives them velocity and acceleration
            movement = movement.normalized;
            rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, movement * speed, acceleration * Time.fixedDeltaTime);
        }

    }

    // function used to flip the player's sprite
    void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force, float stunTime) // knocks player back
    {
        isKnockedBack = true; // removes player movement
        Vector2 direction = (transform.position - enemy.position).normalized; // direction to be knocked back is calculated
        rb.linearVelocity = direction * force; // moves player in direction calculated above
        StartCoroutine(KnockbackCounter(stunTime)); // stuns the player for stunTime
    }

    IEnumerator KnockbackCounter(float stunTime) // coroutine to stun for certain time after knockback
    {
        yield return new WaitForSeconds(stunTime); // waits stunTime seconds
        rb.linearVelocity = Vector2.zero; // zeroes speed
        isKnockedBack = false; // gives player control back
    }

}
