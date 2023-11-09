using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydroArea : MonoBehaviour
{
    [SerializeField] int damage = 20;
    [SerializeField] float radius = 5f;
    [SerializeField] int duration = 10;

    private CircleCollider2D hydroCollider;
    private bool isActive = false;

    private void Awake()
    {
        hydroCollider = gameObject.GetComponent<CircleCollider2D>();
        hydroCollider.radius = radius;
        hydroCollider.isTrigger = true;
        hydroCollider.enabled = false;
    }

    public void ActivateAbility()
    {
        isActive = true;
        hydroCollider.enabled = true;
        StartCoroutine(DeactivateAfterDuration());
    }

    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        hydroCollider.enabled = false;
        isActive = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isActive && other.CompareTag("Enemy"))
        {
            //other.GetComponent<Enemy>().TakeDamage(damage * Time.deltaTime);
        }
    }
}
