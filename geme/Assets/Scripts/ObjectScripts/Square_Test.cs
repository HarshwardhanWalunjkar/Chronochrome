using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Square_Test : MonoBehaviour
{

    private Rigidbody2D rb;
    private PlayerInputActions SquareControls;
    private InputAction move;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 moveForce = Vector2.zero;
    private bool SquareMoveActive = false;
    private TimeBody timeBodyInstance;
    private SpriteRenderer SquareSprite;

    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SquareSprite = GetComponent<SpriteRenderer>();
        timeBodyInstance = GetComponent<TimeBody>();
    }



    private void Awake()
    {
        SquareControls = new PlayerInputActions();
    }



    private void OnEnable()
    {
        move = SquareControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }


    private void FixedUpdate()
    {
        if (SquareMoveActive)
        {
            moveDirection = move.ReadValue<Vector2>();
            //Debug.Log(moveDirection);
            moveForce = new Vector2(moveDirection.x, 0f);
            rb.AddForce(moveForce * 10);
        }
    }

    private void OnMouseDown()
    {
        SquareMoveActive = !SquareMoveActive;
        if (SquareMoveActive) { 
            SquareSprite.color = Color.green;
        }
        else
        {
            SquareSprite.color= Color.red;
        }
        timeBodyInstance.isActive = !timeBodyInstance.isActive;
    }
}
