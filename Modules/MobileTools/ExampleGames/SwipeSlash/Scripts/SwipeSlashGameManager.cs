using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;
using TMPro;

public class SwipeSlashGameManager : MobileGameManager
{
    public TextMeshProUGUI scoreText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static SwipeSlashGameManager GetInstance()
    {
        return (SwipeSlashGameManager)SwipeSlashGameManager.instance;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}
