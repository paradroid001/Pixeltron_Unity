using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixeltron.MobileTools;

public class InfVertPlayerCharacter : Char2D
{
    public float jumpPower = 1.0f;
    bool grounded;
    Rigidbody2D rb2d;
    public Transform groundRayCast;
    public LayerMask whatIsGround;
    public Vector2 groundDetectorSize;
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        rb2d = GetComponent<Rigidbody2D>();
        MobileControls.OnSwipe += OnSwipe;
        MobileControls.OnTap += OnTap;
    }

    void OnDestroy()
    {
        MobileControls.OnSwipe -= OnSwipe;
        MobileControls.OnTap -= OnTap;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(currentMovement.x, rb2d.velocity.y);
        
        //if falling, check for ground.
        if (rb2d.velocity.y < 0)
        {
            Collider2D col = Physics2D.OverlapBox(groundRayCast.position, groundDetectorSize, 0, whatIsGround);
            if (col == null)
            {
                grounded = false;
            }
            else
            {
                //Debug.Log(col.name);
                grounded = true;
            }
        }
    }

    void OnDrawGizmos() 
    {
        Gizmos.DrawWireCube(groundRayCast.position, new Vector3(groundDetectorSize.x, groundDetectorSize.y, 0));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Fill in for enemies, powerups, etc
    }

    void Jump()
    {
        if (grounded)
        {
            grounded = false;
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void OnSwipe(SwipeData sdata)
    {
        if (sdata.dir == Direction.Dir.RIGHT)
        {
            SetMovement(Vector2.right);
        }
        else if (sdata.dir == Direction.Dir.LEFT)
        {
            SetMovement(Vector2.left);
        }
    }

    void OnTap(Vector2 pos)
    {
        Jump();
    }

    
}
