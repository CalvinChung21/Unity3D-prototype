using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class BGM : MonoBehaviour
    {
        // public AudioClip narrativeBgm;
        // public AudioClip gameLoseBgm;
        // public AudioClip bgm;
        // private AudioSource _bgm;
        
        private bool pressed = false;

        private void Awake()
        {
            // // audio setting
            // _bgm = GetComponent<AudioSource>();
            // _bgm.loop = true;
            SoundManager.Initialize();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // when in game main stage
            if (LevelState.Level == 3 || LevelState.Level == 6 || LevelState.Level == 9)
            {
                LevelState.NarrativeMode = false;
                LevelState.GameStart = true;
            }
            else
            {
                LevelState.NarrativeMode = true;
                LevelState.GameStart = false;
            }
            
            // // change bgm depending on the mode
            // if (LevelState.GameLose)
            // {  // game lose bgm
            //     _bgm.clip = gameLoseBgm;
            // }
            // else if(LevelState.GameStart)
            // { // normal in game bgm
            //     _bgm.clip = bgm;
            // }else
            // {
            //     _bgm.clip = narrativeBgm;
            // }
            // _bgm.Play();

        }

        void Update ()
        {
            // during narrative mode
            if (Input.GetKeyDown("space") && LevelState.NarrativeMode && !pressed)
            {
                pressed = true;
                if (LevelState.Level != 11)
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

            // when game over and want to restart
            if (Input.GetKey(KeyCode.R) && LevelState.GameLose && !pressed)
            {
                HopelessInfo.setCountHopeless(0);
                LevelState.NotesNum = 0;
                Flashlight.setBatteries(5);
                pressed = true;
                LevelState.GameLose = false;
                switch (LevelState.Level)
                {
                    case 3:SceneManager.LoadScene("level1");
                        break;
                    case 6: SceneManager.LoadScene("level2");
                        break;
                    case 9: SceneManager.LoadScene("level3");
                        break;
                    default: break;
                }
            }
            pressed = false;
        }
    }
