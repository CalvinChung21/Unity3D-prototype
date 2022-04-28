using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CommandPattern
{
    public class BGM : MonoBehaviour
    {
        public AudioClip narrativeBgm;
        public AudioClip gameLoseBgm;
        public AudioClip bgm;
        private AudioSource _bgm;
    
        private void Awake()
        {
            // when in game main stage
            if (LevelState.Level == 4 || LevelState.Level == 7 || LevelState.Level == 10)
            {
                LevelState.NarrativeMode = false;
                LevelState.GameStart = true;
            }
            else
            {
                LevelState.NarrativeMode = true;
                LevelState.GameStart = false;
            }
            // when game over
            if (LevelState.Level == 12)
            {
                LevelState.NarrativeMode = false;
                LevelState.GameStart = false;
                LevelState.GameLose = true;
            }
            else
            {
                LevelState.GameLose = false;
            }
            
            SoundManager.Initialize();
            // audio setting
            _bgm = GetComponent<AudioSource>();
            _bgm.loop = true;
            
            // change bgm depending on the mode
            if (LevelState.NarrativeMode)
            {   // narrative bgm
                _bgm.clip = narrativeBgm;
            }else if (LevelState.GameLose)
            {  // game lose bgm
                _bgm.clip = gameLoseBgm;
            }
            else if(LevelState.GameStart)
            { // normal in game bgm
                _bgm.clip = bgm;
            }
            _bgm.Play();
        }

        void Update ()
        {
            // during narrative mode
            if (Input.GetKeyDown("space") && LevelState.NarrativeMode)
            {
                LevelState.Level+=1;
                Debug.Log(LevelState.Level);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            // when win the game
            if (HopelessInfo.getCountHopeless() == 0 && LevelState.GameStart)
            {
                StartCoroutine(changeLevel());
            }
            // when game over and want to restart
            if (Input.GetKey(KeyCode.R) && LevelState.GameLose)
            {
                switch (LevelState.Level)
                {
                    case 4:SceneManager.LoadScene("level1");
                        break;
                    case 7: SceneManager.LoadScene("level2");
                        break;
                    case 10: SceneManager.LoadScene("level3");
                        break;
                    default: break;
                }
            }
        }

        IEnumerator changeLevel()
        {
            yield return new WaitForSeconds(2);
            LevelState.Level+=1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Reset();
        }

        void Reset()
        {
            // reset the setting
            HopelessInfo.setCountHopeless(0);
            LevelState.NotesNum = 0;
            Flashlight.setBatteries(5);
        }
    }

}
