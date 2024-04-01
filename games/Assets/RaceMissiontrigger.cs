using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RaceMissiontrigger : MonoBehaviour
{
    public GameObject MissionStart;
    public GameObject RaceMissionUI;
    public GameObject Lap1;
    public GameObject Lap2;
    public GameObject Lap3;
    public TextMeshPro TimerUI;
    public TextMeshProUGUI ScoreUI;


    private void Start()
    {
        RaceMissionUI.SetActive(false);
    }

    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == MissionStart)
        {
            
            RaceMissionUI.SetActive(true);
            StartCoroutine(DeactivateMission3UI());

            IEnumerator DeactivateMission3UI()
            {
                yield return new WaitForSeconds(3f);
                RaceMissionUI.SetActive(false);
                
            }



           
        }
    }
}
