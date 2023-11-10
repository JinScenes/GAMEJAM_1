using UnityEngine;

public class PoisonProjectile : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerHealth playerHealth;

    [Header("Projectile Damage")]
    [Range(0, 50), SerializeField] private int damage;

    [Header("Projectile Speed")]
    [Range(0, 50), SerializeField] private float travelSpeed;

    private Vector2 target;
    private Transform player;

    int colTimes = 0;

    [HideInInspector]
    public bool hitReg;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, travelSpeed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            sr.enabled = false;
            DestroyProjectile(8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (colTimes == 0)
            {
                playerHealth.TakeDamage(damage);
            }

            hitReg = true;
            colTimes = 1;

            sr.enabled = false;
            DestroyProjectile(8f);
        }

        if(other.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }

    private void DestroyProjectile(float time)
    {
        Destroy(gameObject, time);
    }
}
