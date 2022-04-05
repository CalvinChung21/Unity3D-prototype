using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace  CommandPattern
{
    public class Notes : MonoBehaviour
    {
        #region whenPlayerCollideWithNote
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                LevelState.NotesNum++;
                SoundManager.PlaySound(SoundManager.Sound.grab, gameObject.transform.position);
                Destroy(gameObject);
            }
        }
        #endregion
    }

}
