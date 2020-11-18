using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSlashEnemy : MonoBehaviour
{
    Rigidbody2D rb2d;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Vector2 force)
    {
        if (rb2d)
        {
            rb2d.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            SwipeSlashGameManager.GetInstance().AddScore(1);
        }
    }
}
