using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;

public class TiltGameManager : MobileGameManager
{
    public GameObject mainMenuUI;
    public GameObject gameUI;
    public GameObject gameOverUI;
    public string menuSceneName;
    public string gameSceneName;
    

    public override void OnSceneChanged()
    {
        if (currentSceneName == gameSceneName)
        {
            //you are in the game scene
            mainMenuUI.SetActive(false);
            gameUI.SetActive(true);
            gameOverUI.SetActive(false);
        }
        else if  (currentSceneName == menuSceneName)
        {
            //you are in the menu scene
            mainMenuUI.SetActive(true);
            gameUI.SetActive(false);
            gameOverUI.SetActive(false);
        }   
    }


}
