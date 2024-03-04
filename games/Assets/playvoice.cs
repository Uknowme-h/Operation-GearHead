using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playvoice : MonoBehaviour
{
    public AudioClip attackVoiceLine; // Reference to your audio clip

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // Replace with your desired key
        {
            PlayVoiceLine(attackVoiceLine);
        }
    }

    void PlayVoiceLine(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
}
