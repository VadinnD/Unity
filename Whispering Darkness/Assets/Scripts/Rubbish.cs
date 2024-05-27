using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class CameraController : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 300f; // Mouse sensitivity for camera rotation
    public Transform playerBody; // Reference to the player's body
    public bool lockCursor = true; // Whether to lock the cursor to the center of the screen

    [Header("Camera Limits")]
    public float minYAngle = -180f; // Minimum vertical angle for camera rotation
    public float maxYAngle = 180f; // Maximum vertical angle for camera rotation

    [Header("Walking Bobbing Settings")]
    public float bobbingSpeed = 6f; // Speed of the walking bobbing effect
    public float bobbingAmount = 0.1f; // Amount of the walking bobbing effect

    [Header("Footstep Sounds")]
    public AudioClip[] footstepSounds; // Array of footstep sounds
    public float stepInterval = 0.2f; // Interval between footstep sounds
    
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

        // Apply initial rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the camera
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player body left and right
        playerBody.Rotate(Vector3.up * mouseX);

        // Adjust camera up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minYAngle, maxYAngle);
        
        // Apply rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply walking bobbing effect
        ApplyWalkingBobbing();
        PlayFootstepSounds();
    }

    void ApplyWalkingBobbing()
    {
        // Get player speed
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Check if the player is moving
        if (Mathf.Abs(horizontal) > 0.9f || Mathf.Abs(vertical) > 0.9f)
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
}
*/








/*
26.05.24


public class CameraController : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 1000f; // Mouse sensitivity for camera rotation
    public Transform playerBody; // Reference to the player's body
    public bool lockCursor = true; // Whether to lock the cursor to the center of the screen
    private bool isFrozen = false;

    [Header("Camera Limits")]
    public float minYAngle = -90f; // Minimum vertical angle for camera rotation
    public float maxYAngle = 90f; // Maximum vertical angle for camera rotation

    [Header("Walking Bobbing Settings")]
    public float bobbingSpeed = 6f; // Speed of the walking bobbing effect
    public float bobbingAmount = 0.1f; // Amount of the walking bobbing effect

    [Header("Footstep Sounds")]
    public AudioClip[] footstepSounds; // Array of footstep sounds
    public float stepInterval = 0.2f; // Interval between footstep sounds
    
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

        // Apply initial rotation to the camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the camera
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player body left and right
        playerBody.Rotate(Vector3.up * mouseX);

        // Adjust camera up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minYAngle, maxYAngle);
        
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
        if (Mathf.Abs(horizontal) > 0.9f || Mathf.Abs(vertical) > 0.9f)
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




public class PlayerLightController : MonoBehaviour
{
    public Light playerLight;  // Light source reference
    public float lightDuration = 3.0f;  // Duration of light in seconds
    private bool lightActive = false;
    private float lightTimer = 0.0f;
    void Start()
    {
        if (playerLight != null)
        {
            playerLight.enabled = false;  // Изначально свет выключен
        }
    }

    void Update()
    {
        // Проверяем, если свет активен
        if (lightActive)
        {
            lightTimer -= Time.deltaTime;
            if (lightTimer <= 0)
            {
                // Выключаем свет, когда таймер истечет
                playerLight.enabled = false;
                lightActive = false;
            }
        }

        // Для примера включаем свет по нажатию клавиши "L"
        if (Input.GetKeyDown(KeyCode.L) && !lightActive)
        {
            ActivateLight();
        }
    }

    public void ActivateLight()
    {
        if (playerLight != null)
        {
            playerLight.enabled = true;  // Включаем свет
            lightTimer = lightDuration;  // Устанавливаем таймер
            lightActive = true;  // Устанавливаем флаг активности света
        }
    }
}


*/




