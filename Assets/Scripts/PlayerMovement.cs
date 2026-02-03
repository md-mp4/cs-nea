using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5; // variable for the speed of the player
    public float acceleration = 10f; // variable for player acceleration
    public Rigidbody2D rb; // reference to the player's RigidBody 2D
    

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

        // sets the player's velocity to the movement vector * speed
        movement = movement.normalized;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, movement * speed, acceleration * Time.fixedDeltaTime);
    }
}
