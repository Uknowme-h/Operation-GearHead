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
                    Mission2 = false; // Reset Mission 2 after triggering track
                    Mission3 = true;
                    Debug.Log("Use the ramp to fly the car over to the other end. Try to attain maximum height as possible.");
                    maxHeight = 0f; // Reset maximum height for new attempt
                }
            }
        }
    }

    private void CheckLanding()
    {
        if (Mission4 && Physics.Raycast(car.transform.position, Vector3.down, out RaycastHit hit)) // Check for ground collision
        {
            Mission4 = false; // Reset Mission 4 after landing
            maxHeight = Mathf.Max(maxHeight, hit.distance); // Update max height based on current and previous values
            Debug.Log("Maximum height reached: " + maxHeight + " units!");
        }
    }
}
