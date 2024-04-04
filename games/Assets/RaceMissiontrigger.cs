using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public TextMeshProUGUI ScoreUI;

    public TextMeshProUGUI Mission3;

    int Score = 0;



    private float countdownTime = 5 * 60f;
    private bool isCountdownActive = false;
    public int isLap1 = 0;
    private void Start()
    {
        RaceMissionUI.text = "";
        TimerUI.text = "";
        RaceUI.SetActive(false);
    }

    private void Update()
    {
        ScoreUI.text = "Score: " + Score;
        if (isCountdownActive)
        {
            countdownTime -= Time.deltaTime;
            TimerUI.text = "Timer:-" + countdownTime.ToString("0:00");

            if (countdownTime <= 0)
            {
                RaceMissionUI.text = "Too late! Mission aborted.";
                StartCoroutine(DeactivateMission3UI());
                // Perform any additional actions for mission failure here
                isCountdownActive = false;
                TimerUI.text = "";
                Mission3.text = "";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isLap1 += 1;

        if (other.gameObject == MissionStart)
        {
            if (isLap1 == 1)
            {
                RaceMissionUI.text = "Mission Started! Goodluck : )";
                StartCoroutine(DeactivateMission3UI());
                Mission2.text = "";
                RaceUI.SetActive(true);
            }
            else if (isLap1 == 2)
            {
                RaceMissionUI.text = "Lap - 2 ";
                StartCoroutine(DeactivateMission3UI());
                RaceUI.SetActive(true);
                Score += 10;

            }
            else if (isLap1 == 3)
            {
                RaceMissionUI.text = "Lap - 3 ";
                StartCoroutine(DeactivateMission3UI());
                RaceUI.SetActive(true);
                Score += 10;
            }

            if (!isCountdownActive)
            {
                StartCoroutine(Countdown());
                isCountdownActive = true;
            }
            else if (countdownTime > 0 && isLap1 == 4)
            {
                Debug.Log("You win!");
                Score += 10;
                String lastCountdownTime = countdownTime.ToString("0:00"); ;
                Debug.Log(lastCountdownTime);
                countdownTime = 5 * 60f;
                isCountdownActive = false;
                TimerUI.text = "Highscore:-" + lastCountdownTime + "s";
                RaceMissionUI.text = "YAYY ! You win !";
                RaceUI.SetActive(false);
                StartCoroutine(DeactivateMission3UI());
                isLap1 = 0;

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
