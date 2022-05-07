using CommandPattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
   private void OnEnable()
   {
      if (LevelState.Level != 10)
      {
         LevelState.Level+=1;
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
      else
      {
         LevelState.Level = 0;
         SceneManager.LoadScene("_scenes/Menu/Menu");
      }
   }
}
