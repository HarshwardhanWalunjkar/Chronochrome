using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeBody : MonoBehaviour {

	bool isRewinding = false;

	public float recordTimeRewind = 5f;

	public List<PointInTime> pointsInTime;
    private PlayerInputActions playerControls;
	private InputAction rewind;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody2D>();
	}

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
		rewind = playerControls.Player.Rewind;
		rewind.Enable();
    }

    private void OnDisable()
    {
        rewind.Disable();	
    }

    // Update is called once per frame
    void Update () {
		if (rewind.WasPressedThisFrame())
			StartRewind();
		if (rewind.WasReleasedThisFrame())
			StopRewind();
	}

	void FixedUpdate ()
	{
		if (isRewinding)
			Rewind();
		else
		{
			Record();
		}
	}

	void Rewind ()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		} else
		{
			StopRewind();
		}
		
	}

	void Record()
	{
		if (pointsInTime.Count > Mathf.Round(recordTimeRewind / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}
		//Debug.Log("RECORDING");
		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

	}

	public void StartRewind ()
	{
		isRewinding = true;
		rb.isKinematic = true;
	}

	public void StopRewind ()
	{
		isRewinding = false;
		rb.isKinematic = false;
	}
}
