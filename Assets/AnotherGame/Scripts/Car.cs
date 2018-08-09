using UnityEngine;

// Important: To call the GobblerWareManager public API, you must use the GobblerWare namespace!
using WizardJam.GobblerWare;

//
// Some terrible code written for a sample GobblerWare game
// Written by [redacted] August 2018 
//
public class Car : MonoBehaviour {

    public GameObject getReadyFlag;
    public AudioSource carSpeaker;

    public AudioClip boop;
    public AudioClip beep;
    public AudioClip carCrash;
    public AudioClip winRace;

    private enum eCarState
    {
        GETREADY = 0,
        MOVING = 1,
        WIN = 2,
        LOSE = 3
    }
    private eCarState currentCarState = eCarState.GETREADY;

    private int readyCount = 0;
    private float readyCountdown = 1.0f;

    private Vector3 startPosition = Vector3.zero;
    private Vector3 steerDistance = new Vector3(2.5f, 0f, 0f);
    private Vector3 moveDistance = new Vector3(0f, 0f, 5.0f);

    private void Awake()
    {
        startPosition = gameObject.transform.position;
        ResetCar(false);
    }

    void Start () {
		
	}

    void Update()
    {

        if (currentCarState == eCarState.GETREADY)
        {

            readyCountdown -= Time.deltaTime;

            if (readyCountdown <= 0.0f)
            {
                readyCount += 1;

                if (readyCount < 3)
                {
                    carSpeaker.clip = boop;
                }
                else
                {
                    carSpeaker.clip = beep;
                }

                carSpeaker.Play();
                readyCountdown = 1.0f;

                if (readyCount == 3)
                {
                    startRace();
                }

            }

        }
        else if (currentCarState == eCarState.MOVING)
        {

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Translate(-steerDistance * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Translate(steerDistance * Time.deltaTime);
            }

            transform.Translate(moveDistance * Time.deltaTime);

        }
        else if (currentCarState == eCarState.WIN)
        {

            if (carSpeaker.isPlaying == false)
            {
                // When the game is over, tell GobblerWare you're done and provide your score!
                GobblerWareManager.Instance.EndMyGame(1);
            }

        }

    }

    public void ResetCar(bool playCrashSound = true)
    {

        if (playCrashSound)
        {
            carSpeaker.PlayOneShot(carCrash);
        }

        gameObject.transform.position = startPosition;
        getReadyFlag.SetActive(true);
        readyCount = 0;
        currentCarState = eCarState.GETREADY;

    }

    public void WinRace()
    {

        carSpeaker.PlayOneShot(winRace);
        currentCarState = eCarState.WIN;

    }

    private void startRace()
    {

        getReadyFlag.SetActive(false);
        currentCarState = eCarState.MOVING;

    }

}
