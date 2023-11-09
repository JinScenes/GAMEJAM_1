using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private int healthBoost = 30;

    public void Use(GameObject player)
    {
        //player.GetComponent<PlayerHealth>().currentHealth += healthBoost;
    }
}
