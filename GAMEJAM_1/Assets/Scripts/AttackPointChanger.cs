using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointChanger : MonoBehaviour
{
    SkeletonController skeletonController;

    void Start()
    {
        skeletonController = GetComponentInParent<SkeletonController>();

        Vector2 attackPos = transform.localPosition;

        //FACING DOWN
        if (skeletonController.movement.y < 0)
        {
            attackPos.x = 0f;
            attackPos.y = -0.296f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //attackPos is the localposition
        Vector2 attackPos = transform.localPosition;

        //float distanceX = Mathf.Abs(transform.parent.transform.position.x - enemyAi.movement.x);
        //float distanceY = Mathf.Abs(transform.parent.transform.position.y - enemyAi.movement.y);

        //Debug.Log("Distance x" + distanceX + "Distance y" + distanceY);

        //FACING DOWN
        if (skeletonController.movement.y < 0 && skeletonController.movement.y < skeletonController.movement.x)
        {
            attackPos.x = 0f;
            attackPos.y = -0.9f;
        }

        //FACING UP
        if (skeletonController.movement.y > 0 && skeletonController.movement.y > skeletonController.movement.x)
        {
            attackPos.x = 0f;
            attackPos.y = 0.9f;
        }

        //FACING LEFT
        if (skeletonController.movement.x < 0 && skeletonController.movement.x < skeletonController.movement.y)
        {
            attackPos.x = -0.35f;
            attackPos.y = -0.1f;
        }

        //FACING RIGHT
        if (skeletonController.movement.x > 0 && skeletonController.movement.x > skeletonController.movement.y)
        {
            attackPos.x = 0.35f;
            attackPos.y = -0.1f;
        }

        //UPDATES POSITIONS OF THE VECTORS
        transform.localPosition = attackPos;
    }
}
