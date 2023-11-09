using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerControler playerController;
    private Animator anim;

    [Header("Attack Position Transform GameObject")]
    [SerializeField] Transform attackPoint;

    [Header("Enemy Mask")]
    [SerializeField] LayerMask enemyMask;
    [Header("Object Mask")]
    [SerializeField] LayerMask objectMask;

    [Header("Attack Rate & Range Stats")]
    [Range(0, 2)][SerializeField] float attackRange = 0.5f;
    [Range(0, 5)][SerializeField] float attackRate = 2.3f;

    [Header("Attack Damage")]
    [Range(0, 10)] public int minAttackDMG;
    [Range(10, 20)] public int maxAttackDMG;

    private float newSpeed = 4f;
    private float nextAttackTime = 0f;

    void Start()
    {
        //COMPONENTS
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0))
            {
                playerController.speed = 2f;

                //Audio
                //FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.Sound.playerAttack, transform.position, 1f);
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                StartCoroutine(SpeedChange(0.4f));
            }
        }
    }

    #region Attacking

    void Attack()
    {
        anim.SetTrigger("Attacking");

        //ENEMIES
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyMask);
        foreach (Collider2D enemy in hitEnemies)
        {
            //ATTACKS ENEMY FROM A RANGE OF DAMAGE
            //enemy.GetComponent<Enemy>().TakeDamage(Random.Range(minAttackDMG, maxAttackDMG));
        }
    }

    #endregion

    private IEnumerator SpeedChange(float time)
    {
        yield return new WaitForSeconds(time);

        //CREATES DELAY IN SPEED SO PLAYER ISN'T ABLE TO ATTACK AND RUN AWAY EASILY
        playerController.speed = newSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
