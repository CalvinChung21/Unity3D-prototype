using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public static bool safe = true;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.layer == 8)
        {
            safe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Leave");
        if (other.gameObject.layer == 8)
        {
            safe = false;
        }
    }
    
}
