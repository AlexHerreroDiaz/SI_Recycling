using UnityEngine;
using UnityEngine.UI;

public class SlideshowController : MonoBehaviour
{
    public SlideShow slideShow;
    public float collisionTimeRequired = 2.0f; // Time required to collide to change the image
    public float CollisionTime = 0f;
    public bool isColliding = false;
    public bool isNextButton;

    public Image imageButton;

    void Start()
    {
        if (imageButton != null)
        {
            imageButton.enabled = false; // Disable the Image component
        }
    }

    void Update()
    {

        if (isColliding)
        {
            CollisionTime += Time.deltaTime;

            if (imageButton != null)
            {
                imageButton.enabled = true; // Enable the Image component
            }

            if (CollisionTime >= collisionTimeRequired)
            {
                if (isNextButton){
                    slideShow.ShowNextImage();
                    Debug.Log("Next Image");
                    CollisionTime = 0f; // Reset collision time
   
                }
                else
                {
                    slideShow.ShowPreviousImage();
                    Debug.Log("Previous Image");
                    CollisionTime = 0f; // Reset collision time

                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
            CollisionTime = 0f; // Reset collision time
            
            if (imageButton != null)
            {
                imageButton.enabled = false; // Disable the Image component
            }
        }
    }


}
