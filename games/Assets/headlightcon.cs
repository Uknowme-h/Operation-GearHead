using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headlightcon : MonoBehaviour
{
    public Light[] headlights;
    public KeyCode headlightKey = KeyCode.L;
    bool isHeadlightsOn = false;

    void Update()
    {
        if (Input.GetKeyDown(headlightKey))
        {
            ToggleHeadlights();
        }
    }

    void ToggleHeadlights()
    {
        isHeadlightsOn = !isHeadlightsOn;

        foreach (Light light in headlights)
        {
            light.enabled = isHeadlightsOn;
        }
    }
}
