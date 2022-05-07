using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem DeadParticle;
    private FlashScreen _flashScreen;
    public bool isDestroyed = false;

    private void Awake()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(7f);
        if (gameObject!=null && !isDestroyed)
        {
            SoundManager.PlaySound(SoundManager.Sound.evilLaugh, transform.position);
            if (Flashlight.FlashlightActive && WeaponSwitching.selectedWeapon == 0)
            {
                Flashlight.setBatteries(1f);
                Flashlight.flicker = true;
                HealthBarFade.Damage();
                SoundManager.PlaySound(SoundManager.Sound.flashlightFlicker, transform.position);
            }
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {
        if (Spherecast.t > 80)
        {
            isDestroyed = true;
            _flashScreen = GameObject.Find("FlashBang").GetComponent<FlashScreen>();
            _flashScreen.FlashBanged();
            Instantiate(DeadParticle, transform.position, Quaternion.identity);
            Spherecast.t = 1;
        }
    }
}
