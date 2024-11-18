using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlindingLight_Effect : MonoBehaviour
{
    public Volume volume; // Assign your Global Volume here in the Inspector
    public float duration = 3f; // Duration of the fade effect

    private ColorAdjustments colorAdjustments;
    private float startExposure = 10f; // Starting post-exposure value
    private float targetExposure = 0f; // Target post-exposure value

    void Start()
    {
        // Get the Color Adjustments override from the Volume
        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = startExposure; // Set initial post-exposure
            StartCoroutine(FadePostExposure());
        }
        else
        {
            Debug.LogWarning("Color Adjustments not found in Volume profile!");
        }
    }

    IEnumerator FadePostExposure()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Lerp post-exposure value from start to target over the specified duration
            colorAdjustments.postExposure.value = Mathf.Lerp(startExposure, targetExposure, elapsedTime / duration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the final post-exposure value is exactly the target value
        colorAdjustments.postExposure.value = targetExposure;
    }
}
