using UnityEngine.SceneManagement;

namespace CommandPattern
{
    public class LevelState
    {
        private static int level = SceneManager.sceneCount;
        private static bool gameStart = false;
        private static bool gameLose = false;
        private static bool narrativeMode = true;
        private static int notesNum = 0;
        public static int decoyNum = 4;
        public static int Level
        {
            get => level;
            set => level = value;
        }

        public static bool GameLose
        {
            get => gameLose;
            set => gameLose = value;
        }

        public static bool GameStart
        {
            get => gameStart;
            set => gameStart = value;
        }

        public static bool NarrativeMode
        {
            get => narrativeMode;
            set => narrativeMode = value;
        }

        public static int NotesNum
        {
            get => notesNum;
            set => notesNum = value;
        }
    }
}