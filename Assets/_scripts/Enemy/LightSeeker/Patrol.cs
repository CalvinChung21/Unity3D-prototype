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

    [SerializeField] GameObject model;
    
    Color colour;

    private Color glowingColor;

    private float t = 1;
    // Start is called before the first frame update
    void Start()
    {
        colour = model.GetComponent<SkinnedMeshRenderer>().material.GetColor("_EmissionColor");
        glowingColor = colour;
        moveSpots = GameObject.Find("MoveSpots").GetComponentsInChildren<Transform>();
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (patrolOn)
        {
            t = 1;
            glowingColor = colour * 9.9f;
            model.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", colour);
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
        else
        {
            if (t < 50)
            {
                t += 1;
                glowingColor *= 1.1f;
                model.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", glowingColor);
            }
        }
    }
}
