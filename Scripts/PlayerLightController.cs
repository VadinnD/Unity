using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour
{
    public Light playerLight;  // Light source reference
    private float lightDuration = 10.0f;  // Duration of light in seconds
    private float initialLightRadius; // Initial radius of the light
    private float initialLightIntensity; // Initial light intensity
    private float maxLightRadius = 15.0f; // Maximum radius of light
    private float lightExpandDuration = 3.0f; // Speed of expansion of light
    private float maxLightIntensity = 1.0f; // Maximum light intensity
    private bool lightActive = false; // Light activity flag
    private float lightTimer = 0.0f; // Timer for light duration
    void Start()
    {
        if (playerLight != null)
        {
            playerLight.enabled = false;  // Initially the light is off
            initialLightRadius = playerLight.range; // Maintaining the initial radius of the light
            initialLightIntensity = playerLight.intensity; // Maintaining the initial light intensity
        }
    }

    void Update()
    {
        // Checking if the light is active
        if (lightActive)
        {
            lightTimer -= Time.deltaTime;
            if (lightTimer <= 0)
            {
                // Turn off the lights when the timer expires
                playerLight.enabled = false;
                lightActive = false;
                playerLight.range = initialLightRadius; // Reset the radius of the light to the initial value
                playerLight.intensity = initialLightIntensity; // Reset the light intensity to the initial value
            }
        }
/*
        // Для примера включаем свет по нажатию клавиши "L"
        if (Input.GetKeyDown(KeyCode.L) && !lightActive)
        {
            ActivateLight();
        }
*/
    }

    public void ActivateLight()
    {
        if (playerLight != null)
        {
            playerLight.enabled = true;  // Turn on the light
            lightTimer = lightDuration;  // Setting a timer
            lightActive = true;  // Setting the light activity flag
            StartCoroutine(ExpandLight()); // Launching a coroutine to expand the light
        }
    }

    private IEnumerator ExpandLight()
    {
        float elapsedTime = 0f; // Time elapsed since expansion began
        float startRadius = initialLightRadius; // Initial radius of light
        float startIntensity = initialLightIntensity; // Initial light intensity

        while (elapsedTime < lightExpandDuration && lightActive)
        {
            elapsedTime += Time.deltaTime; // We increase the time
            // Linear radius interpolation
            playerLight.range = Mathf.Lerp(startRadius, maxLightRadius, elapsedTime / lightExpandDuration);
            // Linear intensity interpolation
            playerLight.intensity = Mathf.Lerp(startIntensity, maxLightIntensity, elapsedTime / lightExpandDuration);
            yield return null; // Waiting until the next frame
        }
        
        // Make sure that the radius and intensity do not exceed the maximum values
        playerLight.range = maxLightRadius;
        playerLight.intensity = maxLightIntensity;
    }
}
