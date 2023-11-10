using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float stoppingDistance;

    private Vector2 movement;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sp;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GetComponent<EnemyHealth>().isDead)
        {
            HandleDeath();
            return;
        }

        Vector3 dir = player.position - transform.position;
        float distance = dir.magnitude;

        if (distance > stoppingDistance)
        {
            dir.Normalize();
            movement = dir;
            anim.SetBool("Walk", true);

            if (player.position.x < transform.position.x)
            {
                sp.flipX = true;
            }
            else
            {
                sp.flipX = false;
            }
        }
        else
        {
            movement = Vector2.zero;
            anim.SetBool("Walk", false);
        }
    }

    private void HandleDeath()
    {
        speed = 0;
        rb.velocity = Vector2.zero;
        anim.SetBool("Attacking", false);
        anim.SetBool("Moving", false);

        rb.isKinematic = true;
        rb.simulated = false;

        anim.SetTrigger("Die");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
