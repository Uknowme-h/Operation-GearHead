using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PovManager : MonoBehaviour
{
    
    public GameObject playerCamera;
    public GameObject CarCamera;
    public GameObject Carsounds;
    public GameObject unitychan;
    public GameObject charcter;
    public GameObject carspeedUI;
    public GameObject minimap;
    
    public PrometeoCarController carController; 

    public KeyCode switchKey = KeyCode.C;

    
    private GameState currentState = GameState.Character;

    
    public StarterAssets.ThirdPersonController characterController; 

    public float switchDistanceThreshold = 3f;

    public enum GameState { Character, Car }


    void Start()
    {
        
        GameObject characterControllerObject = GameObject.Find("PlayerArmature");

        
        characterController = characterControllerObject.GetComponent<StarterAssets.ThirdPersonController>();

        minimap.SetActive(false);

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
            
            float distance = Vector3.Distance(characterController.transform.position, carController.transform.position);

            
            return distance <= switchDistanceThreshold;
        }
        else
        {
            

            return true;
        }
    }

    void UpdateCameraAndControls()
    {
        switch (currentState)
        {
            case GameState.Character:

                playerCamera.SetActive(true);
                minimap.SetActive(false);
                CarCamera.SetActive(false);

                
                characterController.enabled = true;
                carController.enabled = false;
                carspeedUI.SetActive(false);

                Carsounds.SetActive(false);
               
                playerCamera.GetComponent<AudioListener>().enabled = true;
                CarCamera.GetComponent<AudioListener>().enabled = false;

                
                unitychan.SetActive(true);
                break;
            case GameState.Car:
                minimap.SetActive(true);
                playerCamera.SetActive(false);
                CarCamera.SetActive(true);

                
                characterController.enabled = false;
                carController.enabled = true;
                carspeedUI.SetActive(true);
               


                Carsounds.SetActive(true);
                
                playerCamera.GetComponent<AudioListener>().enabled = false;
                CarCamera.GetComponent<AudioListener>().enabled = true;

                
                unitychan.SetActive(false);
                break;
        }
    }

    IEnumerator ShowTempMessage(float duration, string message)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            Debug.Log(message); 
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}

