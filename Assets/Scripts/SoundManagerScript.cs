using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip flashlightButton;
    public static AudioClip moan;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        flashlightButton = Resources.Load<AudioClip>("flashlightOn");
        moan = Resources.Load<AudioClip>("moan");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void playSound(string objectName)
    {
        switch (objectName)
        {
            case "flashlight": audioSrc.PlayOneShot(flashlightButton); break;
            case "NPC": if(!audioSrc.isPlaying)audioSrc.PlayOneShot(moan); break;
            default:break;
        }
    }
}
