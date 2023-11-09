using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSP : MonoBehaviour
{
    [SerializeField] private int maxSpeedIncrease = 10;

    public void Use(GameObject player)
    {
        //player.GetComponent<PlayerMovement>().IncreaseMaxSpeed(maxSpeedIncrease);
    }
}
