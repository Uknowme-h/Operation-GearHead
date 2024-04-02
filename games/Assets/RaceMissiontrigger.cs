using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceMissiontrigger : MonoBehaviour
{
    public GameObject MissionStart;
    public TextMeshProUGUI RaceMissionUI;
    public GameObject RaceUI;
    public GameObject Lap1;
    public GameObject Lap2;
    public GameObject Lap3;
    public TextMeshProUGUI TimerUI;
    public TextMeshProUGUI Mission2;

    private float countdownTime = 3 * 60f; 
    private bool isCountdownActive = false;
    public int isLap1 = 1;
    private void Start()
    {
        RaceMissionUI.text="";
        TimerUI.text = "";
        RaceUI.SetActive(false);
    }

    private void Update()
    {
        if (isCountdownActive)
        {
            countdownTime -= Time.deltaTime;
            TimerUI.text = "Timer:-" + countdownTime.ToString("0:00");

            if (countdownTime <= 0)
            {
                RaceMissionUI.text = "Too late! Mission aborted.";
                // Perform any additional actions for mission failure here
                isCountdownActive = false;
                TimerUI.text = ""; 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == MissionStart)
        {
            if (isLap1 == 1)
            {
                RaceMissionUI.text = "Mission Started! Goodluck : )";
                StartCoroutine(DeactivateMission3UI());
                isLap1 = 2;
                Mission2.text = "";
                RaceUI.SetActive(true);
            }
                if (!isCountdownActive)
                {
                    StartCoroutine(Countdown());
                    isCountdownActive = true;
                }
                else if (countdownTime > 0)
                {
                    Debug.Log("You win!");
                  
                     isCountdownActive = false;
                     TimerUI.text = "";
                     RaceMissionUI.text = "YAYY ! You win !";
                    RaceUI.SetActive(false);
                    StartCoroutine(DeactivateMission3UI());

            }
            
        }
    }

    IEnumerator DeactivateMission3UI()
    {
        yield return new WaitForSeconds(3f);
        RaceMissionUI.text = "";
    }

    IEnumerator Countdown()
    {
        while (countdownTime > 0)
        {
            yield return null;
        }
    }
}
