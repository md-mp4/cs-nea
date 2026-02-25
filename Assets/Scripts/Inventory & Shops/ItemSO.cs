using UnityEngine;

[CreateAssetMenu(fileName = "New Item")] // allows me to make new SO in Unity
public class ItemSO : ScriptableObject
{
    public string itemName; // defines the item's name
    [TextArea] public string itemDescription; // defines the item's description
    public Sprite icon; // defines the item's sprite / appearance

    public bool isGold; // checks if the item is gold or not - handles differently

    [Header("Stats")] // below are stats that can be changed by items
    public int currentHealth;
    public int maxHealth;
    public int speed;
    public int damage;

    [Header("Duration")] // holds the duration of temporary items
    public float duration; // duration of temporary items



}
