using UnityEngine;
using NaughtyAttributes;

public class playerHealth : MonoBehaviour
{
    [SerializeField, ReadOnly] private int playerHealth = 3;
    [SerializeField] private int playerMaxHealth = 3;

    public void TakeDamage(int damage = 1) {
        if (playerHealth < 0) return;
        playerHealth -= damage;
    }


    public void GiveHealth(int heal = 1) {
        if (playerHealth <= 0) return;
        playerHealth = Mathf.Min(playerHealth + heal, PlayerMaxHealth);
    }
    
    public void ResetHealt() {
        playerHealth = PlayerMaxHealth;
    }
}
