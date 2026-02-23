using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots; // reference to statsSlot object

    private void Start() // runs at start of the program
    {
        UpdateAllStats(); // initialises all stats in the UI
    }

    public void UpdateDamage() // changes the damage stat text
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage: " + StatsManager.Instance.damage;
        // gets the statsSlots TMP object's text and changes it to include the new damage
    }

    public void UpdateWeaponRange() // changes the weaponRange stat text
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Weapon Range: " + StatsManager.Instance.weaponRange;
        // gets the statsSlots TMP object's text and changes it to include the new weaponRange
    }

    public void UpdateSpeed() // changes the speed stat text
    {
        statsSlots[2].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;
        // gets the statsSlots TMP object's text and changes it to include the new speed
    }

    public void UpdateMaxHealth() // changes the maxHealth stat text
    {
        statsSlots[3].GetComponentInChildren<TMP_Text>().text = "Max Health: " + StatsManager.Instance.maxHealth;
        // gets the statsSlots TMP object's text and changes it to include the new maxHealth
    }

    public void UpdateKnockbackForce() // changes the knockbackForce stat text
    {
        statsSlots[4].GetComponentInChildren<TMP_Text>().text = "Knockback Force: " + StatsManager.Instance.knockbackForce;
        // gets the statsSlots TMP object's text and changes it to include the new knockbackForce
    }

    public void UpdateStunTime() // changes the stunTime stat text
    {
        statsSlots[5].GetComponentInChildren<TMP_Text>().text = "Stun Time: " + StatsManager.Instance.stunTime;
        // gets the statsSlots TMP object's text and changes it to include the new stunTime
    }

    public void UpdateAllStats()
    {
        UpdateDamage(); // initialises the damage stat in the UI
        UpdateWeaponRange(); // initialises the weapon range stat in the UI
        UpdateSpeed(); // initialises the speed stat in the UI
        UpdateMaxHealth(); // initialises the max health stat in the UI
        UpdateKnockbackForce(); // initialises the knockback force stat in the UI
        UpdateStunTime(); // initialises the stun time stat in the UI
    }

}
