using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // For detecting objects in the Ground layer (assigned in inspector)
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform jumpPoint;
    [SerializeField] LayerMask EndLayer;

    public Win win;
    // rigidbody for physics
    Rigidbody2D rb;

    // Raycast to determine if the player is on the ground
    RaycastHit2D hit;
    float raycastLength = 0.5f;

    // movement parameters
    float moveSpeed = 300f;
    float moveDirection = 0;
    bool jump;
    float jumpHeight = 3f;
    public bool facingRight = true;

    // called at start of game
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // detects movement inputs (left, right, jump)
        moveDirection = Input.GetAxisRaw("Horizontal");
        jump = Input.GetButton("Jump");

        // flips the player depending on where they're facing and which direction they want to go
        if(moveDirection > 0 && !facingRight)
        {
            transform.Rotate(0, 180, 0);
            facingRight = true;
        }
        
        if(moveDirection < 0 && facingRight)
        {
            transform.Rotate(0, 180, 0);
            facingRight = false;
        }

        if(Input.GetKeyDown(KeyCode.E) && isAtEnd()){
            win.Setup();
        }
    }

    private void FixedUpdate()
    {
        // moves the player left/right based on input
        rb.velocity = new Vector2(moveDirection * moveSpeed * Time.deltaTime, rb.velocity.y);

        // lets the player jump and makes sure the player is on the ground before jumping
        if (jump && isGrounded())
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
    }

    bool isGrounded()
    {
        // casts a line downwards from the player's position to detect if it hits the ground
        hit = Physics2D.Raycast(jumpPoint.position, Vector2.down, raycastLength, groundLayer);

        if (hit.collider != null) // checks if something in ground layer is hit
        {
            return true;
        }
        return false;
    }

    bool isAtEnd()
    {
        hit = Physics2D.Raycast(jumpPoint.position, Vector2.down, raycastLength, EndLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
