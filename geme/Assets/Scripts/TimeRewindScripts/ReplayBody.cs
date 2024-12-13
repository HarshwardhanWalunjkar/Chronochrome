using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReplayBody : MonoBehaviour
{
    bool isReplaying;

    private float recordTimeReplay = 5f;

    public GameObject ReplayObjectPrefab;
    private GameObject ReplayObject;
    private Rigidbody2D rb_clone;

    internal List<PointInTime> pointsInTimeReplay;
    private PointInTime point;
    private int index = 0;
    private int direction = 1;
    private int replay_count = 0;

    private PlayerInputActions playerControls;
    private InputAction replay;

    private Rigidbody2D rb;

    private int count;
    private int sec_count = 0;

    private bool isActive = true;
    private bool mov_flag = true;

    private Vector2 init_pos;
    private Quaternion init_rot;

    private Square_Test sq_mov;

    // Use this for initialization
    void Start()
    {
        isReplaying = false;
        pointsInTimeReplay = new List<PointInTime>();
        rb = GetComponent<Rigidbody2D>();
        sq_mov = GetComponent<Square_Test>();  
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        replay = playerControls.Player.Replay;
        replay.Enable();
    }

    private void OnDisable()
    {
        replay.Disable();
    }

    // Update is called once per frame
    //Only check for if Replay key is being pressed
    void Update()
    {
        if (!isReplaying)
        {
            if (replay.WasPressedThisFrame())
            {
                Debug.Log("REPLAYING NOW");
                StartReplay();
            }
        }
    }



    //FixedUpdate Replays every physics calculation to move the object along the desired path also this disables the movement of the player square for a second
    void FixedUpdate()
    {

        if (isReplaying)
        {
            if (mov_flag)
            {
                sec_count++;
                if (sec_count > Mathf.Round(1f / Time.fixedDeltaTime))
                {
                    sq_mov.OnEnable();
                    Debug.Log("Movement Enabled");
                    mov_flag = false;
                }
            }
            Replay();
        }
        else if (isActive)
        {
            Record();
        }
    }



   
    //SpawnReplayInstance will spawn our replay object prefab that will be replaying our players motions;
    void SpawnReplayInstance()
    {
        if (!isReplaying)
        {
            count = pointsInTimeReplay.Count;
            init_pos.x = pointsInTimeReplay[count - 1].position.x;
            init_pos.y = pointsInTimeReplay[count - 1].position.y;
            init_rot = Quaternion.identity;
            ReplayObject = Instantiate(ReplayObjectPrefab, init_pos, init_rot);
            ColorManager.clone_spawn = 1;
            rb_clone = ReplayObject.GetComponent<Rigidbody2D>(); 
            rb_clone.isKinematic = true;  
        }
    }


    //Logic for replaying the movements, here the replay count of 3 makes sure it only replays 3 times;
    void Replay()
    {
        point = pointsInTimeReplay[index];
        rb_clone.transform.SetPositionAndRotation(point.position, point.rotation);
        index += direction;
        if (index == pointsInTimeReplay.Count - 1)
        {
            direction *= -1;

        }
        else if (index == 0)
        {
            direction *= -1;
            replay_count++;
        }
        if(replay_count == 5)
        {
            StopReplay();
        }
    }




    //Records the movements when not replaying 
    void Record()
    {
        if (pointsInTimeReplay.Count > Mathf.Round(recordTimeReplay / Time.fixedDeltaTime))
        {
            pointsInTimeReplay.RemoveAt(pointsInTimeReplay.Count - 1);
        }
        //Debug.Log("RECORDING");
        pointsInTimeReplay.Insert(0, new PointInTime(rb.transform.position, rb.transform.rotation));

    }



    //Starts the replay, responsible for disabling player square movement, spawning replay object prefab and then replaying
    public void StartReplay()
    {
        sq_mov.OnDisable();
        Debug.Log("Movement disabled");
        SpawnReplayInstance();
        isReplaying = true;
    }


    //Responsible for stopping the replay
    public void StopReplay()
    {
        rb_clone.isKinematic = false;
        isReplaying = false;
        replay_count = 0;
        pointsInTimeReplay.Clear();
    }
}
