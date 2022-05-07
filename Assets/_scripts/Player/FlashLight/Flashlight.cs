using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Random = UnityEngine.Random;

    public class Flashlight : MonoBehaviour
    {
        [SerializeField] private GameObject Lights;
        // battery number reference code from https://www.youtube.com/watch?v=Dq1T4qezGh8
        private static int _maxBatteryCount = 5;
        private static float _currentBatteries = 0;

        public static bool flashlightActive = false;
        
        public static bool FlashlightActive
        {
            set => flashlightActive = value;
            get => flashlightActive;
        }
        
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

        private MeshRenderer rend;

        private float intensity;

        public static bool flicker = false;
        // float lerpSpeed;
        private void Start()
        {
            _currentBatteries = 2.5f;
            rend = gameObject.GetComponent<MeshRenderer>();
        }
    
        private void Update()
        {

            if (_currentBatteries > 0 && !flicker)
            {
                flashlightActive = true;
                Lights.SetActive(true);
                Patrol.patrolOn = false;
            }
            else if(_currentBatteries < 0 && !flicker)
            {
                flashlightActive = false;
                Lights.SetActive(false);
                if (GameObject.Find("FlareThrew(Clone)") == null)
                {
                    Patrol.patrolOn = true;
                }
            }

            // AdjustEmissionOfTheFlashlight();

            if (Input.GetMouseButton(0) && !flicker)
            {
                Shake();
            }

            ShakeFlashlight();

            ResetFlashlightTransform();

            if (flashlightActive)
            {
                changeBatteries(-(Time.deltaTime / 3));
            }
            
            #region turn off the flashlight when the battery is depleted completely
            if (!enoughBatteries())
            {
                flashlightActive = false;
                Lights.SetActive(false);
                Patrol.patrolOn = true;
                flicker = false;
            }
            #endregion

            if (flicker)
            {
                StartCoroutine(FlashLightFlicker());
            }
        }

        // void AdjustEmissionOfTheFlashlight()
        // {
        //     Color color = new Color(172, 191, 144);
        //     intensity = Mathf.Lerp(0, 0.001f, _currentBatteries);
        //     rend.material.EnableKeyword("_Emission");
        //     rend.material.SetColor("_EmissionColor", color * intensity);
        // }
        
        public void Shake()
        {
            if (!isShaking)
            {
                isShaking = true;
                temp_shake_intensity = shake_intensity;
            }
        }
        
        public float shake_decay = 0.01f;
        public float shake_intensity = 0.01f;
        private float temp_shake_intensity = 0;
        public bool isShaking = false;
        void ShakeFlashlight()
        {
            if (temp_shake_intensity > 0){
                Vector3 newPos = transform.localPosition + Random.insideUnitSphere * temp_shake_intensity;
                newPos.x = 0.75f;
                transform.localPosition = newPos;
                
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0,0, 75), Time.deltaTime*5.0f);
                temp_shake_intensity -= shake_decay;
                if (_currentBatteries < _maxBatteryCount)
                {
                    _currentBatteries += shake_decay*5;
                }
                else
                {
                    _currentBatteries = _maxBatteryCount;
                }
                
                SoundManager.PlaySound(SoundManager.Sound.shaking);
            }else
            {
                isShaking = false;
            }
        }
        
        IEnumerator FlashLightFlicker()
        {
            Lights.SetActive(!Lights.activeSelf);
                
            yield return new WaitForSeconds(Random.Range(0,1));
        }

        void ResetFlashlightTransform()
        {
            if (!isShaking)
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0,0, 0), Time.deltaTime*5.0f);
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), Time.deltaTime * 5.0f);
            }
        }
        
    }

