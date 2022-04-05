using System.Collections;
using System.Collections.Generic;
using CommandPattern;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    // reference code from https://answers.unity.com/questions/1690028/destroy-objects-after-a-set-time.html
    public float duration = 15f;
    // self destroy when it exists for a specific duration
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
