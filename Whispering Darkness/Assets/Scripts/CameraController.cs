using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Mouse Settings")]
    public Transform playerBody; // Reference to the player's body
    public float mouseSensitivityX = 100f; // Mouse horizontal sensitivity
    public float mouseSensitivityY = 100f; // Mouse vertical sensitivity
    private float smoothTime = 0.1f; // Smoothing time
    public bool lockCursor = true; // Whether to lock the cursor to the center of the screen
    private bool isFrozen = false; // Flag for freezing the camera
    private Vector3 currentMouseDelta; // Current mouse delta for smoothing
    private Vector3 currentMouseDeltaVelocity; // Mouse delta speed for smoothing

    [Header("Camera Limits")]
    private float minAngle = -90f; // Minimum angle for camera rotation
    private float maxAngle = 90f; // Maximum angle for camera rotation

    [Header("Walking Bobbing Settings")]
    private float bobbingSpeed = 6f; // Speed of the walking bobbing effect
    private float bobbingAmount = 0.1f; // Amount of the walking bobbing effect

    [Header("Footstep Sounds")]
    public AudioClip[] footstepSounds; // Array of footstep sounds
    private float stepInterval = 0.2f; // Interval between footstep sounds
    private float xRotation = 0f; // Current rotation around the X-axis
    private float defaultPosY = 1f; // Default Y position of the camera
    private float timer = 0f; // Timer for walking bobbing effect
    private AudioSource audioSource; // Reference to the AudioSource component
    private float stepTimer = 0f; // Timer for footstep sounds

    void Start()
    {
        // Initialize the cursor state
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
            Cursor.visible = false; // Hide the cursor
        }
        
         // Ensure the camera starts looking forward
        xRotation = 1f; // Set initial rotation to zero
        defaultPosY = transform.localPosition.y; // Save the default Y position of the camera

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Apply initial rotation to the camera
        
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the camera
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Сглаживание движения мыши
        Vector3 targetMouseDelta = new Vector3(mouseX, mouseY, 0);
        currentMouseDelta = Vector3.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, smoothTime);


        // Rotate player body left and right
        playerBody.Rotate(Vector3.up * currentMouseDelta.x);

        // Adjust camera up and down
        xRotation -= currentMouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle);
        
        // Apply rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (!isFrozen)
        {
            // Apply walking bobbing effect
            ApplyWalkingBobbing();
            PlayFootstepSounds();
        }
    }

    void ApplyWalkingBobbing()
    {
        // Get player speed
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if the player is moving
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            // Player is moving
            timer += Time.deltaTime * bobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            // Player is not moving
            timer = 0f;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * bobbingSpeed), transform.localPosition.z);
        }
    }

    void PlayFootstepSounds()
    {
        if (footstepSounds.Length == 0) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f) // Check if the player is moving
        {
            stepTimer += Time.deltaTime;
            if (stepTimer > stepInterval)
            {
                stepTimer = 0f;

                // Check if another footstep sound is not already playing
                if (!audioSource.isPlaying)
                {
                    // Play a random footstep sound
                    AudioClip footstep = footstepSounds[Random.Range(0, footstepSounds.Length)];
                    audioSource.PlayOneShot(footstep);
                }
            }
        }
        else
        {
            // Player is not moving, stop footstep sounds
            stepTimer = 0f;
            audioSource.Stop();
        }
    }

    public void FreezeCamera()
    {
        isFrozen = true;
    }

    public void UnfreezeCamera()
    {
        isFrozen = false;
    }
}
