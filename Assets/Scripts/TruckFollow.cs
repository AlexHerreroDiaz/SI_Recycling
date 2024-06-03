using UnityEngine;

public class TruckFollow : MonoBehaviour
{
    public Transform target; // Reference to the cube player
    public float followDistance = 2f; // Distance threshold to stop following

    public float followSpeed = 1f; // Speed at which the truck follows the cube
    public float rotationSpeed = 1f; // Speed at which the truck rotates to face the cube
    public bool rotateOnlyY = true; // Whether to rotate only around the y-axis

    public bool selectedByPlayer = false; // Whether the truck is selected by the player

    public float collisionTimer = 0f; // Timer for collision duration
    public float selectDelay = 0f; // Timer for collision duration
    public bool isParked = false; // Whether the truck is parked
    private Vector3 parkPosition; // The position to move to when parked
    private Quaternion parkedRotation; // The rotation when parked


    public Collider playerCollider; // Reference to the player's collider
    private PlayerMovement playerMovement; // Reference to the PlayerMovement component
    public GameObject player; // Reference to the player object

    private void Start()
    {
        // Store the initial position and rotation as the parking position and rotation
        parkPosition = transform.position;
        parkedRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (!isParked && target != null)
        {
            selectedByPlayer = true;
            // Calculate the direction to move towards the target
            Vector3 direction = new Vector3(target.position.x - transform.position.x, 1f, target.position.z - transform.position.z);

            // Check if the distance to the target is greater than the follow distance
            if (direction.magnitude > followDistance)
            {
                // Calculate the rotation to look at the target
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                if (rotateOnlyY)
                {
                    targetRotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);
                }

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

                // Smoothly move towards the target position (excluding y-axis)
                Vector3 targetPos = new Vector3(target.position.x, 1f, target.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);
            }
        }
        else if(isParked || !selectedByPlayer){
            selectedByPlayer = false;
            
            if(player != null){
                player.GetComponent<PlayerMovement>().selectingPlayer = false;
                player = null;
            }

            if(target != null){
                target = null;
            }
            

            selectDelay += Time.fixedDeltaTime;
            // Move to the park position when parked
            transform.position = Vector3.Lerp(transform.position, parkPosition, followSpeed * Time.fixedDeltaTime);
            // Rotate to the parked rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, parkedRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        if (selectDelay > 6f){
            isParked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !selectedByPlayer && other.gameObject.GetComponent<PlayerMovement>().selectingPlayer == false)
        {
            other.gameObject.GetComponent<PlayerMovement>().selectingPlayer = true;
            player = other.gameObject;
            playerCollider = other;
            // Set the target to the collided object
            target = other.transform;
            Debug.Log("Collision detected");

            // Set Player collider to be a trigger
            //collision.collider.isTrigger = true;
            selectedByPlayer = true;
        }
        else if (other.CompareTag("Park"))
        {
            // Start the collision timer
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Park") && isParked == false && selectedByPlayer == true)
        {
            // Increment the collision timer
            collisionTimer += Time.deltaTime;

            // Check if the collision timer exceeds 4 seconds
            if (collisionTimer >= 4f)
            {
                // Truck has been parked for 2 seconds, stop following the player
                //playerCollider.isTrigger = false;
                collisionTimer = 0f;
                isParked = true;
                selectedByPlayer = false;
                target = null;
                selectDelay = 0f;
                player.GetComponent<PlayerMovement>().selectingPlayer = false;
                player = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reset the target when the collision ends
            // target = null;

            // Reset Player collider to be non-trigger
            // collision.collider.isTrigger = false;
        }
        else if (other.CompareTag("Park"))
        {
            // Reset the collision timer when leaving the park area
        }
    }
}
