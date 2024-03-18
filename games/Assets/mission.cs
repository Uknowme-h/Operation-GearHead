using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class mission : MonoBehaviour
{
    public Transform spawnPoint;
    private float recordedCarHeight;
    public TextMeshProUGUI heightText;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && other.gameObject.name == "Cube")
        {
            if (other.isTrigger)
            {
                float carHeight = other.transform.position.y - transform.position.y;
                recordedCarHeight = carHeight;

                other.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

                if (heightText != null)
                {
                    heightText.text = "Car Height: " + recordedCarHeight.ToString("0.00") + " meters";
                }

                CheckMissionCompletion();
            }
            else
            {
                Debug.LogWarning("MissionTrigger: 'gateCollider' should be a Trigger!");
            }
        }
    }

    void CheckMissionCompletion()
    {
        if (recordedCarHeight > 5f)
        {
            Debug.Log("Mission Completed! Car Flew Above 5 Meters!");
            // Additional actions on mission completion can be added here
        }
    }

    void Start()
    {
        MakeColliderTransparent(GetComponent<Collider>());
    }

    void MakeColliderTransparent(Collider collider)
    {
        if (collider != null && collider.GetComponent<MeshRenderer>() != null)
        {
            Material material = collider.GetComponent<MeshRenderer>().material;
            Color color = material.color;
            color.a = 0.5f; // Set transparency (alpha = 0.5)
            material.color = color;
        }
    }
}