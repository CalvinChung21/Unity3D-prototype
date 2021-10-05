using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    // reference code from https://www.youtube.com/watch?v=NRPUmRb994o
    public static AudioClip flashlightButton;
    public static AudioClip moan;
    public static AudioClip flashlightMode;
    public static AudioClip ah;
    public static AudioClip grab;
    public static AudioClip throwDecoy;
    public static AudioClip hello;
    public static AudioClip haha;
    public static AudioClip evilLaugh;
    public static AudioClip thank;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        flashlightButton = Resources.Load<AudioClip>("flashlightOn");
        moan = Resources.Load<AudioClip>("moan");
        flashlightMode = Resources.Load<AudioClip>("flashlightMode");
        ah = Resources.Load<AudioClip>("ah");
        grab = Resources.Load<AudioClip>("grab");
        throwDecoy = Resources.Load<AudioClip>("throw");
        hello = Resources.Load<AudioClip>("hello");
        haha = Resources.Load<AudioClip>("haha");
        evilLaugh = Resources.Load<AudioClip>("evilLaugh");
        thank = Resources.Load<AudioClip>("thank");
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
            case "throw": audioSrc.PlayOneShot(throwDecoy); break;
            case "hello": audioSrc.PlayOneShot(hello); break;
            case "haha": audioSrc.PlayOneShot(haha); break;
            case "evilLaugh": audioSrc.PlayOneShot(evilLaugh); break;
            case "thank": audioSrc.PlayOneShot(thank); break;
            default:break;
        }
    }
}
