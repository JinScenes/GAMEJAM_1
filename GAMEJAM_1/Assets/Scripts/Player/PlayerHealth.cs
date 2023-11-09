using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    private Animator anim;
    private Slider hpSlider;

    public GameObject deathUI;

    [Header("Player Health")]
    [Range(0, 500)] public int maxHealth;
    [Range(0, 500)] public int currentHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        hpSlider = GameObject.Find("HP Bar").GetComponent<Slider>();
        hpSlider.maxValue = maxHealth;
        hpSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        hpSlider.value = currentHealth;
        anim.SetTrigger("IsHurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        deathUI.SetActive(true);
    }
}
