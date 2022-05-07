using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Interactable : MonoBehaviour
{
   public UnityEvent onInteract;
   public int ID;

   void Start()
   {
      ID = Random.Range(0, 99999);
   }

   public void CollectNote()
   {
      LevelState.NotesNum++;
      SoundManager.PlaySound(SoundManager.Sound.grab, gameObject.transform.position);
      Destroy(gameObject);
   }

   public void SaveHopeless()
   {
      if (LevelState.NotesNum > 0)
      {
         GetComponentInChildren<SkinnedMeshRenderer>().material.EnableKeyword("_EMISSION");
         GetComponent<Hopeless>().savedAndHopeful = true;
         LevelState.NotesNum--;
         SoundManager.PlaySound(SoundManager.Sound.thankYou, gameObject.transform.position);
         HopelessInfo.changeCountHopeless(-1);
         GetComponent<AudioSource>().Stop();
      }
      
   }
}
