﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfVertObjectDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
