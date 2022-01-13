using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLightToggle : MonoBehaviour
{
    public static int notesNum = 0;
    // battery number reference code from https://www.youtube.com/watch?v=Dq1T4qezGh8
    private static int _maxBatteryCount = 4;
    public static float _currentBatteries = 4;
    // setting related to flashlight
    [SerializeField]
    private float _maxIntensity = 4f;
    private bool flashlightMode;
    private static bool FlashlightActive = false;
    // for task and battery bar
    public Slider slider;
    public Text numText;
    // for the flashlight game object
    [SerializeField] GameObject FlashlightLight;
    [SerializeField] GameObject Lights; 
    [SerializeField] GameObject SpotLight;
    [SerializeField] GameObject ambient;

    // Update is called once per frame
    void Update()
    {
        batteryAndTask();
        
        if (FlashlightLight.activeSelf)
        {
            flashlightIntensity();
            
            controlFlashlight();

            battery();
        }
        
    }
    
    // showing the task and the current battery
    private void batteryAndTask()
    {
        numText.text = "Hopeless: " + Hopeless.countHopeless + " Notes: " +notesNum+  " Decoy: " + DecoyThrower.decoyNum;
        slider.value = _currentBatteries;
    }
    // controlling the intensity of the light
    private void flashlightIntensity()
    {
        float intensityPercent = (float)_currentBatteries / (float)_maxBatteryCount;
        float intensityFactor = _maxIntensity * intensityPercent;
        SpotLight.GetComponent<Light>().intensity = intensityFactor;
    }
    // changing the flashlight depending on the player's control
    private void controlFlashlight()
    {
        // change the mode of flashlight between white and red light
        if (Input.GetMouseButtonDown(0) && _currentBatteries > 0 && FlashlightActive)
        {
            flashlightMode = false;
            SpotLight.gameObject.GetComponent<Light>().color = Color.white;
            ambient.GetComponent<MeshRenderer>().material.SetColor("_TintColor", new Color(255,255,255,0.02f));
            SoundManagerScript.playSound("flashlightMode");
        }
        if (Input.GetMouseButtonDown(1) && _currentBatteries > 0 && FlashlightActive)
        {
            flashlightMode = true;
            SpotLight.gameObject.GetComponent<Light>().color = Color.red;
            ambient.GetComponent<MeshRenderer>().material.SetColor("_TintColor", new Color(255,0,0,0.02f));
            SoundManagerScript.playSound("flashlightMode");
        }

        // turn on/off the flashlight
        if (Input.GetKeyDown(KeyCode.F) && _currentBatteries > 0)
        {
            if (FlashlightActive == false)
            {
                Lights.SetActive (true);
                FlashlightActive = true;
            }
            else
            {
                Lights.SetActive(false);
                FlashlightActive = false;
            }
            SoundManagerScript.playSound("flashlight");
        }
    }
    // setting related to battery
    private void battery()
    {
        // the battery deplete speed will depend on the mode of the flashlight
        if (FlashlightActive && _currentBatteries > 0)
        {
            if (SpotLight.GetComponent<Light>().color == Color.white)
            {
                _currentBatteries -= (Time.deltaTime / 10);
            }
            else if (SpotLight.GetComponent<Light>().color == Color.red)
            {
                _currentBatteries -= (Time.deltaTime / 5);
            }
        }
        // turn off the flashlight when the battery is depleted completely
        if (_currentBatteries <= 0)
        {
            FlashlightActive = false;
            Lights.SetActive(false);
        }
    }

    // this is used to add battery
    public static void AddBattery()
    {
        if(_currentBatteries >= 4)
        {
            return;
        }
        _currentBatteries++;
    }
    // this is used by the hello guy to steal the player's battery
    public static void ReduceBattery()
    {
        if(_currentBatteries <= 0)
        {
            return;
        }
        _currentBatteries--;
    }
    // this is used to check whether the player's flashlight is on or off
    public static bool flashLightStatus()
    {
        return FlashlightActive;
    }
}
