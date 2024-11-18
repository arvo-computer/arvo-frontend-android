using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SceneLoader_A : MonoBehaviour
{
    public string sceneToLoad;                  // Name of the scene to load
    public float transitionDuration = 3f;       // Duration to increase post-exposure
    public int controllerLayer = 8;             // Layer for VR controllers (set in Inspector)

    private Volume volume;                      // Reference to the Volume component
    private ColorAdjustments colorAdjustments;  // Color Adjustments effect
    private bool isTransitioning = false;       // Flag to prevent multiple transitions

    private void Start()
    {
        // Find the Volume in the scene and set up Color Adjustments
        volume = FindObjectOfType<Volume>();

        if (volume != null && volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = 0; // Initial exposure
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the overlapping object's layer matches the specified layer
        if (other.tag == "Hand" && !isTransitioning)
        {
            StartCoroutine(TransitionAndLoadScene());
            Debug.Log("Over Lapped");
        }
    }

    private IEnumerator TransitionAndLoadScene()
    {
        isTransitioning = true; // Ensure the transition only happens once
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float exposureValue = Mathf.Lerp(0, 10, elapsedTime / transitionDuration);
            colorAdjustments.postExposure.value = exposureValue;
            yield return null; // Wait for next frame
        }

        // Load the specified scene once the transition is complete
        SceneManager.LoadScene(sceneToLoad);
    }
}
