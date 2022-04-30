using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem DeadParticle;
    private FlashScreen _flashScreen;
    private void OnDestroy()
    {
        if (Spherecast.t > 149)
        {
            _flashScreen = GameObject.Find("FlashBang").GetComponent<FlashScreen>();
            _flashScreen.FlashBanged();
            Instantiate(DeadParticle, transform.position, Quaternion.identity);
            Spherecast.t = 1;
        }
    }
}
