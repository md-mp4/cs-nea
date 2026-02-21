using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth; // refers to player's current health
    public int maxHealth; // refers to player's max possible health currently
    public Slider healthslider; // refers to the player's health slider
    public TMP_Text healthText; // reference to the health UI element
    public Animator healthTextAnimation; // reference to text's animator

    private void Start() // runs at the start of the game
    {
        healthslider.maxValue = maxHealth; // sets slider max possible value to player max hp
        healthslider.value = currentHealth; // sets slider current value to player current hp
        healthText.text = "HP" + currentHealth + " / " + maxHealth; // sets text to current hp
    }

    public void ChangeHealth(int amount) // used to modify player health
    {
        currentHealth += amount;
        healthTextAnimation.Play("health text animation"); // plays text animation upon hp change

        healthslider.value = currentHealth; // sets slider current value to player current hp
        healthText.text = "HP" + currentHealth + " / " + maxHealth; // sets text to current hp

        if(currentHealth <= 0) // present so if the player health goes to 0 or below, they die
        {
            gameObject.SetActive(false);
        }
    }
}
