using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("LayerMask")]
    [SerializeField] private LayerMask player;

    [HideInInspector]
    public Vector2 movement;
    Vector3 dir;

    private Transform target;

    [Header("Movement")]
    [Range(0, 20), SerializeField] private float speed;

    float moveSpeed;

    [Header("Combat Stats")]
    [Range(0, 20), SerializeField] private int damage = 1;
    [Range(0, 1), SerializeField] private float attackRangeRadius;
    [Range(0, 1), SerializeField] private float attackRadius = 0.5f;
    [Range(0, 3), SerializeField] private float attackSpeed = 2.3f;

    [Header("Attack Position Transform GameObject")]
    [SerializeField] Transform attackArea;

    private float checkRadius = 150f;
    private float nextAttackTime = 0f;

    float timebetweenFootsteps;
    float sinceLastFootstep;

    bool attackRange;
    bool chaseRange;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveSpeed = speed;
    }

    private void Update()
    {
        if (GetComponent<EnemyHealth>().isDead)
        {
            HandleDeath();
            return; 
        }

        MovementFunction();
        ChasingFunction();
    }

    private void ChasingFunction()
    {
        if (!chaseRange)
        {
            moveSpeed = 0;
        }
        else if (chaseRange)
        {
            moveSpeed = speed;
        }

        if (chaseRange && !attackRange)
        {

            MoveCharacter(movement);
            anim.SetBool("Attacking", false);
        }

        //ATTACKING THE PLAYER
        if (attackRange)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Attacking", true);

            if (Time.time >= nextAttackTime)
            {
                AttackFunction();
                nextAttackTime = Time.time + 1f / attackSpeed;
            }
        }
    }

    private void MovementFunction()
    {
        chaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, player);
        attackRange = Physics2D.OverlapCircle(transform.position, attackRangeRadius, player);

        dir = target.position - transform.position;
        dir.Normalize();

        movement = dir;

        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetFloat("Horizontal", dir.x);
            anim.SetFloat("Vertical", dir.y);
            anim.SetFloat("Speed", moveSpeed);

            timebetweenFootsteps = 0.5f;
            if (sinceLastFootstep > timebetweenFootsteps)
            {
                sinceLastFootstep = 0f;
                //PLAY AUDIO HERE
            }
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * moveSpeed * Time.deltaTime));
    }

    private void AttackFunction()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackArea.position, attackRadius, player);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void HandleDeath()
    {
        moveSpeed = 0;
        rb.velocity = Vector2.zero;
        anim.SetBool("Attacking", false);
        anim.SetBool("Moving", false);

        rb.isKinematic = true;
        rb.simulated = false;

        anim.SetTrigger("Die");
    }

    void OnDrawGizmosSelected()
    {
        //Gizmo Attacking Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRangeRadius);

        //Attack Area
        if (attackArea == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackArea.position, attackRangeRadius);
    }
}
