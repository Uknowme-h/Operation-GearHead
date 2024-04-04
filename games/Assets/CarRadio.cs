using TMPro;
using UnityEngine;

public class CarRadio : MonoBehaviour
{
    // Public variables
    public AudioSource radioSource;
    public KeyCode switchKey = KeyCode.R;
    public Camera carCamera;
    public TextMeshProUGUI radioText;

    // Private variables
    private bool isRadioOn = false;

    void Start()
    {
        radioSource = GetComponent<AudioSource>();
        radioText.text = "";
    }

    void Update()
    {

        bool isInCar = carCamera.gameObject.activeInHierarchy;


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
                isRadioOn = false;
                radioText.text = "Radio off";
            }
            else
            {
                radioSource.loop = true;
                radioSource.Play();
                isRadioOn = true;
                radioText.text = "Now playing: " + radioSource.clip.name;
            }

        }

        if (!isInCar && isRadioOn)
        {
            radioSource.Stop();
            isRadioOn = false;
            radioText.text = "";
        }
    }
}
