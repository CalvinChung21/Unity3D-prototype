using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Hopeless : MonoBehaviour
    {
        // whether the hopeless guy is being saved
        public bool savedAndHopeful = false;

        [SerializeField]
        Transform savePoint;
        
        private void Awake()
        {
            HopelessInfo.changeCountHopeless(1);
        }

        private void OnCollisionEnter(Collision collision)
        {
            #region saving the hopeless guy with note
            if (collision.gameObject.tag == "MainCamera" && LevelState.NotesNum > 0 && !savedAndHopeful)
            {
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

        private void Update()
        {
            if (savedAndHopeful)
            {
                transform.position = Vector3.MoveTowards(transform.position, savePoint.position, Time.deltaTime*2.0f);
            }

            if (transform.position == savePoint.position)
            {
                GetComponentInChildren<SkinnedMeshRenderer>().material.DisableKeyword("_EMISSION");
            }
        }
    }

