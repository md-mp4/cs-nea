using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
   public int currentHealth; // integer for the enemy's current health
   public int maxHealth; // integer for the enemy's max possible health

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        else if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

}
