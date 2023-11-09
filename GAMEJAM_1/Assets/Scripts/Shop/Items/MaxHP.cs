using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHP : MonoBehaviour
{
    [SerializeField] private int maxHealthIncrease = 10;

    public void Use(GameObject player)
    {
        //player.GetComponent<PlayerHealth>().IncreaseMaxHP(maxHealthIncrease);
    }
}
