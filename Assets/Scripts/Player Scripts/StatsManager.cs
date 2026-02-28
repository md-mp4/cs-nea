using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance; // single stats manager reference - only 1 should exist
    public TMP_Text healthText; // health text
    public StatsUI statsUI; // stats UI reference

    [Header("Combat Stats")]
    public int damage; // player's damage
    public float weaponRange; // player's attack reach
    public float knockbackForce; // how far enemies are knocked back
    public float knockbackTime; // how lon enemies are knocked back
    public float stunTime; // how long enemies are stunned after being knocked back

    [Header("Movement Stats")]
    public int speed; // how fast the player moves

    [Header("Health Stats")]
    public int maxHealth; // max possible player hp
    public int currentHealth; // current player hp

    private void Awake() // runs before the game starts
    {
        if (Instance == null) // if there is no matching instance
        {
            Instance = this; // keep the instance as is
        }
        else
        {
            Destroy(gameObject); // destroy the instance
        }
    }

    public void UpdateMaxHealth(int amount) // updates maxHealth and text
    {
        maxHealth += amount; // increases maxHealth by amount
        healthText.text = "HP: " + currentHealth + "/" + maxHealth; 
        // changes health text to reflect new values
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    } 
    
    public void UpdateHealth(int amount) // updates currentHealth and text
    {
        currentHealth += amount; // increases currentHealth by amount
        healthText.text = "HP: " + currentHealth + "/" + maxHealth; 
        // changes health text to reflect new values
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    } 

    public void UpdateDamage(int amount) // updates damage
    {
        damage += amount; // increases damage by amount
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    }

    public void UpdateSpeed(int amount) // updates speed
    {
        speed += amount; // increases speed by amount
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    }
    public void UpdateWeaponRange(int amount) // updates weaponRange
    {
        weaponRange += amount; // increases weaponRange by amount
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    }

    public void UpdateKnockbackForce(int amount) // updates knockbackForce
    {
        knockbackForce += amount; // increases knockbackForce by amount
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    }

    public void UpdateKnockbackTime(int amount) // updates knockbackTime
    {
        knockbackTime += amount; // increases knockbackTime by amount
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    }

    public void UpdateStunTime(int amount) // updates stunTime
    {
        stunTime += amount; // increases stunTime by amount
        statsUI.UpdateAllStats(); // updates stats UI to reflect new stats
    }
}
