using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class FinishUI : MonoBehaviour {

    public float Duration = 5;

    public float BlinkInterval = 0.5f;

    private Text textComponent;

    private float runTime = 0;

	// Use this for initialization
	void Start()
    {
        textComponent = GetComponent<Text>();
        textComponent.enabled = false;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (runTime > 0)
        {
            if (Mathf.RoundToInt((Duration - runTime)/BlinkInterval) % 2 == 0)
            {
                textComponent.enabled = true;
            }
            else
            {
                textComponent.enabled = false;
            }
            if (runTime < Time.deltaTime)
            {
                var titleSequence = Camera.main.GetComponent<TitleSequence>();
                if (titleSequence != null)
                {
                    titleSequence.SetTitleSequenceActive(true);
                }
            }
        }
        else
        {
            textComponent.enabled = false;
        }

        runTime = Mathf.Max(runTime - Time.deltaTime, 0);
	}

    public void DoFinishSequence()
    {
        textComponent.text = "FINISH";
        runTime = Duration;
    }

    public void DoTimeoutSequence()
    {
        textComponent.text = "TIME'S UP";
        runTime = Duration;
    }
}
