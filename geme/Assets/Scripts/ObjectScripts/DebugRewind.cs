using UnityEngine;
using UnityEngine.InputSystem;

public class DebugRewind : MonoBehaviour
{

    private PlayerInputActions playerControls;
    private InputAction rewind;

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


    private void Update()
    {
        if (rewind.WasPressedThisFrame())  // Simple input check for testing
        {
            Debug.Log("Rewind initiated");

            // Call the RewindManager methods directly
            RewindManager.Instance.StartRewindTimeBySeconds(5);  // Test with 5 seconds
        }

        else  // Stop rewind
        {
            Debug.Log("Rewind stopped");
            RewindManager.Instance.StopRewindTimeBySeconds();
        }
    }
}

