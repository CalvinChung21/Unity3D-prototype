using UnityEngine;

namespace CommandPattern
{
    public abstract class Command
    {
        public abstract void Execute(GameObject gameObject);
    }

    public class OpenFlashLight : Command
    {
        public override void Execute(GameObject light)
        {
            if (!light.activeSelf  && BatteryBar.enoughBatteries())
            {
                light.SetActive(true);
                FlashLightToggle.FlashlightActive = true;
                if (!GameObject.Find("FlareThrew(Clone)"))
                {
                    Patrol.patrolOn = false;
                }
            }
            else
            {
                light.SetActive(false);
                FlashLightToggle.FlashlightActive = false;
                if (!GameObject.Find("FlareThrew(Clone)"))
                {
                    Patrol.patrolOn = true;
                }
            }
            SoundManager.PlaySound(SoundManager.Sound.flashlightButton);
            
        }
    }

    public class WhiteFlashLight : Command
    {
        public override void Execute(GameObject gameObject)
        {
            if (FlashLightToggle.FlashlightActive && BatteryBar.enoughBatteries())
            {
                gameObject.GetComponent<Light>().color = Color.white;
                gameObject.GetComponent<Light>().innerSpotAngle = 45;
                gameObject.GetComponent<Light>().spotAngle = 83;
                FlashLightToggle.FlashlightMode = false;
                SoundManager.PlaySound(SoundManager.Sound.flashlightChange);
            }
        }
    }

    // public class RedFlashLight : Command
    // {
    //     public override void Execute(GameObject gameObject)
    //     {
    //         if (FlashLightToggle.FlashlightActive && BatteryBar.enoughBatteries())
    //         {
    //             gameObject.GetComponentInChildren<Light>().color = Color.red;
    //             gameObject.GetComponent<Light>().innerSpotAngle = 0;
    //             gameObject.GetComponent<Light>().spotAngle = 37;
    //             FlashLightToggle.FlashlightMode = true;
    //             SoundManager.PlaySound(SoundManager.Sound.flashlightChange);
    //         }
    //     }
    // }
}