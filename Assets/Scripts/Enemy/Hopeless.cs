using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopeless : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && FlashLightToggle.notesNum > 0 && GetComponentInChildren<Light>().enabled == false)
        {
            FlashLightToggle.notesNum--;
            GetComponentInChildren<Light>().enabled = true;
            GetComponentInChildren<BoxCollider>().enabled = true;
            SoundManagerScript.playSound("thank");
            countHopeless--;
        }

        if(collision.gameObject.tag == "Player" && GetComponentInChildren<Light>().enabled)
        {
            if (FlashLightToggle._currentBatteries < 4)
            {
                FlashLightToggle._currentBatteries++;
                SoundManagerScript.playSound("heal");
            }
        }
    }

    public static int countHopeless;
    private void Start()
    {
        countHopeless++;
    }

   
}
