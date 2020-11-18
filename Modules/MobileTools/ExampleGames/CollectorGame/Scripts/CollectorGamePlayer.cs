using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;

public class CollectorGamePlayer : Char2D
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        MobileControls.OnTap += OnTap;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        MobileControls.OnTap -= OnTap;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += (Vector3)currentMovement * Time.deltaTime;
        rb.velocity = currentMovement;
    }

    void OnTap(Vector2 tappos)
    {
        //Vector2 wpos = Camera.main.ScreenToWorldPoint(tappos);
        Vector2 dir = (tappos - (Vector2)transform.position).normalized;
        SetMovement(dir);
    }
}
