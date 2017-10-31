using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

    public GameObject FinishUIObject;

    public GameObject RaceTimerObject;

    public GameObject[] StartPositions;

    private GameObject player;
    private FinishUI finishUI;
    private RaceTimer raceTimer;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (FinishUIObject != null)
        {
            finishUI = FinishUIObject.GetComponent<FinishUI>();
        }
        if (RaceTimerObject != null)
        {
            raceTimer = RaceTimerObject.GetComponent<RaceTimer>();
        }
        ResetPosition();
    }

	// Update is called once per frame
	void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * 2);
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
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        var start = StartPositions[Random.Range(0, StartPositions.Length)];
        if (start != null)
        {
            transform.position = start.transform.position;
        }
    }
}
