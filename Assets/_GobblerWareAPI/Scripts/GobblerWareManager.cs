using UnityEngine;
using UnityEngine.SceneManagement;

namespace WizardJam.GobblerWare
{

    //
    // GobblerWareManager
    // A simple class that manages starting and ending a set of games, while tracking score. Good for "WarioWare" type
    // jam games, trash games, game anthologies, and so on.
    //
    // Written by Richard Walsh August 2018
    //
    public class GobblerWareManager : MonoBehaviour
    {

        private static GobblerWareManager _instance;
        public static GobblerWareManager Instance {
            get { return _instance; }
        }

        private bool initialized = false;
        private static readonly int MainMenuSceneIndex = 0;
        private int runningGameIndex = -1;

        private int totalScore = 0;
        public int TotalScore {
            get { return totalScore; }
        }

        void Awake()
        {

            if (_instance != null)
            {

                // This warning is very harmless, and can be ignored. It should indicate that you are returning to the 
                // main level. It's here for catching duplicate prefabs and what not.
                Debug.Log("GobblerWareManager:: Duplicate GobblerWareManager, deleting duplicate instance. If you saw this message when loading into the main scene, ignore. Otherwise, it's still not a problem, but might indicate redundant GobblerWareManager in another Scene file or something similar.");
                Destroy(this.gameObject);

            }
            else
            {

                _instance = this;
                DontDestroyOnLoad(this.gameObject);

                if (!initialized)
                {
                    initialized = true;
                    initialize();
                }

            }

        }

        private void initialize()
        {

            totalScore = 0;

        }

        #region PUBLIC_API

        /// <summary>
        /// To be called only by UI buttons or controls that should start a game.
        /// </summary>
        /// <param name="gameIndex">The Scene Index (in Build Settings) of the main scene of the game to start.</param>
        public string StartAGame(int gameIndex)
        {

            string resultText = "";
            string scenePath = SceneUtility.GetScenePathByBuildIndex(gameIndex);
            
            if(scenePath != "")
            {

                runningGameIndex = gameIndex;
                SceneManager.LoadScene(gameIndex);
                
            }
            else
            {

                Debug.LogError("GobblerWareManager:: Failed to load game at index '" + gameIndex + "'. No such scene exists in Build Settings.");
                resultText = "Failed to load game at index '" + gameIndex + "'. No Scene exists in Build Settings for this game!";

            }

            return resultText;

        }

        /// <summary>
        /// Call this function when your game is ready to exit
        /// </summary>
        /// <param name="score">The score achieved during the game that is about to end</param>
        public void EndMyGame(int score)
        {

            Debug.Log("Game at index '" + runningGameIndex + "' ended with score '" + score + "'");
            totalScore += score;
            runningGameIndex = -1;
            SceneManager.LoadScene(MainMenuSceneIndex);

        }

        #endregion

    }

}