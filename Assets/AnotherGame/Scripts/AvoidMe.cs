using UnityEngine;

//
// Some terrible code written for a sample GobblerWare game
// Written by [redacted] August 2018 
//
public class AvoidMe : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            resetGame();
        }

    }

    private void resetGame()
    {

        GameObject.Find("MyCar").GetComponent<Car>().ResetCar();

    }

}
