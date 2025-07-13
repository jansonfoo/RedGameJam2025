using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicAdvance : MonoBehaviour
{
    public Animator animator;
    public int totalPanels = 8;
    public string nextSceneName = "MainMenu";

    int currentPanel = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentPanel < totalPanels)
            {
                animator.SetTrigger("Next");
                currentPanel++;
            }
            else
            {
                LoadNextScene();
            }
        }
    }

    void LoadNextScene()
    {
        Debug.Log("Loading next scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
