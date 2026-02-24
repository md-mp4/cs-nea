using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ExpManager : MonoBehaviour
{
    public int level; // player current level
    public int currentExp; // the amount of xp the player has to reach the next level
    public int expToLevel = 10; // the xp needed to reach next level
    public float expGrowthMultiplier = 1.2f; // each level requires 20% more exp

    public Slider expSlider; // reference to UI slider
    public TMP_Text currentLevelText; // reference to UI level text

    private void Start() // runs at the start of the program
    {
        UpdateUI(); // readys up the UI with the correct starting values
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                GainExperience(2);
            }
            
        }
    }

    public void GainExperience(int amount) // increases player experience
    {
        currentExp += amount; // increases currentExp by the amount given as a method parameter
        if (currentExp >= expToLevel) // if they have enough exp to level up
        {
            LevelUp(); // calls level up method
        }
        UpdateUI(); // updates UI with new values
    }

    public void LevelUp() // increases player level
    {
        level ++; // increments level
        currentExp -= expToLevel; // gives player any overflow xp after leveling
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);
        // increases expToLevel by the multiplier after each level up. also rounds to integer
        // since expToLevel is an integer value, so a float would break it
    }


    public void UpdateUI() // updates UI to have new values
    {
        expSlider.maxValue = expToLevel; // sets bar max value to expToLevel
        expSlider.value = currentExp; // sets bar current value to expToLevel
        currentLevelText.text = "Level: " + level; 
        // changes the level text to display the player's actual level
    }

}
