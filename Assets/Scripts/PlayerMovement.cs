using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5; // variable for the speed of the player
    public float acceleration = 10f; // variable for player acceleration
    public Rigidbody2D rb; // reference to the player's RigidBody 2D
    public Animator animator1; // reference to the player's Animator
    public int direction = 1; // variable to define the direction the player is facing

    void Start() // Runs only when the class is initialised
    {
        
    }

    void FixedUpdate() // Runs 50x a second continuously
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

    // function used to flip the player's sprite
    void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
