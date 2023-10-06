using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity;
    public float acceleration = 10;
    public float maxAccleration = 10;

    public float distance = 0;

    public float maxXVelocity = 100;
    public Vector2 velocity;
    public float groundHeight = 10;

    public bool isGrounded = false;
    public float jumpVelocity = 20;
    public bool isHoldingJump = false;

    public float maxHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;

    public float jumpGroundTreshold = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if(isGrounded || groundDistance <= jumpGroundTreshold)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if(!isGrounded)
        {
            if(isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;
                if(holdJumpTimer > maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }
            pos.y += velocity.y * Time.fixedDeltaTime;
            
            if(!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            
            if(pos.y <= groundHeight)
            {
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        distance += velocity.x * Time.fixedDeltaTime;

        if(isGrounded)
        {
            float velocityRatio = velocity.x/maxXVelocity;
            acceleration = maxAccleration * (1-velocityRatio);

            velocity.x += acceleration * Time.fixedDeltaTime;
            
            if(velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

        }

        transform.position = pos;
    }
}
