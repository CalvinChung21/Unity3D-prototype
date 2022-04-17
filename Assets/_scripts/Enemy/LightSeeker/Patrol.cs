using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    // code reference from https://www.youtube.com/watch?v=8eWbSN2T8TE&t=467s
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpot;

    public static bool patrolOn = true;

    // Start is called before the first frame update
    void Start()
    {
        moveSpots = GameObject.Find("MoveSpots").GetComponentsInChildren<Transform>();
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

            if(Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
            {
                if(waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}
