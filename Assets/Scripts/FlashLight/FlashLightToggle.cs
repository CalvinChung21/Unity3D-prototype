using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLightToggle : MonoBehaviour
{
    public static int notesNum = 0;
    [SerializeField] GameObject FlashlightLight;
    private static bool FlashlightActive = false;

    [SerializeField]
    private static int _maxBatteryCount = 4;
    [SerializeField]
    public static float _currentBatteries = 4;
    [SerializeField]
    private float _maxIntensity = 4f;

    private bool flashlightMode;

    public Slider slider;
    public Text numText;


    // Update is called once per frame
    void Update()
    {
        numText.text = "Hopeless: " + Hopeless.countHopeless + " Notes: " +notesNum+  " Decoy: " + DecoyThrower.decoyNum;

        if (FlashlightLight.activeSelf == true)
        {
            slider.value = _currentBatteries;
            // controlling the intensity of the light
            float intensityPercent = (float)_currentBatteries / (float)_maxBatteryCount;
            float intensityFactor = _maxIntensity * intensityPercent;
            FlashlightLight.GetComponent<Light>().intensity = intensityFactor;

            // change the mode of flashlight between white and red light
            if (Input.GetMouseButtonDown(0) && _currentBatteries > 0 && FlashlightActive)
            {
                flashlightMode = false;
                FlashlightLight.gameObject.GetComponent<Light>().color = Color.white;
                SoundManagerScript.playSound("flashlightMode");
            }
            if (Input.GetMouseButtonDown(1) && _currentBatteries > 0 && FlashlightActive)
            {
                flashlightMode = true;
                FlashlightLight.gameObject.GetComponent<Light>().color = Color.red;
                SoundManagerScript.playSound("flashlightMode");
            }
            // decrease the size of the flash light
            if (flashlightMode && FlashlightActive)
            {
                if (FlashlightLight.gameObject.GetComponent<Light>().spotAngle > 16)
                {
                    FlashlightLight.gameObject.GetComponent<Light>().spotAngle--;
                }
            }
            // increase the size of the flash light
            if (flashlightMode == false && FlashlightActive)
            {
                if (FlashlightLight.gameObject.GetComponent<Light>().spotAngle < 36)
                {
                    FlashlightLight.gameObject.GetComponent<Light>().spotAngle++;
                }
            }

            // turn on/off the flashlight
            if (Input.GetKeyDown(KeyCode.F) && _currentBatteries > 0)
            {
                if (FlashlightActive == false)
                {
                    FlashlightLight.GetComponent<Light>().enabled = true;
                    FlashlightActive = true;
                }
                else
                {
                    FlashlightLight.GetComponent<Light>().enabled = false;
                    FlashlightActive = false;
                }
                SoundManagerScript.playSound("flashlight");
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                AddBattery();
            }
            // the battery deplete spped will depend on the mode of the flashlight
            if (FlashlightActive && _currentBatteries > 0)
            {
                if (FlashlightLight.GetComponent<Light>().color == Color.white)
                {
                    _currentBatteries -= (Time.deltaTime / 10);
                }
                else if (FlashlightLight.GetComponent<Light>().color == Color.red)
                {
                    _currentBatteries -= (Time.deltaTime / 5);
                }
            }
            // turn off the flashlight when the battery is depleted completely
            if (_currentBatteries <= 0)
            {
                FlashlightActive = false;
                FlashlightLight.GetComponent<Light>().enabled = false;
            }
        }
        
    }

    public static void AddBattery()
    {
        if(_currentBatteries >= _maxBatteryCount)
        {
            return;
        }
        _currentBatteries++;
    }

    public static void ReduceBattery()
    {
        if(_currentBatteries <= 0)
        {
            return;
        }
        _currentBatteries--;
    }

    public static bool flashLightStatus()
    {
        return FlashlightActive;
    }
}
