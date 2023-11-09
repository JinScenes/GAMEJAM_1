using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarBurst : MonoBehaviour
{
    [SerializeField] int damageAmount = 20;
    [SerializeField] float duration = 3f;
    [SerializeField] int width = 18;
    [SerializeField] int height = 12;

    public BoxCollider2D damageCollider;

    void Start()
    {
       damageCollider.enabled = false;

        damageCollider.size = new Vector2(width, height);
    }

    public void ActivateAbility()
    {
        StartCoroutine(ActivateCollider());
    }

    private IEnumerator ActivateCollider()
    {
        damageCollider.enabled = true;
        yield return new WaitForSeconds(duration);
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
        }
    }
}
