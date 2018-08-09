using UnityEngine;

// Important: To call the GobblerWareManager public API, you must use the GobblerWare namespace!
using WizardJam.GobblerWare;

//
// Some terrible code written for a sample GobblerWare game
// Written by [redacted] August 2018 
//
public class PressSpaceToWin : MonoBehaviour {

	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        if(gameObject.transform.position.y < 0.0f)
        {
            // When the game is over, tell GobblerWare you're done and provide your score!
            GobblerWareManager.Instance.EndMyGame(99);
        }

	}

}
