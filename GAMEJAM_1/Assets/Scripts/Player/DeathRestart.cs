using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathRestart : MonoBehaviour
{
    public GameObject deathUI;

    private void Awake()
    {
        deathUI.SetActive(true);
        GameObject.Find("HP Bar").SetActive(false);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

    }


}
