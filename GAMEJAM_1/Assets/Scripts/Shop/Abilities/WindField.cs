using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindField : MonoBehaviour
{
    [SerializeField] int pushForce = 10;
    [SerializeField] float duration = 5f;
    [SerializeField] int width = 30;
    [SerializeField] int height = 30;

    private BoxCollider2D windCollider;

    private void Awake()
    {        
        windCollider = gameObject.AddComponent<BoxCollider2D>();
        windCollider.isTrigger = true;
        windCollider.size = new Vector2(width, height);
        windCollider.enabled = false; 
    }

    public void ActivateAbility()
    {
        windCollider.enabled = true;
        // Start the effect
        StartCoroutine(DeactivateAfterDuration());
    }

    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        windCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 pushDirection = (other.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
