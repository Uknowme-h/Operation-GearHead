using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PovManager : MonoBehaviour
{
    // Replace these with the actual names of your cameras in the scene
    public GameObject playerCamera;
    public GameObject CarCamera;

    // Replace this with the actual name of your car controller script
    public PrometeoCarController carController; // Assuming your car control script derives from CarController

    public KeyCode switchKey = KeyCode.C;

    // **Character set as default state**
    private GameState currentState = GameState.Character;

    // Reference to your third person controller script component
    public StarterAssets.ThirdPersonController characterController; // Assuming your script is named ThirdPersonController

    public enum GameState { Character, Car }

    void Start()
    {
        characterController = GetComponent<StarterAssets.ThirdPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            currentState = (currentState == GameState.Character) ? GameState.Car : GameState.Character;
            UpdateCameraAndControls();
        }
    }

    void UpdateCameraAndControls()
    {
        Debug.Log("Switching to " + currentState.ToString());
        switch (currentState)
        {
            case GameState.Character:
                playerCamera.SetActive(true);
                CarCamera.SetActive(false);

                // Enable character controller and disable car controller
                characterController.enabled = true;
                carController.enabled = false;

                // Disable audio listener on car camera
                playerCamera.GetComponent<AudioListener>().enabled = true;
                CarCamera.GetComponent<AudioListener>().enabled = false;
                break;
            case GameState.Car:
                playerCamera.SetActive(false);
                CarCamera.SetActive(true);

                // Disable character controller and enable car controller
                characterController.enabled = false;
                carController.enabled = true;
                
                // Enable audio listener on player camera
                playerCamera.GetComponent<AudioListener>().enabled = false;
                CarCamera.GetComponent<AudioListener>().enabled = true;
                break;
        }
    }
}
