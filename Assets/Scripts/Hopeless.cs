using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hopeless : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && FlashLightToggle.notesNum>0)
        {
            FlashLightToggle.notesNum--;
            gameObject.GetComponent<Light>().enabled = true;
            SoundManagerScript.playSound("thank");
        }
    }

}
