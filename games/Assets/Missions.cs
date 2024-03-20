using System.Linq;
using Unity.VisualScripting;

// Remove Unity.VisualScripting (if not needed)
using UnityEngine;
using KeyCode = UnityEngine.KeyCode; // Explicitly define KeyCode type 

public class MissionController : MonoBehaviour
{
    public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool Mission4 = false;

    public GameObject car; // Reference to the car GameObject (or null if car is a Prefab)
    private float maxHeight = 0f; // Stores the maximum height of the car
    private Vector3 startPosition;

    private Collider racingTrackTrigger; // Reference to the trigger collider in the racing track
    private KeyCode switchKey = KeyCode.C; // Key to switch to car (assuming 'C')

    private void Start()
    {
        racingTrackTrigger = GameObject.Find("raceTrackCollider").GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Mission1)
        {
            Mission1 = true;
            Debug.Log("Get in the car!");
        }
    }

    private void Update()
    {
        CheckCarActive();
        CheckRaceTrackTrigger();
        CheckLanding();
    }

    private void CheckCarActive()
    {
        if (Mission1 && Input.GetKeyDown(switchKey)) // Check for "Get in the car" message and C key press
        {
            Mission1 = false; // Reset Mission 1 as car is assumed active
            Mission2 = true;
            Debug.Log("Go to the racing track!");
        }
    }

    private void CheckRaceTrackTrigger()
    {
        if (Mission2)
        {
            RaycastHit hit;
            if (Physics.Raycast(car.transform.position, Vector3.forward, out hit, Mathf.Infinity)) // Raycast forward from car
            {
                if (hit.collider == racingTrackTrigger)
                {
                    Mission2 = false;
                    Mission3 = true;
                    Debug.Log("Use the ramp to fly the car over to the other end. Try to attain maximum distance as possible.");
                    maxHeight = 0f; // Reset maximum height for new attempt

                    // Store initial position
                    startPosition = car.transform.position;
                }
            }
        }
    }

    private void CheckLanding()
    {
        if (Mission4)
        {
            // Raycast a short distance down for initial ground check (optional)
            float raycastDistance = 50f; // Adjust this value based on your car's size and expected landing clearance
            RaycastHit hit;
            // Raycast forward from a point in front of the car
            float offset = 20f; // Adjust this value based on your car model

            // Raycast forward from a point in front of the car
            bool isGrounded = Physics.Raycast(car.transform.position + car.transform.forward * offset, Vector3.forward, out hit, raycastDistance);


            // Find the ramp GameObject (adjust the name if needed)
            GameObject ramp = GameObject.Find("Ramp");

            // Check if car is colliding with the top surface of the "Quad" (assuming it's a platform)
            Collider Collider = GameObject.Find("CheckLandingCollider").GetComponent<BoxCollider>(); // Get any collider on the Quad

            if (isGrounded && Collider != null && Physics.Raycast(car.transform.position, Vector3.up, out hit, 20f) && hit.collider == Collider)
            {
                Mission4 = false; // Reset Mission 4 after landing
                float maxDistance = Vector3.Distance(startPosition, car.transform.position);
                Debug.Log("Maximum distance covered: " + maxDistance + " units!");
            }
            else if (isGrounded)
            {
                Debug.Log("Out of track! Land on the designated area.");
            }
        }
    }

}
