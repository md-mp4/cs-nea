using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 1; // variable dictating damage of enemy
    public Transform attackPoint; // reference to the child object's transform
    public float weaponRange; // variable for the weapon's range
    public LayerMask playerLayer; // layer mask used to check if the player is in range

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        // array of playerLayer objects that enter a circle of radius weapon range around attackPoint

        if(hits.Length > 0) // only runs if the enemy has detected a player to hit
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage); // does damage
        }
    }

}
