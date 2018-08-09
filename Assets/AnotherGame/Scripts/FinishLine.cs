using UnityEngine;

//
// Some terrible code written for a sample GobblerWare game
// Written by [redacted] August 2018 
//
public class FinishLine : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            winRace();
        }

    }

    private void winRace()
    {

        GameObject.Find("MyCar").GetComponent<Car>().WinRace();

    }

}
