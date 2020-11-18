using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorGameCollectable : MonoBehaviour
{
    public GameObject coinPrefab;
    public int scoreValue = 10;
    // Start is called before the first frame update
    void Start()
    {
        Transform top = CollectorGameManager.GetInstance().topExtent;
        Transform bottom = CollectorGameManager.GetInstance().bottomExtent;
        Transform left = CollectorGameManager.GetInstance().leftExtent;
        Transform right = CollectorGameManager.GetInstance().rightExtent;
        
        float x = Random.Range(left.position.x, right.position.x);
        float y = Random.Range(bottom.position.y, top.position.y);

        transform.position = new Vector2(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            CollectorGameManager.GetInstance().AddScore(scoreValue);
            Instantiate(coinPrefab);
            Destroy(gameObject);
        }
    }
}
