using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject deathMenuUI;

    private int newHealth;



 

    public void Update()
    {
        newHealth = FindObjectOfType<PlayerMovement>().GetHealth();

        if (newHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;  //freeze game
            GameIsPaused = true;
        }
    }





    public void LoadMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        deathMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }

}
