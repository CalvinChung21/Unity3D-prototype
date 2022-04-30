using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("FPSController").transform.position;
        StartCoroutine(MoveTowardsPlayer());
    }

    IEnumerator MoveTowardsPlayer()
    {
        transform.LookAt(playerPosition);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * 0.01f);
        yield return new WaitForSeconds(5);
    }
}
