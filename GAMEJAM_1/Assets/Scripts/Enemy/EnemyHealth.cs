using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sp;

    [Header("Health Stats")]
    [Range(0, 100)] [SerializeField] private int maxHealth;
    [Range(0, 50)] [SerializeField] private int currentHealth;

    [SerializeField] private GameObject droppedItem;

    [HideInInspector]
    public bool isDead = false;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        anim.SetTrigger("IsHurt");
        StartCoroutine(FlashColor(0.2f));

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            DeathFunction(1f);
        }
    }


    private IEnumerator FlashColor(float duration)
    {
        Color originalColor = sp.color;
        sp.color = Color.red;
        yield return new WaitForSeconds(duration);
        sp.color = originalColor;
    }

    private void DeathFunction(float time)
    {
        Instantiate(droppedItem, transform.position, Quaternion.identity);
        anim.SetBool("IsDead", true);
        Destroy(gameObject, time);
        this.enabled = false;
    }
}
