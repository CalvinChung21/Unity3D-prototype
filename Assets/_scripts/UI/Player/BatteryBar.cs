using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommandPattern
{
    public class BatteryBar : MonoBehaviour
    {
        // code reference to video from https://www.youtube.com/watch?v=ZzkIn41DFFo
        public Image batteryBar;
        public Image[] batteryPoints;
        
        float battery, maxBattery = 100;
        
        // battery number reference code from https://www.youtube.com/watch?v=Dq1T4qezGh8
        private static int _maxBatteryCount = 5;
        private static float _currentBatteries = 5;
        // to check whether there is enough battery
        public static bool enoughBatteries()
        {
            return _currentBatteries > 0;
        }
        // to change the amount of batteries
        public static void changeBatteries(float val)
        {
            if ((_currentBatteries+val) <= _maxBatteryCount)
            {
                _currentBatteries += val;
            }
        }

        public static void setBatteries(float val)
        {
            _currentBatteries = val;
        }
        
        public static float getCurrentBatteries()
        {
            return _currentBatteries;
        }

        public static float getMaxBatteryCount()
        {
            return _maxBatteryCount;
        }
        
        // float lerpSpeed;
        private void Start()
        {
            battery = maxBattery;
        }
    
        private void Update()
        {
            battery = _currentBatteries*20;
            // lerpSpeed = 3f * Time.deltaTime;
            HealthBarFiller();
            // ColorChange();
        }
    
        void HealthBarFiller()
        {
            // batteryBar.fillAmount = Mathf.Lerp(batteryBar.fillAmount, battery/maxBattery, lerpSpeed);
            
            for (int i = 0; i < batteryPoints.Length; i++)
            {
                batteryPoints[i].enabled = !DisplayHealthPoint(battery, i);
            }
        }
    
        // void ColorChange()
        // {
        //     Color healthColor = Color.Lerp(Color.red, Color.green, (battery / maxBattery));
        //     batteryBar.color = healthColor;
        // }
    
        // public void Damage(float damagePoints)
        // {
        //     if (battery > 0) battery -= damagePoints;
        // }
        //
        // public void Heal(float healingPoints)
        // {
        //     if (battery < maxBattery)
        //     {
        //         battery += healingPoints;
        //     }
        // }
    
        bool DisplayHealthPoint(float _health, int pointNumber)
        {
            return ((pointNumber * 10) >= _health);
        }
        
    }

}
