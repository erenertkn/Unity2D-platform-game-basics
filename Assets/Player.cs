using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D myrigidbody;
    Animator myAnim;
    Collider2D myCollider2D;

    float gravityScaleAtStart;
    bool isAlive = true;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        gravityScaleAtStart = myrigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow*5, myrigidbody.velocity.y);
        myrigidbody.velocity = playerVelocity;
        myAnim.SetBool("isRunning", Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon);
    }

    private void Jump()
    {
        if(myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0f, 10f);
                myrigidbody.velocity += jumpVelocity;
            }
        }
    }

    private void FlipSprite()
    {
        if(Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
        }
    }

    private void ClimbLadder()
    {
        if(myCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myrigidbody.gravityScale = 0;
            float controlThrow = Input.GetAxis("Vertical");
            Vector2 climbvelocity = new Vector2(myrigidbody.velocity.x, controlThrow*2f);
            myrigidbody.velocity = climbvelocity;

            myAnim.SetBool("isClimbing", Mathf.Abs(myrigidbody.velocity.y)> Mathf.Epsilon);
        }
        else
        {
            myrigidbody.gravityScale = gravityScaleAtStart;
        }
    }
}
