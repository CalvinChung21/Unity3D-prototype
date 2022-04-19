using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace CommandPattern
{
    public class LightSeeker : MonoBehaviour
    {
        private bool attack = false;
        #region whenHitingThePlayer

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 8 && !attack)
            {
                ScreenShake.Execute();
                HealthBarFade.Damage();
                SoundManager.PlaySound(SoundManager.Sound.playerHurt, gameObject.transform.position);
                attack = true;
            }
        }

        #endregion

        IEnumerator attackCD()
        {
            yield return new WaitForSeconds(3);
            attack = false;
        }
    }

}