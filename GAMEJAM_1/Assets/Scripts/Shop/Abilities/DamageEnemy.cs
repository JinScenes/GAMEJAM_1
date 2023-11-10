using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageEnemy : MonoBehaviour
{

    private float BaseDamage = 5;

    public float baseDamage{ 
        get { return BaseDamage; }
        set {
            BaseDamage = value;
            maxDamage = value * 1.5f;
        }
    }
    public float maxDamage = 10;

    public bool destroyOnFirstHit = false;
    private bool hitEnemy = false;

    public float speed = 3;
    public float aliveTime = 4;

    public int maxEnemiesHit = 2;
    public int currentEnemiesHit = 0;

    public LayerMask enemyLayer;
    public GameObject hitEffectPrefab;

    private List<GameObject> enemiesHit = new List<GameObject>();
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.layer == 7 && currentEnemiesHit < maxEnemiesHit) //Enemy layer
        //{
        //    currentEnemiesHit++;
        //    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage((int)Random.Range(baseDamage, maxDamage));

        //    if (destroyOnFirstHit || currentEnemiesHit >= maxEnemiesHit)
        //    {
        //        print("Hit max enemies");
        //        Destroy(gameObject);
        //    }

        //}
    }

    private void Update()
    {
        aliveTime -= Time.deltaTime;
        if (aliveTime <= 0)
        {
            Destroy(gameObject);
            
        }
        
        MoveProjectile(Time.deltaTime);

        Collider2D hitEnemy = Physics2D.OverlapCircle(transform.position, 5f, enemyLayer);
        if (hitEnemy && hitEnemy.tag != "Player" && !enemiesHit.Contains(hitEnemy.gameObject))
        {
            enemiesHit.Add(hitEnemy.gameObject);
            currentEnemiesHit++;
            hitEnemy.gameObject.GetComponent<EnemyHealth>().TakeDamage((int)Random.Range(baseDamage, maxDamage));
            Instantiate(hitEffectPrefab, transform.position, Quaternion.identity, GameObject.Find("Abilities").transform);

            if (destroyOnFirstHit || currentEnemiesHit >= maxEnemiesHit)
            { 
                Destroy(gameObject);
            }
        }
    }

 

    private void MoveProjectile(float delta)
    {
        // Get the current position and direction of the projectile
        Vector3 currentPosition = transform.position;
        Vector3 forwardDirection = transform.right;

        // Calculate the new position based on the direction and speed
        Vector3 newPosition = currentPosition + forwardDirection * speed * delta;

        // Move the projectile to the new position
        transform.position = newPosition;

    }
}
