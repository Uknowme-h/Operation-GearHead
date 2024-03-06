using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class RacingTrack : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;
    float distanceTravelled;

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
}
