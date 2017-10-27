using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceTimer : MonoBehaviour {

    public float SecondsRemaining = 100;

    private Text text;

	// Use this for initialization
	void Start()
    {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update()
    {
        SecondsRemaining = Mathf.Max(SecondsRemaining - Time.deltaTime, 0);

        int minutes = Mathf.FloorToInt(SecondsRemaining / 60);
        int seconds = Mathf.FloorToInt(SecondsRemaining % 60);
        int centiseconds = Mathf.FloorToInt((SecondsRemaining * 100) % 100);
        text.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);
	}
}
