using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthslider; // refers to the player's health slider
    public TMP_Text healthText; // reference to the health UI element
    public Animator healthTextAnimation; // reference to text's animator

    private void Start() // runs at the start of the game
    {
        healthslider.maxValue = StatsManager.Instance.maxHealth; // sets slider max possible value to player max hp
        healthslider.value = StatsManager.Instance.currentHealth; // sets slider current value to player current hp
        healthText.text = "HP" + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth; // sets text to current hp
    }

    public void ChangeHealth(int amount) // used to modify player health
    {
        StatsManager.Instance.currentHealth += amount;
        healthTextAnimation.Play("health text animation"); // plays text animation upon hp change

        healthslider.value = StatsManager.Instance.currentHealth; // sets slider current value to player current hp
        healthText.text = "HP" + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth; // sets text to current hp

        if(StatsManager.Instance.currentHealth <= 0) // present so if the player health goes to 0 or below, they die
        {
            gameObject.SetActive(false);
        }
    }
}
