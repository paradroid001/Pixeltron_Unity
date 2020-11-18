using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;
using TMPro;

public class CollectorGameManager : MobileGameManager
{
    public Transform topExtent;
    public Transform bottomExtent;
    public Transform leftExtent;
    public Transform rightExtent;

    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject gameOverUI;
    

    int playerScore;
    int highScore;
    public string menuSceneName;
    public string gameSceneName;
    public TextMeshProUGUI gameUIScoreText;
    public TextMeshProUGUI gameOverUIScoreText;
    public TextMeshProUGUI gameOverUIHighScoreText;
    


    
    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        highScore = 0;
        PositionScreenExtents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static CollectorGameManager GetInstance()
    {
        return (CollectorGameManager)CollectorGameManager.instance;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        Debug.Log("Score: " + playerScore);
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
        gameOverUIScoreText.text = "Score: " + playerScore;
        gameOverUIHighScoreText.text = "Highscore: " + highScore;
    }

    public override void OnSceneChanged()
    {
        Debug.Log("CurrentSceneName is " + currentSceneName);
        if (currentSceneName == gameSceneName)
        {
            Debug.Log("Game Scene was run");
            //initialise game scene here
            playerScore = 0;
            AddScore(0);
            menuUI.SetActive(false);
            gameOverUI.SetActive(false);
            gameUI.SetActive(true);
        }
        else if (currentSceneName == menuSceneName)
        {
            //initialise menu here.
            gameOverUI.SetActive(false);
            menuUI.SetActive(true);
            gameUI.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneTransition(gameSceneName);
    }

    public void GoToMenu()
    {
        SceneTransition(menuSceneName);
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        if (playerScore > highScore)
        {
            highScore = playerScore;
        }
        gameUIScoreText.text = "Score: " + playerScore;
    }

    void PositionScreenExtents()
    {
        Vector2 bl = Camera.main.ScreenToWorldPoint( new Vector2(0.0f,0.0f) );
        Vector2 tr = Camera.main.ScreenToWorldPoint( new Vector2(Screen.width, Screen.height) );    
        float midx = bl.x + ( (Mathf.Abs(bl.x) + Mathf.Abs(tr.x) ) / 2.0f);
        float midy = bl.y + ( (Mathf.Abs(bl.y) + Mathf.Abs(tr.y) ) / 2.0f); 
        topExtent.transform.position = new Vector2(midx, tr.y);
        bottomExtent.transform.position = new Vector2(midx, bl.y);
        leftExtent.transform.position = new Vector2(bl.x, midy);
        rightExtent.transform.position = new Vector2(tr.x, midy);
    }
}
