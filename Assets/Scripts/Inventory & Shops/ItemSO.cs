using UnityEngine;

[CreateAssetMenu(fileName = "New Item")] // allows me to make new SO in Unity
public class ItemSO : ScriptableObject
{
    public string itemName; // defines the item's name
    [TextArea] public string itemDescription; // defines the item's description
    public Sprite icon; // defines the item's sprite / appearance

    public bool isGold; // checks if the item is gold or not - handles differently
    public int stackSize = 4; // max amount of the item one slot can have

    [Header("Stats")] // below are stats that can be changed by items
    public int currentHealth;
    public int maxHealth;
    public int speed;
    public int damage;
    public int weaponRange;
    public int knockbackForce;
    public int knockbackTime;
    public int stunTime;

    [Header("Duration")] // holds the duration of temporary items
    public float duration; // duration of temporary items



}
