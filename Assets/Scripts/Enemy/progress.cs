using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class progress : MonoBehaviour
{
    void Update ()
    {
        if (Hopeless.countHopeless == 0)
        {
            StartCoroutine(changeLevel());
        }
    }

    IEnumerator changeLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FlashLightToggle._currentBatteries = 4;
        
    }
}
