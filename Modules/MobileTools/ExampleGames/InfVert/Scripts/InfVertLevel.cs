using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfVertLevel : MonoBehaviour
{
    public GameObject camObject;
    public Camera gameCamera;
    public float levelSpeed = 1.0f;
    public Transform spawnPoint;
    public GameObject[] screens;
    public float scrollBetweenScreens = 20;
    float scroll;
    
    void Awake()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        NewScreen();
    }

    // Update is called once per frame
    void Update()
    {
        float dy = levelSpeed * Time.deltaTime;
        camObject.transform.position += Vector3.up * dy;
        scroll -= dy;
        if (scroll < 0)
        {
            NewScreen();
        }
    }

    void NewScreen()
    {
        scroll = scrollBetweenScreens;
        GameObject screenPrefab = screens[Random.Range(0, screens.Length)];
        GameObject screen = Instantiate(screenPrefab, spawnPoint.position, Quaternion.identity);
        screen.transform.SetParent(transform);
    }
}
