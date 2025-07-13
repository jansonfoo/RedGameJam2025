using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public void Pause()
    {
        pauseMenu.SetActive(true);
    }

    public void Home()
    {

    }

    public void Rresume()
    {
        pauseMenu.SetActive(false);
    }

    public void Restart()
    {
        
    }
}
