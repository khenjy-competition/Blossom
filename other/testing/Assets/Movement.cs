using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float HorizontalMovement;
    public float speed = 5;

    public Camera mainCamera;
    private bool dontMoveLeft = false;
    private bool dontMoveRight = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        moveLeft = false;
        moveRight = false;
    }

   
    public void OnPointerDownLeft()
    {
        // hold
        moveLeft = true;
    }
    public void OnPointerUpLeft()
    {
        // unhold
        moveLeft = false;
    }

    public void OnPointerDownRight()
    {
        // hold
        moveRight= true;
    }
    public void OnPointerUpRight()
    {
        // unhold
        moveRight= false;
    }
    // Update is called once per frame
    void Update()
    {
        MovementPlayer();
        CheckPosition();
    }

    private void MovementPlayer()
    {
        if (moveLeft)
        {
            // check position
            if (dontMoveLeft)
            {
                moveLeft = false;
                return;
            }

            HorizontalMovement = -speed;
        }
        else if(moveRight)
        {
            // check position
            if (dontMoveRight)
            {
                moveRight = false;
                return;
            }

            HorizontalMovement = speed;
        }
        else
        {
            HorizontalMovement = 0;
        }
    }

    private void CheckPosition()
    {
        Vector3 squarePosition = mainCamera.WorldToViewportPoint(transform.position);
        Debug.Log("squarePosition: " + squarePosition);
        Vector2 squarePositionVector2 = new Vector2(squarePosition.x, squarePosition.y);
        
        if (squarePosition.x < 0.1f)
        {
            dontMoveLeft = true;
        }
        else if (squarePosition.x > 0.9f)
        {
            dontMoveRight = true;
        }
        else
        {
            dontMoveLeft = false;
            dontMoveRight = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(HorizontalMovement, rb.velocity.y);

    }

}