using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RaceTimer : MonoBehaviour {

    public float SecondsRemaining = 100;

    public bool IsRunning = true;

    private Text text;

	// Use this for initialization
	void Start()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (IsRunning)
        {
            SecondsRemaining = Mathf.Max(SecondsRemaining - Time.deltaTime, 0);

            text.text = GetFormattedTime(SecondsRemaining);
        }
	}

    public string GetFormattedTime(float seconds)
    {
        int intMinutes = Mathf.FloorToInt(seconds / 60);
        int intSeconds = Mathf.FloorToInt(seconds % 60);
        int intCentiseconds = Mathf.FloorToInt((seconds * 100) % 100);
        return string.Format("{0:00}:{1:00}.{2:00}", intMinutes, intSeconds, intCentiseconds);
    }
}
