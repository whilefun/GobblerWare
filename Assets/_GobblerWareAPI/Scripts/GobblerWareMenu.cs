using UnityEngine;
using UnityEngine.UI;

namespace WizardJam.GobblerWare
{

    //
    // GobblerWareMenu
    // This class takes button press inputs (through Unity Events on UI Buttons) and tells the 
    // GobblerWareManager to start a specific game. On Start(), the total score text is refreshed.
    //
    // Written by Richard Walsh August 2018
    //
    public class GobblerWareMenu : MonoBehaviour
    {

        private Text globalScoreText = null;
        private Text errorText = null;

        private void Awake()
        {

            globalScoreText = gameObject.transform.Find("GameSelectorCanvas/MenuBackground/TotalGlobalScore").GetComponent<Text>();
            errorText = gameObject.transform.Find("GameSelectorCanvas/MenuBackground/ErrorText").GetComponent<Text>();

            if (!globalScoreText || !errorText)
            {
                Debug.LogError("GobblerWareMenu:: Cannot find GlobalScoreText child Text object. Did you change the GobblerWareMenu prefab?");
            }

        }

        private void Start()
        {
            globalScoreText.text = "Total Score: " + GobblerWareManager.Instance.TotalScore;
        }

        public void BeginGame(int gameIndex)
        {

            string resultText = GobblerWareManager.Instance.StartAGame(gameIndex);

            if(resultText != "")
            {
                errorText.text = "Error: " + resultText;
            }
            else
            {
                errorText.text = "";
            }

        }

        public void QuitToDesktop()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }

    }

}