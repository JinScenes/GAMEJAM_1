using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator anim;

    [Header("Player Health")]
    [Range(0, 500)] public int maxHealth;
    [Range(0, 500)] public int currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("IsHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
