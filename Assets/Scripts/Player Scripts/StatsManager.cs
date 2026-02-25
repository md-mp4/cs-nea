using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Rendering;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance; // single stats manager reference - only 1 should exist

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

}
