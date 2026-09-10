using UnityEngine;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, ReadOnly] private int playerHealth = 3;
    [SerializeField] private int playerMaxHealth = 3;

    public void TakeDamage(int damage = 1) {
        if (playerHealth < 0) return;
        playerHealth -= damage;
    }


    public void GiveHealth(int heal = 1) {
        if (playerHealth <= 0) return;
        playerHealth = Mathf.Min(playerHealth + heaL, playerMaxHealth);
    }
    
    public void ResetHealt() {
        playerHealth = playerMaxHealth;
    }
}
