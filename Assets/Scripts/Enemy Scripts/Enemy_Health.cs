using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
   public int currentHealth; // integer for the enemy's current health
   public int maxHealth; // integer for the enemy's max possible health
   public int expReward = 3; // exp given to the player after enemy kill

   public delegate void MonsterDefeated(int exp); // decides what information is passed to other scripts
   public static event MonsterDefeated OnMonsterDefeated; // shares information to other scripts when a monster is defeated

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount) // changes enemy health by given amount
    {
        currentHealth += amount; // adds amount to currenHealth. does damage if amount is negative

        if(currentHealth > maxHealth) // if enemy overheals
        {
            currentHealth = maxHealth;
        }

        else if(currentHealth <= 0) // if enemy dies
        {
            OnMonsterDefeated(expReward); // pass on the expReward to the expManager
            Destroy(gameObject); // kills enemy
        }

    }

}
