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
            }
            else
            {
                light.SetActive(false);
                FlashLightToggle.FlashlightActive = false;
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
                FlashLightToggle.FlashlightMode = false;
                SoundManager.PlaySound(SoundManager.Sound.flashlightChange);
            }
        }
    }

    public class RedFlashLight : Command
    {
        public override void Execute(GameObject gameObject)
        {
            if (FlashLightToggle.FlashlightActive && BatteryBar.enoughBatteries())
            {
                gameObject.GetComponent<Light>().color = Color.red;
                FlashLightToggle.FlashlightMode = true;
                SoundManager.PlaySound(SoundManager.Sound.flashlightChange);
            }
        }
    }
}