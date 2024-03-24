using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PovManager : MonoBehaviour
{
    // Replace these with the actual names of your cameras in the scene
    public GameObject playerCamera;
    public GameObject CarCamera;
    public GameObject Carsounds;
    public GameObject unitychan;
    public GameObject charcter;
    // Replace this with the actual name of your car controller script
    public PrometeoCarController carController; // Assuming your car control script derives from CarController

    public KeyCode switchKey = KeyCode.C;

    // **Character set as default state**
    private GameState currentState = GameState.Character;

    // Reference to your third person controller script component
    public StarterAssets.ThirdPersonController characterController; // Assuming your script is named ThirdPersonController

    public float switchDistanceThreshold = 3f;

    public enum GameState { Character, Car }

    void Start()
    {
        // Find the game object with the ThirdPersonController component
        GameObject characterControllerObject = GameObject.Find("PlayerArmature");

        // Get the ThirdPersonController component from the found game object
        characterController = characterControllerObject.GetComponent<StarterAssets.ThirdPersonController>();

        // Ensure characterController is not null before accessing it
        if (characterController != null)
        {
            playerCamera.SetActive(true);
            CarCamera.SetActive(false);
            Carsounds.SetActive(false);

            characterController.enabled = true;
            carController.enabled = false;

            playerCamera.GetComponent<AudioListener>().enabled = true;
            CarCamera.GetComponent<AudioListener>().enabled = false;
        }
        else
        {
            Debug.LogError("Failed to find or assign character controller component.");
        }
    }


    void Update()
    {
        Vector3 carPosition = carController.transform.position;
        if (Input.GetKeyDown(switchKey))
        {
            if (CanSwitch())
            {
                currentState = (currentState == GameState.Character) ? GameState.Car : GameState.Character;
                charcter.transform.position = carPosition + new Vector3(2f, 0f, 0f);
                UpdateCameraAndControls();
                // Debug.Log(carPosition);
                // Stop any currently running ShowTempMessage coroutine
                //StopCoroutine(ShowTempMessage(5f, "Switching to " + currentState.ToString()));
                //StartCoroutine(ShowTempMessage(5f, "Switching to " + currentState.ToString())); // Start new coroutine only when switching
            }
            else
            {
                Debug.Log("Cannot switch. Character and car are too far apart.");
            }
        }
    }


    bool CanSwitch()
    {
        if (currentState == GameState.Character)
        {
            // Calculate the distance between the character and the car
            float distance = Vector3.Distance(characterController.transform.position, carController.transform.position);

            // Check if the distance is within the threshold
            return distance <= switchDistanceThreshold;
        }
        else
        {
            // Return true if not in character POV

            return true;
        }
    }

    void UpdateCameraAndControls()
    {
        switch (currentState)
        {
            case GameState.Character:

                playerCamera.SetActive(true);

                CarCamera.SetActive(false);

                // Enable character controller and disable car controller
                characterController.enabled = true;
                carController.enabled = false;


                Carsounds.SetActive(false);
                // Disable audio listener on car camera
                playerCamera.GetComponent<AudioListener>().enabled = true;
                CarCamera.GetComponent<AudioListener>().enabled = false;

                // Make the character GameObject visible
                unitychan.SetActive(true);

                // Reset the position and rotation of the character to match the car's


                break;
            case GameState.Car:
                playerCamera.SetActive(false);
                CarCamera.SetActive(true);

                // Disable character controller and enable car controller
                characterController.enabled = false;
                carController.enabled = true;


                Carsounds.SetActive(true);
                // Enable audio listener on player camera
                playerCamera.GetComponent<AudioListener>().enabled = false;
                CarCamera.GetComponent<AudioListener>().enabled = true;

                // Make the character GameObject invisible
                unitychan.SetActive(false);
                break;
        }
    }

    IEnumerator ShowTempMessage(float duration, string message)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            Debug.Log(message); // Log the provided message
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}

