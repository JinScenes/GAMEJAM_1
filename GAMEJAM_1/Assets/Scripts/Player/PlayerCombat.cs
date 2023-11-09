using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControler playerController;
    private Animator anim;

    [Header("Attack Position Transform GameObject")]
    [SerializeField] private Transform attackPoint;

    [Header("Enemy Mask")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attack Rate & Range Stats")]
    [Range(0, 2)][SerializeField] private float attackRange;
    [Range(0, 5)][SerializeField] private float attackSpeed;

    [Header("Attack Damage")]
    [Range(0, 10)] public int minAttackDMG;
    [Range(10, 20)] public int maxAttackDMG;

    private Vector2 lastDir;

    private float originalSpeed;
    private float nextAttackTime = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerControler>();
        originalSpeed = playerController.speed;
        lastDir = Vector2.down;
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
            {
                playerController.speed /= 4f;
                AttackFunction();
                nextAttackTime = Time.time + 1f / attackSpeed;
                StartCoroutine(ResetSpeedAfterDelay(0.1f));
            }
        }

        if (playerController.movement != Vector2.zero)
        {
            lastDir = playerController.movement.normalized;
        }

        UpdateAttackPointPosition();
    }

    private void UpdateAttackPointPosition()
    {
        float attackPointDistance = 1.5f;
        Vector3 offset = new Vector3(lastDir.x, lastDir.y, 0) * attackPointDistance;
        attackPoint.position = transform.position + offset;
    }

    private void AttackFunction()
    {
        anim.SetTrigger("Attacking");

        //ENEMIES
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(Random.Range(minAttackDMG, maxAttackDMG));
        }
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        playerController.speed = originalSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
