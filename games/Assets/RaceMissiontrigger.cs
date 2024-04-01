using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceMissiontrigger : MonoBehaviour
{
    public GameObject MissionStart;
    public GameObject RaceMissionUI;
    public GameObject Lap1;
    public GameObject Lap2;
    public GameObject Lap3;
    public TextMeshProUGUI TimerUI;

    private float countdownTime = 3 * 60f; // 3 minutes in seconds
    private bool isCountdownActive = false;
    public bool isLap1 = false;
    private void Start()
    {
        RaceMissionUI.SetActive(false);
        TimerUI.text = ""; // Set initial text to empty
    }

    private void Update()
    {
        if (isCountdownActive)
        {
            countdownTime -= Time.deltaTime;
            TimerUI.text = "Timer:-" + countdownTime.ToString("0:00"); // Update timer display

            if (countdownTime <= 0)
            {
                Debug.Log("Too late! Mission aborted.");
                // Perform any additional actions for mission failure here
                isCountdownActive = false;
                TimerUI.text = ""; // Clear timer display after countdown ends
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == MissionStart)
        {
            if (!isLap1)
            {
                RaceMissionUI.SetActive(true);
                StartCoroutine(DeactivateMission3UI());
                isLap1 = true;
                if (!isCountdownActive)
                {
                    StartCoroutine(Countdown());
                    isCountdownActive = true;
                }
                else if (countdownTime > 0)
                {
                    Debug.Log("You win!");
                    // Perform any additional actions for mission success here
                    isCountdownActive = false;
                    TimerUI.text = ""; // Clear timer display after success
                }
            }
        }
    }

    IEnumerator DeactivateMission3UI()
    {
        yield return new WaitForSeconds(3f);
        RaceMissionUI.SetActive(false);
    }

    IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            yield return null;
        }
    }
}
