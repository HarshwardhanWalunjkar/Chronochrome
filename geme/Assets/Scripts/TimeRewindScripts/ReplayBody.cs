using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReplayBody : MonoBehaviour
{
    bool isReplaying;

    public float recordTime = 10f;

    List<PointInTime> pointsInTimeReplay;
    List<PointInTime> pointsOutTimeReplay;
    private PointInTime pointInTimeReplay;
    private PointInTime pointOutTimeReplay;
    private PlayerInputActions playerControls;
    private InputAction replay;
    private Rigidbody2D rb;
    private bool switch_motion = true;
    private bool note_switch;
    private float replay_count = 0f;
    public bool isActive;
    internal bool was_moved = false;

    // Use this for initialization
    void Start()
    {
        isReplaying = false;
        isActive = false;
        pointsInTimeReplay = new List<PointInTime>();
        pointsOutTimeReplay = new List<PointInTime>();
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
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
    void Update()
    {
        if (isActive || was_moved)
        {
            if (replay.WasPressedThisFrame())
            {
                Debug.Log("REPLAYING NOW");
                StartReplay();
            }
        }
    }

    void FixedUpdate()
    {
        if (isReplaying)
        {   
            if (replay_count > 5)
            {
                StopReplay();
            }
            note_switch = switch_motion;
            Replay();
            if (note_switch != switch_motion)
            {
                replay_count++;
            }
        }
        else if (isActive)
        {
            Record();
        }
    }

    void Replay()
    {
        //Debug.Log("Replayingggggg");

        if(pointsInTimeReplay.Count > 0 && switch_motion)
        {
            switch_motion = true;
            //Debug.Log("Part 1");
            pointInTimeReplay = pointsInTimeReplay[0]; 
            //Debug.Log("Position: " + pointInTimeReplay.position + " Rotation: " + pointInTimeReplay.rotation);
            rb.transform.position = pointInTimeReplay.position;
            rb.transform.rotation = pointInTimeReplay.rotation;
            pointsOutTimeReplay.Insert(0, pointsInTimeReplay[0]);
            pointsInTimeReplay.RemoveAt(0);
        }
        else if(pointsOutTimeReplay.Count > 0)
        {
            switch_motion = false;
            //Debug.Log("Part 2");
            pointOutTimeReplay = pointsOutTimeReplay[0];
            //Debug.Log("Position: " + pointOutTimeReplay.position + " Rotation: " + pointOutTimeReplay.rotation);  
            rb.transform.position = pointOutTimeReplay.position;
            rb.transform.rotation = pointOutTimeReplay.rotation;
            pointsInTimeReplay.Insert(0, pointsOutTimeReplay[0]);
            pointsOutTimeReplay.RemoveAt(0);
        }
        else
        {
            switch_motion = true;
        }
    }

    void Record()
    {
        if (pointsInTimeReplay.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTimeReplay.RemoveAt(pointsInTimeReplay.Count - 1);
        }
        //Debug.Log("RECORDING");
        pointsInTimeReplay.Insert(0, new PointInTime(rb.transform.position, rb.transform.rotation));

    }

    public void StartReplay()
    {
        isReplaying = true;
        rb.isKinematic = true;
    }
    public void StopReplay()
    {
        isReplaying = false;
        rb.isKinematic = false;
        replay_count = 0f;
        pointsInTimeReplay.Clear();
        pointsOutTimeReplay.Clear();
    }
}
