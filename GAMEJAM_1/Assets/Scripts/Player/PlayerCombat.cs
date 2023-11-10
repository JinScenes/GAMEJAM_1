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

    [Header("Attack Box Dimensions")]
    [SerializeField] private Vector2 attackBoxSize;
    [Range(0, 5)][SerializeField] private float attackSpeed;

    [SerializeField] float attackPointDistance;
    
    [Header("Attack Damage")]
    [Range(0, 10)] public int minAttackDMG;
    [Range(10, 20)] public int maxAttackDMG;


    public Vector2 lastDir;

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
        float attackPointDistance = 1.9f;
        Vector3 offset = new Vector3(lastDir.x, lastDir.y, 0) * attackPointDistance;
        attackPoint.position = transform.position + offset;
    }

    private void AttackFunction()
    {
        anim.SetTrigger("Attacking");
        AudioManager.Instance.PlaySFX("Player Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, attackBoxSize, 0, enemyMask);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(Random.Range(minAttackDMG, maxAttackDMG));
            //Debug.Log((Random.Range(minAttackDMG, maxAttackDMG)));
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

        Gizmos.color = Color.red;
        // Draw a cube in the scene view for the attack box
        Gizmos.DrawWireCube(attackPoint.position, new Vector3(attackBoxSize.x, attackBoxSize.y, 1));
    }
}
