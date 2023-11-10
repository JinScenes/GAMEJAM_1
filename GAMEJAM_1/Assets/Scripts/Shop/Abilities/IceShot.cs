using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShot : MonoBehaviour
{

    Transform player;
    PlayerCombat plrCombat;

    public GameObject iceShotStartPrefab;
    public GameObject iceShotPrefab;

    public float distanceOffset = 10;
    public float timeBeforeProjectile = 1.25f;
    public float speed = 50f;
    public float damage;
    public int maxEnemyHit = 3;
    public float aliveTime = 4;

    private GameObject startShot;
    private Transform inGameShot;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").transform;
        plrCombat = player.GetComponent<PlayerCombat>();

        
        Vector3 abilityPosition = new Vector3(plrCombat.lastDir.x, plrCombat.lastDir.y, 0) * distanceOffset + player.position;
        startShot = Instantiate(iceShotStartPrefab, abilityPosition, Quaternion.identity, gameObject.transform);

        Vector3 lastDir = plrCombat.lastDir;
        if (lastDir.y == 1)
        {
            startShot.transform.rotation = Quaternion.Euler(0,0,90);
        }
        else if (lastDir.y == -1)
        {
            startShot.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (lastDir.x == -1)
        {
            startShot.GetComponent<SpriteRenderer>().flipX = true;
        }
       
        StartCoroutine(WaitTillShoot(abilityPosition, lastDir));
      
    }

    IEnumerator WaitTillShoot(Vector3 abilityPosition, Vector3 lastDir)
    {
        bool flipX = startShot.GetComponent<SpriteRenderer>().flipX;

        yield return new WaitForSeconds(timeBeforeProjectile);
        Destroy(startShot);
        
        inGameShot = Instantiate(iceShotPrefab, abilityPosition, Quaternion.identity, gameObject.transform).transform;
        DamageEnemy damageEnemy = inGameShot.GetComponent<DamageEnemy>();

        inGameShot.GetComponent<SpriteRenderer>().flipX = flipX;
        damageEnemy.speed = speed;
        if (lastDir.y == 1)
        {
            inGameShot.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (lastDir.y == -1)
        {
            inGameShot.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (lastDir.x == -1)
        {
            damageEnemy.speed = -speed;
            inGameShot.GetComponent<SpriteRenderer>().flipX = true;
        }
       
        damageEnemy.baseDamage = damage;
        damageEnemy.maxEnemiesHit = maxEnemyHit;
        
        damageEnemy.aliveTime = aliveTime;

        enabled = false;
    }

 
}
