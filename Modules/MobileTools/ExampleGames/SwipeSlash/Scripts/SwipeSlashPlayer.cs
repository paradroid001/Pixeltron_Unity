using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;

public class SwipeSlashPlayer : MonoBehaviour
{
    LineRenderer lr;
    Collider2D collider;
    Vector2 prevStartPos = new Vector2(0.432432f, 0.4234f); //random
    Vector2 prevEndPos = Vector2.zero;
    public float swipeTimeout = 0.3f;
    float swipeTimer;
    bool slash = false;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        lr = GetComponent<LineRenderer>();
        MobileControls.OnSwipe += OnSwipe;
        slash = false;
        collider.enabled = false;      
    }

    // Update is called once per frame
    void Update()
    {
        if (swipeTimer < swipeTimeout)
        {
            swipeTimer += Time.deltaTime;
            if (slash)
                transform.position = Vector2.Lerp(prevStartPos, prevEndPos, swipeTimer/swipeTimeout);
        }
        else
        {
            slash = false;
            collider.enabled = false;
            DrawSlash(Vector2.zero, Vector2.zero);
        }
    }

    void OnSwipe(SwipeData sd)
    {
        if (sd.posStart != prevStartPos)
        {
            swipeTimer = 0.0f;
            prevStartPos = sd.posStart;
            transform.position = sd.posStart;
            slash = true;
            collider.enabled = true;
        }
        prevEndPos = sd.posCurrent;
        
        DrawSlash(prevStartPos, sd.posCurrent);
    }

    void DrawSlash(Vector2 start, Vector2 end)
    {
        if (start == Vector2.zero && end == Vector2.zero)
        {
            //clear the slash
            lr.positionCount = 0;
        }
        else
        {
            lr.positionCount = 2;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }
    }
}
