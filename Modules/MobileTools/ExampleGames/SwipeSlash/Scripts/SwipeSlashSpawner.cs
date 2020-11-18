using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSlashSpawner : MonoBehaviour
{
    public float XRangeMin = -0.5f;
    public float XRangeMax = 0.5f;
    public float yForce = 3.0f;
    public SwipeSlashEnemy[] prefabs;
    public float spawnFrequency = 0.5f;

    float spawnTimer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < spawnFrequency)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnTimer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        SwipeSlashEnemy prefab = prefabs[Random.Range(0, prefabs.Length)];
        SwipeSlashEnemy enemy = Instantiate(prefab, transform.position, Quaternion.identity);
        enemy.Init(new Vector2(Random.Range(XRangeMin, XRangeMax), yForce));
    }
}
