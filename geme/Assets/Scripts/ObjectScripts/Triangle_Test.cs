using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Triangle_Test : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerInputActions TriangleControls;
    private InputAction move;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 moveForce = Vector2.zero;
    private bool TriangleMoveActive = false;
    private SpriteRenderer TriangleSprite;
    private ReplayBody ReplayBodyInstance;
    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TriangleSprite = GetComponent<SpriteRenderer>();
        ReplayBodyInstance = GetComponent<ReplayBody>();
    }



    private void Awake()
    {
        TriangleControls = new PlayerInputActions();
    }



    private void OnEnable()
    {
        move = TriangleControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }


    private void FixedUpdate()
    {
        if (TriangleMoveActive)
        {
            moveDirection = move.ReadValue<Vector2>();
            //Debug.Log(moveDirection);
            moveForce = new Vector2(moveDirection.x, 0f);
            rb.AddForce(moveForce * 15);
        }
    }

    private void OnMouseDown()
    {
        TriangleMoveActive = !TriangleMoveActive;
        if (TriangleMoveActive) { 
            TriangleSprite.color = Color.cyan;
            ReplayBodyInstance.was_moved = true;
        }
        else
        {
            TriangleSprite.color = Color.red;
        }
        ReplayBodyInstance.isActive = !ReplayBodyInstance.isActive;
    }
}
