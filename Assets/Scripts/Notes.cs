using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FlashLightToggle.notesNum++;
            Destroy(gameObject);
            SoundManagerScript.playSound("grab");
        }
    }
}
