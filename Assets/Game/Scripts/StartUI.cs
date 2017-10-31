using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StartUI : MonoBehaviour {

    public float Duration = 3;

    public GameObject RaceTimerObject;

    private float runTime = 0;

    private Text textComponent;

    private MonoBehaviour playerController;

    private RaceTimer raceTimer;

	// Use this for initialization
	void Start()
    {
        textComponent = GetComponent<Text>();
        textComponent.enabled = false;
        if (RaceTimerObject != null)
        {
            raceTimer = RaceTimerObject.GetComponent<RaceTimer>();
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        if (runTime > 0f)
        { 
            textComponent.enabled = true;
            if (runTime > 1f)
            {
                textComponent.text = Mathf.FloorToInt(runTime).ToString();
            }
            else
            {
                textComponent.text = "FIND THE CHEETO";
                if (playerController != null)
                {
                    playerController.enabled = true;
                }
                if (raceTimer != null)
                {
                    raceTimer.IsRunning = true;
                }
            }
        }
        else
        {
            textComponent.enabled = false;
        }


		
        runTime = Mathf.Max(runTime - Time.deltaTime, 0f);
	}

    public void DoStartSequence(BallController controller)
    {
        runTime = Duration + 0.999f;
        playerController = controller;
        if (playerController != null)
        {
            controller.ResetPosition();
            playerController.enabled = false;
        }
        if (raceTimer != null)
        {
            raceTimer.Reset();
            raceTimer.IsRunning = false;
        }
    }
}
