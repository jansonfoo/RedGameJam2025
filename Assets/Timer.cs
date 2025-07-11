using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer = 180f;
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            checklose();

        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TriggerPenalty(float penaltyTime)
    {
        timer -= penaltyTime;
        checklose();
        Debug.Log("penalty applied");
        
    }
    public void checklose()
    {
        if (timer < 0f)
        {
            timer = 0f;
            Debug.Log("No more time lah");
        }
    }
}
