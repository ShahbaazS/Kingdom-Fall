using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // For detecting objects in the Ground layer (assigned in inspector)
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;

    // rigidbody for physics
    Rigidbody2D rb;
    [SerializeField] Collider2D wallCheck;

    // Raycast to determine if the player is on the ground
    [SerializeField] Transform checkGroundPoint;
    RaycastHit2D hit;
    float raycastLength = 0.5f;

    // movement parameters
    float moveSpeed = 100f;
    bool facingRight = true;
    int moveDirection = 1;

    float leftBound = -100f;
    float rightBound = 100f;
    bool canMove = true;
    public bool isBound = true;

    float currentTime = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetBounds(transform.position.x - 10, transform.position.x + 10);
    }

    private void Update()
    {
        if (isBound)
        {
            CheckGround();
            CheckBounds(leftBound, rightBound);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            // moves the enemy left/right based on direction
            rb.velocity = new Vector2(moveDirection * moveSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    public void SetBounds(float minBound, float maxBound)
    {
        leftBound = Mathf.Min(minBound, maxBound);
        rightBound = Mathf.Max(minBound, maxBound);
    }

    void CheckGround()
    {
        // casts a line downwards from the player's position to detect if it hits the ground
        hit = Physics2D.Raycast(checkGroundPoint.position, Vector2.down, raycastLength, groundLayer);

        if (hit.collider == false || wallCheck.IsTouchingLayers(groundLayer) || wallCheck.IsTouchingLayers(wallLayer)) // checks if something in ground layer is hit
        {
            ChangeDirection();
        }
    }

    void CheckBounds(float minBound, float maxBound)
    {
        if(transform.position.x <= minBound || transform.position.x >= maxBound)
        {
            if(Time.time > currentTime)
            {
                ChangeDirection();
                currentTime = Time.time + 1;
            }
        }
    }

    public void SetMoving(bool ableToMove)
    {
        canMove = ableToMove;
    }

    public void ChangeDirection()
    {
        if (facingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facingRight = false;
            moveDirection = -1;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
            moveDirection = 1;
        }
    }

    public bool isFacingRight()
    {
        return facingRight;
    }
}
