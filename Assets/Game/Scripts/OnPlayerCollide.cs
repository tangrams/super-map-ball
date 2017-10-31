using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerCollide : MonoBehaviour {

    public GameObject FinishUIObject;

    public GameObject RaceTimerObject;

    private GameObject player;
    private FinishUI finishUI;
    private RaceTimer raceTimer;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if (FinishUIObject != null)
        {
            finishUI = FinishUIObject.GetComponent<FinishUI>();
        }
        if (RaceTimerObject != null)
        {
            raceTimer = RaceTimerObject.GetComponent<RaceTimer>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            if (finishUI != null)
            {
                finishUI.DoFinishSequence();
            }
            if (raceTimer != null)
            {
                raceTimer.IsRunning = false;
            }

        }
    }
}
