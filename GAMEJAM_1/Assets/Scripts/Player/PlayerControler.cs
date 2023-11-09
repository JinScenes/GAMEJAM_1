using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [HideInInspector]
    public Vector2 movement;

    [Range(0, 20)] public float speed;

    private float timeBetweenStep;
    private float sinceLastStep;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementFunction();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private void MovementFunction()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        sinceLastStep += Time.deltaTime;

        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("Speed", movement.sqrMagnitude);

            timeBetweenStep = 0.2f;

            if (sinceLastStep > timeBetweenStep)
            {
                sinceLastStep = 0f;
            }
        }
        else
        {
            anim.SetFloat("Speed", movement.sqrMagnitude);
            rb.velocity = Vector2.zero;
        }

        movement = new Vector2(movement.x, movement.y);
    }
}
