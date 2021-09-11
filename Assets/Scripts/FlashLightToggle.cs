using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightToggle : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    private bool FlashlightActive = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FlashlightActive == false)
            {
                FlashlightLight.gameObject.SetActive(true);
                FlashlightActive = true;
            }
            else
            {
                FlashlightLight.gameObject.SetActive(false);
                FlashlightActive = false;
            }
            SoundManagerScript.playSound("flashlight");
        }
    }
}
