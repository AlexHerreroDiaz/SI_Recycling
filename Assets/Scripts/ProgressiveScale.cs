using UnityEngine;

public class ProgressiveScale : MonoBehaviour
{
    public float scaleAmplitude = 0.2f; // Maximum change in scale (e.g., 0.2 means 20% increase/decrease)
    public float scaleSpeed = 1f; // Speed at which the model scales up and down
    public Vector3 baseScale = new Vector3(1f, 1f, 1f); // Base scale of the model

    private Vector3 initialScale;
    private float scaleOffset; // Offset to create a continuous scaling effect

    void Start()
    {
        // Store the initial scale of the model
        initialScale = transform.localScale;
        // Randomize the offset to create a unique starting point for each instance
        scaleOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    void Update()
    {
        // Calculate the scaling factor using a sine wave to create a smooth transition
        float scaleFactor = 1 + scaleAmplitude * Mathf.Sin(Time.time * scaleSpeed + scaleOffset);

        // Apply the new scale to the model
        transform.localScale = initialScale * scaleFactor;
    }
}
