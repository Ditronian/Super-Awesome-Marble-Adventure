using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour
{
    public Player player;
    public Rigidbody2D rb;
    public CircleCollider2D playerCollider;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    public float speedX = 0.0f;
    public float speedY = 0.0f;
    public float maxSpeedX = 10000f;
    public float acceleration = 10f;
    public bool hasJumped = false;
    public bool isGrounded = false;
    public float jumpSpeed = 100f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        checkIfGrounded();
        playerJump();

        // Check if Back was pressed this frame
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
    }

    void FixedUpdate()
    {
        playerRoll();
    }

    //Controls player X movement
    private void playerRoll()
    {
        if (player.isMovementLocked) return;

        if (Application.platform == RuntimePlatform.Android)
        {
            Vector2 dir = new Vector2(0, 0);
            dir.x = Input.acceleration.x;

            // clamp acceleration vector to the unit sphere
            if (dir.sqrMagnitude > 1) dir.Normalize();

            // Move object
            Vector2 direction = new Vector2();
            if (dir.x > 0) direction = Vector2.right;
            else if (dir.x < 0) direction = Vector2.left;

            rb.AddForce(direction * acceleration);
            
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) rb.AddForce(Vector2.left * acceleration);
            if (Input.GetKey(KeyCode.RightArrow)) rb.AddForce(Vector2.right * acceleration);
        }
    }

    //Controls player y movement
    private void playerJump()
    {
        //if (player.isMovementLocked) return;

        if (Application.platform == RuntimePlatform.Android)
        {
            //Jump Control on Touch Begin
            if (Input.touchCount > 0 && isGrounded && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //If Player is not mid-jump and is trying to jump
                rb.AddForce(Vector2.up * jumpSpeed);
                hasJumped = true;
            }
        }
        else
        {
            //If Player is not mid-jump and is trying to jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpSpeed);
                hasJumped = true;
            }
        }
    }

    //Checks if the Player is on the Ground, and resets hasJumped if appropriate
    private void checkIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.5f, groundLayer);
        if (isGrounded && hasJumped) hasJumped = false;
    }


    
}