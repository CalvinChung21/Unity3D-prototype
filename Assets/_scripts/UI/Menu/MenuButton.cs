using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController menuButtonController;

    [SerializeField] Animator animator;

    [SerializeField] AnimatorFunctions animatorFunctions;

    [SerializeField] int thisIndex;

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
                StartCoroutine(playAudioAndLoadNextLV());
            }else if(animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctions.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }

    IEnumerator playAudioAndLoadNextLV()
    {
        yield return new WaitForSeconds(1);
        if (thisIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }else if (thisIndex == 2)
        {
            Application.Quit();
        }
    }
}
