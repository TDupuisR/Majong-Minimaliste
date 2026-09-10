using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, ReadOnly] private int playerHealth = 3;
    [SerializeField] private int playerMaxHealth = 3;

    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;
    [SerializeField] private List<SpriteRenderer> spriteRenderer;

    private void ChangeSprite(int index, bool state) {
        if (index < 0 || index >= spriteRenderer.Count) return;
        spriteRenderer[index].sprite = state ? spriteOn : spriteOff;
    }

    public void TakeDamage(int damage = 1) {
        if (playerHealth < 0) return;
        playerHealth -= damage;
        ChangeSprite(playerHealth + 1, false);
    }

    public void GiveHealth(int heal = 1) {
        if (playerHealth <= 0) return;
        playerHealth = Mathf.Min(playerHealth + heal, playerMaxHealth);
        ChangeSprite(playerHealth, true);
    }
    
    public void ResetHealt() {
        playerHealth = playerMaxHealth;
        for (int i = 0; i < spriteRenderer.Count; i++) {
            ChangeSprite(i, true);
        }
    }
}
