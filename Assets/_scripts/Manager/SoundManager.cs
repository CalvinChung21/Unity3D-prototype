using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;

namespace CommandPattern
{
    public static class SoundManager 
    {
        public enum Sound
        {
            recover,
            grab,
            throwing,
            flashlightButton,
            flashlightChange,
            playerHurt,
            ghostAttack,
            ghostDead,
            hello,
            evilLaugh,
            thankYou,
            healing,
            who,
            fire,
            increaseBattery,
            decreaseBattery
        }

        private static Dictionary<Sound, float> soundTimerDictionary;

        public static void Initialize()
        {
            soundTimerDictionary = new Dictionary<Sound, float>();
            soundTimerDictionary[Sound.ghostAttack] = 0;
            soundTimerDictionary[Sound.recover] = 0;
            soundTimerDictionary[Sound.who] = 0;
        }
        
        // generate a gameobject in random position and add audioSource component to it and play the required audio
        public static void PlaySound(Sound sound)
        {
            if (CanPlaySound(sound))
            {
                GameObject soundGameObject = new GameObject("Sound");
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.Play();
                Object.Destroy(soundGameObject, audioSource.clip.length);
            }
        }
        // for 3d audio
        // generate a gameobject and set the position and add audioSource component to it and add the audio clip to the game object and play it
        public static void PlaySound(Sound sound, Vector3 position)
        {
            if (CanPlaySound(sound))
            {
                GameObject soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                // some 3d audio setting
                audioSource.clip = GetAudioClip(sound);
                audioSource.maxDistance = 100f;
                audioSource.spatialBlend = 1f;
                audioSource.rolloffMode = AudioRolloffMode.Linear;
                audioSource.dopplerLevel = 0f;
                audioSource.Play();
                // destroy the game object when it is finished playing audio
                Object.Destroy(soundGameObject, audioSource.clip.length);
            }
        }
        
        // only play sound when it is finished playing
        private static bool CanPlaySound(Sound sound)
        {
            switch (sound)
            {
                default: return true;
                case Sound.ghostAttack:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float ghostAttackTimerMax = .0852f;
                        if (lastTimePlayed + ghostAttackTimerMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case Sound.recover:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float recoverTimerMax = 3.5f;
                        if (lastTimePlayed + recoverTimerMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case Sound.who:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float whoTimerMax = 0.72f;
                        if (lastTimePlayed + whoTimerMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                    break;
            }
        }
        // Get the corresponding audio clip from the array
        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                {
                    return soundAudioClip.AudioClip;
                }
            }
            Debug.LogError("Sound " + sound + " not found!");
            return null;
        }
        
    }
}