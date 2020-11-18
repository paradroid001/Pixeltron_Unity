using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;

public class InfVertManager : MobileGameManager
{
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnSceneChanged()
    {
        Debug.Log("On Scene Changed: " + currentSceneName);
        if (currentSceneName == "InfTopDown" )
        {
            ResizeOrthoCamera();
        }
    }

    public static InfVertManager GetInstance()
    {
        return (InfVertManager)InfVertManager.instance;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
    }
}
