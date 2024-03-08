using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PovManager : MonoBehaviour
{
    // Replace these with the actual names of your cameras in the scene
    public GameObject playerCamera;
    public GameObject CarCamera;
    
    public GameObject Carsounds;

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
        if (Input.GetKeyDown(switchKey))
        {
            if (CanSwitch()) // Check if the player can switch based on distance
            {
                currentState = (currentState == GameState.Character) ? GameState.Car : GameState.Character;
                UpdateCameraAndControls();
            }
            else
            {
                Debug.Log("Cannot switch. Character and car are too far apart.");
            }
        }
    }

    bool CanSwitch()
    {
        // Calculate the distance between the character and the car
        float distance = Vector3.Distance(characterController.transform.position, carController.transform.position);

        // Check if the distance is within the threshold
        return distance <= switchDistanceThreshold;
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

            Carsounds.SetActive(false);
            // Disable audio listener on car camera
            playerCamera.GetComponent<AudioListener>().enabled = true;
            CarCamera.GetComponent<AudioListener>().enabled = false;

            // Make the character GameObject visible
            characterController.gameObject.SetActive(true);

            // Reset the position and rotation of the character to match the car's
            characterController.transform.position = carController.transform.position;
            characterController.transform.rotation = carController.transform.rotation;

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
            characterController.gameObject.SetActive(false);
            break;
    }
}




    public override bool Equals(object obj)
    {
        return obj is PovManager manager &&
               base.Equals(obj) &&
               name == manager.name &&
               hideFlags == manager.hideFlags &&
               EqualityComparer<Transform>.Default.Equals(transform, manager.transform) &&
               EqualityComparer<GameObject>.Default.Equals(gameObject, manager.gameObject) &&
               tag == manager.tag &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Rigidbody>(), manager.GetComponent<Rigidbody>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Rigidbody2D>(), manager.GetComponent<Rigidbody2D>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Camera>(), manager.GetComponent<Camera>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Light>(), manager.GetComponent<Light>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Animation>(), manager.GetComponent<Animation>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<ConstantForce>(), manager.GetComponent<ConstantForce>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Renderer>(), manager.GetComponent<Renderer>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<AudioSource>(), manager.GetComponent<AudioSource>()) &&
               // Remove reference to NetworkView
               // EqualityComparer<Component>.Default.Equals(GetComponent<NetworkView>(), manager.GetComponent<NetworkView>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Collider>(), manager.GetComponent<Collider>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<Collider2D>(), manager.GetComponent<Collider2D>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<HingeJoint>(), manager.GetComponent<HingeJoint>()) &&
               EqualityComparer<Component>.Default.Equals(GetComponent<ParticleSystem>(), manager.GetComponent<ParticleSystem>()) &&
               enabled == manager.enabled &&
               isActiveAndEnabled == manager.isActiveAndEnabled &&
               EqualityComparer<CancellationToken>.Default.Equals(destroyCancellationToken, manager.destroyCancellationToken) &&
               useGUILayout == manager.useGUILayout &&
               runInEditMode == manager.runInEditMode &&
               EqualityComparer<GameObject>.Default.Equals(playerCamera, manager.playerCamera) &&
               EqualityComparer<GameObject>.Default.Equals(CarCamera, manager.CarCamera) &&
               EqualityComparer<PrometeoCarController>.Default.Equals(carController, manager.carController) &&
               switchKey == manager.switchKey &&
               currentState == manager.currentState &&
               EqualityComparer<ThirdPersonController>.Default.Equals(characterController, manager.characterController);
    }
            
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
