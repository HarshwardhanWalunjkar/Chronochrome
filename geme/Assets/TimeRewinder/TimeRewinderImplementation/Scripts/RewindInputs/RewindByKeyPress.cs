using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///  Example how to rewind time with key press
/// </summary>
public class RewindByKeyPress : MonoBehaviour
{
    bool isRewinding = false;
    [SerializeField] float rewindIntensity = 0.02f;          //Variable to change rewind speed
    [SerializeField] AudioSource rewindSound;
    float rewindValue = 0;
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
    void FixedUpdate()
    {
        if(rewind.WasPressedThisFrame())                     //Change keycode for your own custom key if you want
        {
            Debug.Log("REWINDING");
            rewindValue += rewindIntensity;                 //While holding the button, we will gradually rewind more and more time into the past

            if (!isRewinding)
            {
                Debug.Log("Rewind Initiated");
                RewindManager.Instance.StartRewindTimeBySeconds(rewindValue);
                //rewindSound.Play();
            }

            else
            {
                if(RewindManager.Instance.HowManySecondsAvailableForRewind>rewindValue)      //Safety check so it is not grabbing values out of the bounds
                    RewindManager.Instance.SetTimeSecondsInRewind(rewindValue);
            }
            isRewinding = true;
        }
        else
        {
            if(isRewinding)
            {
                RewindManager.Instance.StopRewindTimeBySeconds();
                //rewindSound.Stop();
                rewindValue = 0;
                isRewinding = false;
            }
        }
    }
}
