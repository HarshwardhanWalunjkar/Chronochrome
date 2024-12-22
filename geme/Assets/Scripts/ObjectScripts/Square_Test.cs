using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Square_Test : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private PlayerInputActions SquareControls;
    private InputAction move;
    private InputAction jump;
    private InputAction esc;
    private Vector2 moveDirection = Vector2.zero;
    private float moveSpeed = 7f; // Adjust movement speed here
    private Vector2 jumpForce = new Vector2(0, 5f);
    private bool isGrounded = true; // To check if the object is on the ground
    private Timer timer;
    public int orbGoal;
    private int orbCount = 0;

    public Animator EscScreen;

    private Vector3 m_velocity = Vector3.zero   ;
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        ColorManager.color_code = -1;
    }

    private void Awake()
    {
        SquareControls = new PlayerInputActions();
    }

    internal void OnEnable()
    {
        move = SquareControls.Player.Move;
        jump = SquareControls.Player.Jump;
        esc = SquareControls.Player.Escape;
        move.Enable();
        jump.Enable();
        esc.Enable();
    }

    internal void OnDisable()
    {
        move.Disable();
        jump.Disable();
        esc.Disable();
    }

    private void Update()
    {
        // Jump logic in Update to detect input
        if (jump.WasPressedThisFrame() && isGrounded)
        {
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // Ensure jump happens only when grounded
        
        }
        if (esc.WasPressedThisFrame()) {
            EscScreen.SetBool("Escape", !EscScreen.GetBool("Escape"));
        }
    }

    private void FixedUpdate()
    {
        // Smooth movement in FixedUpdate
        moveDirection = move.ReadValue<Vector2>();
        Vector3 targetVelocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_velocity, m_MovementSmoothing );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object is grounded
        if (collision.gameObject.CompareTag("Tile"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Color and orb handling logic remains unchanged
        if (collision.gameObject.tag == "RedTrigger")
        {
            ColorManager.color_code = 0;
        }
        else if (collision.gameObject.tag == "YellowTrigger")
        {
            ColorManager.color_code = 1;
        }
        else if (collision.gameObject.tag == "GreenTrigger")
        {
            ColorManager.color_code = 2;
        }
        else if (collision.gameObject.tag == "BlueTrigger")
        {
            ColorManager.color_code = 3;
        }
        else if (collision.gameObject.tag == "OrangeTrigger")
        {
            ColorManager.color_code = 4;
        }
        else if (collision.gameObject.tag == "RedLaserTrigger")
        {
            ColorManager.red_disable = (ColorManager.color_code == 0);
        }
        else if (collision.gameObject.tag == "YellowLaserTrigger")
        {
            ColorManager.yellow_disable = (ColorManager.color_code == 1);
        }
        else if (collision.gameObject.tag == "GreenLaserTrigger")
        {
            ColorManager.green_disable = (ColorManager.color_code == 2);
        }
        else if (collision.gameObject.tag == "BlueLaserTrigger")
        {
            ColorManager.blue_disable = (ColorManager.color_code == 3);
        }
        else if (collision.gameObject.tag == "OrangeLaserTrigger")
        {
            ColorManager.orange_disable = (ColorManager.color_code == 4);
        }
        else if (collision.gameObject.tag == "PurpleOrb")
        {
            orbCount++;
            Debug.Log(orbCount);
            Debug.Log(orbGoal);
            collision.gameObject.GetComponent<Animator>().SetBool("Captured", true);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "PurpleTrigger" && orbCount == orbGoal)
        {
            Debug.Log("NextLevel");
            ScenesManager.instance.LoadNextScene();
        }
        else if(collision.gameObject.tag == "GameOver")
        {
            Debug.Log("GameOver");
            ScenesManager.instance.MainMenu();
        }
        else
        {
            return;
        }
    }
}
