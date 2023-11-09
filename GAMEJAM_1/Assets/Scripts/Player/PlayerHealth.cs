using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player Health")]
    [Range(0, 500)] public int maxHealth;
    [Range(0, 500)] public int currentHealth;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GetComponent<Animator>().SetTrigger("IsHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
