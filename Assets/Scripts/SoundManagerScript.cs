using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip flashlightButton;
    public static AudioClip moan;
    public static AudioClip flashlightMode;
    public static AudioClip ah;
    public static AudioClip grab;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        flashlightButton = Resources.Load<AudioClip>("flashlightOn");
        moan = Resources.Load<AudioClip>("moan");
        flashlightMode = Resources.Load<AudioClip>("flashlightMode");
        ah = Resources.Load<AudioClip>("ah");
        grab = Resources.Load<AudioClip>("grab");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void playSound(string objectName)
    {
        switch (objectName)
        {
            case "flashlight": audioSrc.PlayOneShot(flashlightButton); break;
            case "NPC": audioSrc.PlayOneShot(moan); break;
            case "flashlightMode": audioSrc.PlayOneShot(flashlightMode);break;
            case "ah": audioSrc.PlayOneShot(ah);break;
            case "grab": audioSrc.PlayOneShot(grab); break;
            default:break;
        }
    }
}
