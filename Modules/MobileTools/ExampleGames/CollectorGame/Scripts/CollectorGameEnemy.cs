using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;

public class CollectorGameEnemy : Char2D
{
    Direction.Dir dir;
    Transform top, bottom, left, right;
    public GameObject enemyPrefab;
    bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        //Build a list of directions.
        Direction.Dir[] dirs = new Direction.Dir[4];
        dirs[0] = Direction.Dir.UP;
        dirs[1] = Direction.Dir.DOWN;
        dirs[2] = Direction.Dir.LEFT;
        dirs[3] = Direction.Dir.RIGHT;

        isAlive = true;

        //This gives us a random direction
        dir = dirs[Random.Range(0, dirs.Length)];

        //Get the extents
        top = CollectorGameManager.GetInstance().topExtent;
        bottom = CollectorGameManager.GetInstance().bottomExtent;
        left = CollectorGameManager.GetInstance().leftExtent;
        right = CollectorGameManager.GetInstance().rightExtent;

        //Decide start pos and movement.
        float x = Random.Range(left.position.x, right.position.x);
        float y = Random.Range(bottom.position.y, top.position.y);
        switch(dir)
        {
            case Direction.Dir.UP:
            {
                y = bottom.position.y;
                break;
            }
            case Direction.Dir.DOWN:
            {
                y = top.position.y;
                break;
            }
            case Direction.Dir.LEFT:
            {
                x = right.position.x;
                break;
            }
            case Direction.Dir.RIGHT:
            {
                x = left.position.x; 
                break;
            }
            default:
            {
                y = bottom.position.y; //up by default.
                break;
            }        
        }
        transform.position = new Vector2(x, y);
        SetMovement(Direction.DirToVec2(dir));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.position += (Vector3)currentMovement * Time.deltaTime;
            if (dir == Direction.Dir.UP || dir == Direction.Dir.DOWN)
            {
                if ( transform.position.y < bottom.position.y || transform.position.y > top.position.y )
                {
                    Die();
                }
            }
            else if (dir == Direction.Dir.LEFT || dir == Direction.Dir.RIGHT)
            {
                if ( transform.position.x < left.position.x || transform.position.x > right.position.x )
                {
                    Die();
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            Destroy(col.gameObject);
            CollectorGameManager.GetInstance().GameOver();
        }
    }

    void Die()
    {
        isAlive = false;
        Instantiate(enemyPrefab);
        Destroy(gameObject);
        
    }
}
