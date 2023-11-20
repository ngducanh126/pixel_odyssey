using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    private void Awake()
    {
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            PauseGame(!PauseMenu.activeInHierarchy);
        }
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Menu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void PauseScreen(){
        PauseGame(!PauseMenu.activeInHierarchy);
    }
    public void PauseGame(bool status)
    {
        PauseMenu.SetActive(status);

        //When pause status is true change timescale to 0 (time stops)
        //when it's false change it back to 1 (time goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
}