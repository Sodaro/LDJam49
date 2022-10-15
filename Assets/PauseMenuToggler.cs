using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggler : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        if (PlayerPrefs.GetInt("firstTime") == 0)
            SetPause();
    }


    void SetPause()
    {
        pauseMenu.SetActive(true);

        if (pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    void TogglePause()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);

        if (pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
        else
            Time.timeScale = 1;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
            PlayerPrefs.SetInt("firstTime", 1);
        }
    }
}
