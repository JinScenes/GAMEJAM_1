using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 5;
    public float detectionRadius = 1.0f;
    public float attackCooldown = 2f;

    private float lastAttackTime = 0;

    void Update()
    {        
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (Vector2.Distance(transform.position, player.transform.position) < detectionRadius && Time.time > lastAttackTime + attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }
}
