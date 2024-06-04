using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeOnCollision : MonoBehaviour
{
    public string targetSceneName; // The name of the scene to load
    private float collisionTime; // Time the player has been in collision
    private bool isColliding; // Flag to check if the player is currently colliding with the box
    public float requiredCollisionTime = 5.0f; // Time in seconds the player needs to collide to change scene
    public Image imageComponent; // Reference to the Image component to enable/disable

    void Start()
    {
        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>(); // Automatically get the Image component if not set in Inspector
        }

        if (imageComponent != null)
        {
            imageComponent.enabled = false; // Ensure the Image component is initially disabled
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
            if (imageComponent != null)
            {
                imageComponent.enabled = true; // Enable the Image component
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
            collisionTime = 0f; // Reset collision time when player exits
            if (imageComponent != null)
            {
                imageComponent.enabled = false; // Disable the Image component
            }
        }
    }

    void Update()
    {
        if (isColliding)
        {
            collisionTime += Time.deltaTime;
            if (collisionTime >= requiredCollisionTime)
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
        else
        {
            collisionTime = 0f; // Reset collision time if not colliding
        }
    }
}
