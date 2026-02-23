using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots; // reference to statsSlot object

    private void Start() // runs at start of the program
    {
        UpdateDamage(); // initialises the damage stat
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

}
