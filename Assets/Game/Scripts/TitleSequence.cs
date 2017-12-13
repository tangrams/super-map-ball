using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSequence : MonoBehaviour {

    public GameObject IntroOrbitObject;
    public GameObject IntroUIObject;
    public GameObject GamePlayUIObject;
    public GameObject StartUIObject;

    private BallController ballController;
    private MonoBehaviour thirdPersonCamera;
    private StartUI startUI;

	// Use this for initialization
	void Start () {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            ballController = player.GetComponent<BallController>();
        }

        thirdPersonCamera = GetComponent<ThirdPersonCamera>();

        if (GamePlayUIObject != null)
        {
            GamePlayUIObject.SetActive(false);
        }

        if (StartUIObject != null)
        {
            startUI = StartUIObject.GetComponent<StartUI>();
        }

        SetTitleSequenceActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            SetTitleSequenceActive(false);
        }
	}

    public void SetTitleSequenceActive(bool titleSequenceActive)
    {
        if (ballController != null)
        {
            ballController.enabled = !titleSequenceActive;
        }

        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.enabled = !titleSequenceActive;
        }

        if (GamePlayUIObject != null)
        {
            GamePlayUIObject.SetActive(!titleSequenceActive);
        }

        if (IntroUIObject != null)
        {
            IntroUIObject.SetActive(titleSequenceActive);
            IntroOrbitObject.SetActive(titleSequenceActive);
        }

        if (!titleSequenceActive && startUI != null)
        {
            startUI.DoStartSequence(ballController);
        }

        if (titleSequenceActive && IntroUIObject != null)
        {
            this.transform.SetParent(IntroOrbitObject.transform);
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
        }
        else
        {
            this.transform.SetParent(null);
        }
    }
}
