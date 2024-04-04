using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using KeyCode = UnityEngine.KeyCode;

public class MissionController : MonoBehaviour
{
    public bool Mission1 = false;
    public bool Mission2 = false;
    public bool Mission3 = false;
    public bool Mission4 = false;
    public GameObject mission1UI;
    public GameObject mission2UI;

    public GameObject car;
    private float maxHeight = 0f;
    private Vector3 startPosition;

    private Collider racingTrackTrigger;
    private KeyCode switchKey = KeyCode.C;



    private void Start()
    {
        racingTrackTrigger = GameObject.Find("raceTrackCollider").GetComponent<SphereCollider>();
        mission1UI.SetActive(false);
        mission2UI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Mission1)
        {
            Mission1 = true;
            mission1UI.SetActive(true);
        }
    }

    private void Update()
    {
        CheckCarActive();
        CheckLanding();

    }

    private void CheckCarActive()
    {
        if (Mission1 && Input.GetKeyDown(switchKey))
        {
            Mission1 = false;
            Mission2 = true;
            mission1UI.SetActive(false);
            mission2UI.SetActive(true);


        }
    }




    private void CheckLanding()
    {
        if (Mission4)
        {

            float raycastDistance = 50f;
            RaycastHit hit;

            float offset = 20f;


            bool isGrounded = Physics.Raycast(car.transform.position + car.transform.forward * offset, Vector3.forward, out hit, raycastDistance);



            GameObject ramp = GameObject.Find("Ramp");


            Collider Collider = GameObject.Find("CheckLandingCollider").GetComponent<BoxCollider>();
            if (isGrounded && Collider != null && Physics.Raycast(car.transform.position, Vector3.up, out hit, 20f) && hit.collider == Collider)
            {
                Mission4 = false;
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
