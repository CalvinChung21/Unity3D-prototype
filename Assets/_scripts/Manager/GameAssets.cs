using UnityEngine;


    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _i;

        public static GameAssets i
        {
            get
            {
                if(_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
                return _i;
            }
        }

        // to store all the audio clip used for the game
        public SoundAudioClip[] soundAudioClipArray;

        [System.Serializable]
        public class SoundAudioClip
        {
            public SoundManager.Sound sound;
            public AudioClip AudioClip;
        }

        
    }