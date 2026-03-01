using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApplyItemEffects(ItemSO itemSO) 
    // applies the effects of the items on the player
    {
        if (itemSO.currentHealth > 0) // if the item changes health
        {
            StatsManager.Instance.UpdateHealth(itemSO.currentHealth); 
            // updates health to increase it by the amount in the item
        }
        if (itemSO.maxHealth > 0) // if the item changes max health
        {
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth); 
            // updates max to increase it by the amount in the item
        }
        if (itemSO.damage > 0) // if the item changes damage
        {
            StatsManager.Instance.UpdateDamage(itemSO.damage); 
            // updates damage to increase it by the amount in the item
        }
        if (itemSO.speed > 0) // if the item changes speed
        {
            StatsManager.Instance.UpdateSpeed(itemSO.speed); 
            // updates speed to increase it by the amount in the item
        }
        if (itemSO.weaponRange > 0) // if the item changes weaponRange
        {
            StatsManager.Instance.UpdateWeaponRange(itemSO.weaponRange); 
            // updates weaponRange to increase it by the amount in the item
        }
        if (itemSO.knockbackForce > 0) // if the item changes knockbackForce
        {
            StatsManager.Instance.UpdateKnockbackForce(itemSO.knockbackForce); 
            // updates knockbackForce to increase it by the amount in the item
        }
        if (itemSO.knockbackTime > 0) // if the item changes knockbackTime
        {
            StatsManager.Instance.UpdateKnockbackTime(itemSO.knockbackTime); 
            // updates knockbackTime to increase it by the amount in the item
        }
        if (itemSO.stunTime > 0) // if the item changes stunTime
        {
            StatsManager.Instance.UpdateStunTime(itemSO.stunTime); 
            // updates stunTime to increase it by the amount in the item
        }
        if (itemSO.duration > 0) // if the item has a duration
        {
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));
            // starts the coroutine which removes buffs after the item duration
        }
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration) 
    // coroutine for waiting for the duration of the items to run out, then removing the effects
    {
        yield return new WaitForSeconds(duration); // waits for duration seconds
        
        if (itemSO.maxHealth > 0) // if the item changes max health
        {
            StatsManager.Instance.UpdateMaxHealth(-itemSO.maxHealth); 
            // updates max to increase it by the amount in the item
        }
        if (itemSO.damage > 0) // if the item changes damage
        {
            StatsManager.Instance.UpdateDamage(-itemSO.damage); 
            // updates damage to increase it by the amount in the item
        }
        if (itemSO.speed > 0) // if the item changes speed
        {
            StatsManager.Instance.UpdateSpeed(-itemSO.speed); 
            // updates speed to increase it by the amount in the item
        }
        if (itemSO.weaponRange > 0) // if the item changes weaponRange
        {
            StatsManager.Instance.UpdateWeaponRange(-itemSO.weaponRange); 
            // updates weaponRange to increase it by the amount in the item
        }
        if (itemSO.knockbackForce > 0) // if the item changes knockbackForce
        {
            StatsManager.Instance.UpdateKnockbackForce(-itemSO.knockbackForce); 
            // updates knockbackForce to increase it by the amount in the item
        }
        if (itemSO.knockbackTime > 0) // if the item changes knockbackTime
        {
            StatsManager.Instance.UpdateKnockbackTime(-itemSO.knockbackTime); 
            // updates knockbackTime to increase it by the amount in the item
        }
        if (itemSO.stunTime > 0) // if the item changes stunTime
        {
            StatsManager.Instance.UpdateStunTime(-itemSO.stunTime); 
            // updates stunTime to increase it by the amount in the item
        }
    }

}
