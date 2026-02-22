using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
   public int currenHealth; // integer for the enemy's current health
   public int maxHealth; // integer for the enemy's max possible health

    private void Start()
    {
        currenHealth = maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currenHealth += amount;

        if(currenHealth > maxHealth)
        {
            currenHealth = maxHealth;
        }

        else if(currenHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

}
