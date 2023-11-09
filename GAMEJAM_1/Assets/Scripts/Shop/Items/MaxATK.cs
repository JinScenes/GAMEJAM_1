using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxATK : MonoBehaviour
{
    [SerializeField] private int maxAtkIncrease = 10;

    public void Use(GameObject player)
    {
        //player.GetComponent<PlayerAttack>().IncreaseMaxAttack(maxAtkIncrease);
    }
}
