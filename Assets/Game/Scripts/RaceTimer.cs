using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RaceTimer : MonoBehaviour {

    public const float SecondsAllowed = 90;

    public bool IsRunning = true;

    public GameObject FinishUIObject;

    private float secondsRemaining = SecondsAllowed;

    private Text text;

    private FinishUI finishUI;

	// Use this for initialization
	void Start()
    {
        text = GetComponent<Text>();
        if (FinishUIObject != null)
        {
            finishUI = FinishUIObject.GetComponent<FinishUI>();
        }
	}
	
	// Update is called once per frame
	void Update()
    {
        if (IsRunning)
        {
            if (secondsRemaining < Time.deltaTime && finishUI != null)
            {
                finishUI.DoTimeoutSequence();
                IsRunning = false;
            }
            secondsRemaining = Mathf.Max(secondsRemaining - Time.deltaTime, 0);

            text.text = GetFormattedTime(secondsRemaining);
        }
	}

    public void Reset()
    {
        secondsRemaining = SecondsAllowed;
    }

    public string GetFormattedTime(float seconds)
    {
        int intMinutes = Mathf.FloorToInt(seconds / 60);
        int intSeconds = Mathf.FloorToInt(seconds % 60);
        int intCentiseconds = Mathf.FloorToInt((seconds * 100) % 100);
        return string.Format("{0:00}:{1:00}.{2:00}", intMinutes, intSeconds, intCentiseconds);
    }
}
