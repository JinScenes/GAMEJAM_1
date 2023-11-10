using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Transform player;

    Vector2 dir;
    Vector3 checkMove;

    [Header("PlayerMask")]
    [SerializeField] LayerMask playerMask;

    /////////////////////////////////

    [Header("Movement Speeds")]
    //MOVE SPEED
    [Range(0, 50)][SerializeField] float speed;
    float moveSpeed;

    //MOVE BACKWARDS
    [Range(0, 30)][SerializeField] float retreatSpeed;

    /////////////////////////////////

    //RANGE STATS
    [Header("Check Player In Range")]
    [Range(0, 500)][SerializeField] float rangeRadius;

    [Header("Check Attacking Range")]
    [Range(0, 100)][SerializeField] float attackRadius;

    /////////////////////////////////

    //DISTANCE STATS
    [Header("Retreat Distance")]
    [Range(0, 50)][SerializeField] float retreatDis;

    [Header("Stopping Distance")]
    [Range(0, 10)][SerializeField] float stopDis;

    /////////////////////////////////

    //PROJECTILE GAMEOBJ
    [Header("Projectile")]
    [SerializeField] GameObject projectileObj;

    /////////////////////////////////

    //PROJECTILEL STATS
    [Header("Projectile Shooting Speed & Cooldown")]
    [Range(0, 5)][SerializeField] float shootRate;
    [Range(0, 5)][SerializeField] float shootCooldown;

    bool chaseRange;
    bool attackRange;

    float timebetweenFootsteps;
    float sinceLastFootstep;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        shootCooldown = shootRate;
        moveSpeed = speed;
    }

    void Update()
    {
        if (GetComponent<EnemyHealth>().isDead)
        {
            HandleDeath();
            return;
        }

        //GIZMOS CREATION
        chaseRange = Physics2D.OverlapCircle(transform.position, rangeRadius, playerMask);
        attackRange = Physics2D.OverlapCircle(transform.position, attackRadius, playerMask);

        //NOT IN RANGE
        if (!chaseRange || GetComponent<EnemyHealth>().isDead)
        {
            moveSpeed = 0;
        }

        // Enemy moves towards player if in chase range and not too close
        if (chaseRange && Vector2.Distance(transform.position, player.position) > stopDis)
        {
            moveSpeed = speed;
            timebetweenFootsteps = 0.4f;

            if (sinceLastFootstep > timebetweenFootsteps)
            {
                sinceLastFootstep = 0f;
            }

            // Move to player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, player.position) < retreatDis)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -retreatSpeed * Time.deltaTime);
        }

        float facingDir = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();

        dir = player.position - transform.position;
        checkMove = dir;

        if (checkMove.x != 0 || checkMove.y != 0)
        {
            anim.SetFloat("Horizontal", dir.x);
            anim.SetFloat("Vertical", dir.y);
            anim.SetFloat("Speed", moveSpeed);
        }

        ///////////////
        ///PROJECTILES
        ///////////////

        if (attackRange)
        {
            //IF COOLDOWN REACHES 0 FROM COUNTDOWN OF shootCooldown
            if (shootCooldown <= 0)
            {
                //SHOOT PROJECTILE && PLAY ATTACK ANIMATION
                anim.SetTrigger("Attacking");

                Instantiate(projectileObj, transform.position, Quaternion.identity);
                //RESET COOLDOWN BACK TO shotCooldown
                shootCooldown = shootRate;
            }
            else
            {
                //STARTS THE COOLDOWN AGAIN
                shootCooldown -= Time.deltaTime;
            }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, retreatDis);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDis);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, rangeRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
