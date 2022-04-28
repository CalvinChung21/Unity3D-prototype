using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

public class FlashScreen : MonoBehaviour
{
    // code reference from https://www.youtube.com/watch?v=rD4c3j8jWJI
    [SerializeField] Volume volume;
    [SerializeField] CanvasGroup AlphaController;

    private static bool on = false;

    private void Update()
    {
        if (on)
        {
            AlphaController.alpha = AlphaController.alpha - Time.deltaTime;
            volume.GetComponent<Volume>().weight = volume.GetComponent<Volume>().weight - Time.deltaTime;

            if (AlphaController.alpha <= 0)
            {
                volume.GetComponent<Volume>().weight = 0;
                AlphaController.alpha = 0;
                on = false;
            }
        }
    }
    
    public void FlashBanged()
    {
        volume.GetComponent<Volume>().weight = 1;
        on = true;
        AlphaController.alpha = 1;
    }
}
