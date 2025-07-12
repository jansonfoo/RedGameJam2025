using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsSwitch : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject tutorialPanel;
    public GameObject gameSortPanel;

    public void OpenTutorial()
    {
        mainMenuPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }

    public void StartGame()
    {
        tutorialPanel.SetActive(false);
        gameSortPanel.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
