using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommandPattern
{
    public class FlashLightToggle : MonoBehaviour
    {
        // for the flashlight game object
        [SerializeField] GameObject Lights;
        // setting related to flashlight
        [SerializeField]
        private float _maxIntensity = 4f;
        // 0 means white light, 1 means red light
        private static bool flashlightMode;
        public static bool FlashlightMode
        {
            get => flashlightMode;
            set => flashlightMode = value;
        }

        // the state whether the flashlight is on/off
        private static bool flashlightActive = false;

        public static bool FlashlightActive
        {
            get => flashlightActive;
            set => flashlightActive = value;
        }

        // command patterns for specific action
        private Command buttonF,leftMouseClick,rightMouseClick;
        
        private void Awake()
        {
            buttonF = new OpenFlashLight();
            leftMouseClick = new WhiteFlashLight();
            // rightMouseClick = new RedFlashLight();
        }
        
        // When the player is being hit by the ghost's fist
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "fist")
            {
                HealthBarFade.Damage();
                // screenShake
                ScreenShake.Execute();
                // play a 3d sound
                SoundManager.PlaySound(SoundManager.Sound.ghostAttack, gameObject.transform.position);

                other.enabled = false;
            }
        }
        
        // Update is called once per frame
        void Update()
        {
            controlFlashlight();
            battery();
        }

        // changing the flashlight depending on the player's control
        private void controlFlashlight()
        {
            // change the mode of flashlight between white and red light
            
            // left mouse click
            if (Input.GetMouseButtonDown(0) && FlashlightActive)
            {
                leftMouseClick.Execute(Lights);
            }
            // right mouse click
            // if (Input.GetMouseButtonDown(1) && FlashlightActive)
            // {
            //     ScreenShake.Execute();
            //     rightMouseClick.Execute(Lights);
            // }

            // turn on/off the flashlight
            if (Input.GetKeyDown(KeyCode.F))
            {
                buttonF.Execute(Lights);
            }
        }
        
        // setting related to battery
        private void battery()
        {
            #region the battery deplete speed will depend on the mode of the flashlight
            if (FlashlightActive && BatteryBar.enoughBatteries())
            {
                if (FlashlightMode)
                {
                    BatteryBar.changeBatteries(-(Time.deltaTime / 6));
                }
                else if (!FlashlightMode)
                {
                    BatteryBar.changeBatteries(-(Time.deltaTime / 3));
                }
            }
            #endregion

            #region turn off the flashlight when the battery is depleted completely
                if (!BatteryBar.enoughBatteries())
                {
                    FlashlightActive = false;
                    Lights.SetActive(false);
                    Patrol.patrolOn = true;
                }
            #endregion
        }
    }
}

