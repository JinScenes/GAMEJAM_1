using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxATK : MonoBehaviour
{
    [SerializeField] private int damageBuff;

    public void Use(GameObject player)
    {
        player.GetComponent<PlayerCombat>().minAttackDMG += damageBuff;
    }
}
