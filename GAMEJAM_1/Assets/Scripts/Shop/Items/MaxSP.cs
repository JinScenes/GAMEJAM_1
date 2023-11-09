using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSP : MonoBehaviour
{
    [SerializeField] private int SpeedIncreaseValue;

    public void Use(GameObject player)
    {
        player.GetComponent<PlayerControler>().speed += 5;
    }
}
