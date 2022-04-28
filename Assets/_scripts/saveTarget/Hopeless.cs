using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Hopeless : MonoBehaviour
    {
        // whether the hopeless guy is being saved
        private bool savedAndHopeful = false;

        private void Awake()
        {
            HopelessInfo.changeCountHopeless(1);
        }

        private void OnCollisionEnter(Collision collision)
        {
            #region saving the hopeless guy with note
            if (collision.gameObject.tag == "MainCamera" && LevelState.NotesNum > 0 && !savedAndHopeful)
            {
                GetComponentInChildren<Light>().enabled = true;
                // a box collider as a safe area for the player to avoid danger
                GetComponentInChildren<BoxCollider>().enabled = true;
                GetComponentInChildren<SkinnedMeshRenderer>().material.EnableKeyword("_EMISSION");
                    
                savedAndHopeful = true;
                LevelState.NotesNum--;
                SoundManager.PlaySound(SoundManager.Sound.thankYou, gameObject.transform.position);
                HopelessInfo.changeCountHopeless(-1);
            }
            #endregion
        
            #region touching the hopeful guy will increase the battery percentage of the player
            if(collision.gameObject.tag == "MainCamera" && savedAndHopeful)
            {
                HealthBarFade.Heal();
                SoundManager.PlaySound(SoundManager.Sound.healing, gameObject.transform.position);
            }
            #endregion
        }
    }

