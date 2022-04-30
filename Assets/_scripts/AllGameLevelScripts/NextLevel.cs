using CommandPattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
   private void OnEnable()
   {
      LevelState.Level+=1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
