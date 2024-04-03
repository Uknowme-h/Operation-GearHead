using UnityEngine;

public class CarRadio : MonoBehaviour
{
    // Public variables
    public AudioSource radioSource;
    public KeyCode switchKey = KeyCode.R; // Key to toggle radio
    public Camera carCamera;

    // Private variables
    private bool isRadioOn = false;

    void Start()
    {
        radioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }

    void Update()
    {
        // Check if car camera is active and visible
        bool isInCar = carCamera.gameObject.activeInHierarchy;

        // Toggle radio on car entry (no intro clip)
        if (isInCar && !isRadioOn)
        {
            isRadioOn = false;
             
        }


        if (isInCar && Input.GetKeyDown(switchKey)) 
        {
            Debug.Log("Radio active");

            if (isRadioOn)
            {
                radioSource.Stop(); 
                isRadioOn= false;   
            }
            else
            {
                radioSource.Play(); 
                isRadioOn=true;
            }
            
        }
    }
}
