using UnityEngine;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, ReadOnly] private int PlayerHealth = 3;
    [SerializeField] private int PlayerMaxHealth = 3;

    public void TakeDamage(int damage = 1) {
        if (PlayerHealth < 0) return;
        PlayerHealth -= damage;
    }


    public void GiveHealth(int heal = 1) {
        if (PlayerHealth <= 0) return;
        PlayerHealth = Mathf.Min(PlayerHealth + heal, PlayerMaxHealth);
    }
    
    public void ResetHealt() {
        PlayerHealth = PlayerMaxHealth;
    }
}
