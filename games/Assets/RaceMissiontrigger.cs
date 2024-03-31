using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceMissiontrigger : MonoBehaviour
{
    public GameObject MissionStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == MissionStart)
        {
            Debug.Log("Hello! The MissionStart GameObject entered the trigger.");
        }
    }
}
