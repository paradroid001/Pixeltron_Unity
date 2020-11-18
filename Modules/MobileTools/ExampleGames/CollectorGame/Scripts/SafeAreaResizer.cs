using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeAreaResizer : MonoBehaviour
{
    public Canvas rootCanvas;
    RectTransform panelSafeArea;
    Rect currentSafeArea = new Rect(); //to detect safe area changes
    ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation; //to detect orientation changes

    // Start is called before the first frame update
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();
        //store initial values of safe area and orientation
        currentSafeArea = Screen.safeArea;
        currentOrientation = Screen.orientation;
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        //If this object doesn't have a recttransform component, 
        //or if we don't have a canvas, get out!
        if (panelSafeArea == null || rootCanvas == null)
            return;

        //Get the current safe area.
        Rect safeArea = Screen.safeArea;
        //Calculate bottom left and top right of safe area.
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
    
        //this normalises x and y to be related to the width / height of the screen.
        anchorMin.x /= rootCanvas.pixelRect.width;
        anchorMin.y /= rootCanvas.pixelRect.height;
        anchorMax.x /= rootCanvas.pixelRect.width;
        anchorMax.y /= rootCanvas.pixelRect.height;
        
        //now set the anchors of the panel safe area.
        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
        //update the current orientation and safe area values.
        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.orientation != currentOrientation || Screen.safeArea != currentSafeArea)
        {
            Debug.Log("Applying Safe Area");
            ApplySafeArea();
        }
    }
}
