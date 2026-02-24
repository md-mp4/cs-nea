using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Animator anim; // animator reference
    public Transform attackPoint; // reference to player's point of attack
    public LayerMask enemyLayer; // checks if there is an enemy in the attack
    public StatsUI statsUI; // reference to stats ui script

    public float timer; // float to be used as a timer
    public float cooldown = 1; // float to be the time before the player can attack again

    private void Update()
    {
        if (timer > 0) 
        {
            timer -= Time.deltaTime; // counts down the time
        }
    }

    public void Attack() // method for attacking enemies
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true); // starts attack animation

            timer = cooldown; // resets cooldown timer
        }
        
    }

    public void DealDamage() // does damage to enemy
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);
        // find enemies in the enemyLayer in a circle around an attackPoint with a radius of weaponRange

        if (enemies.Length > 0) // runs if an enemy is detected
        {
            if (enemies[0].isTrigger) // if its a trigger then the damage and knockback code don't run
            {
                return;
            }
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-StatsManager.Instance.damage); // hurts enemy
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(
                transform, StatsManager.Instance.knockbackForce, 
                StatsManager.Instance.knockbackTime, 
                StatsManager.Instance.stunTime); // knocks back enemy
        }
    }

    public void FinishAttacking() // method to stop the attack animation and state
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected() 
    // when gizmos are on, shows blue circle of range weaponRange around attackPosition
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }

}

