using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }

        }
    }
    
    //resume game
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; 
        GameIsPaused = false;
    }

    //pause game
    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;  
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  //freeze game
        GameIsPaused = true;
    }

    //load menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    
    //quit game
    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }

}
