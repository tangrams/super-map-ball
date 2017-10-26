using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSequence : MonoBehaviour {

    public GameObject IntroObject;

    private MonoBehaviour ballController;
    private MonoBehaviour thirdPersonCamera;

	// Use this for initialization
	void Start () {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            ballController = player.GetComponent<BallController>();
            if (ballController != null)
            {
                ballController.enabled = false;
            }

            thirdPersonCamera = GetComponent<ThirdPersonCamera>();
            if (thirdPersonCamera != null)
            {
                thirdPersonCamera.enabled = false;
            }
        }

        if (IntroObject != null)
        {
            this.transform.SetParent(IntroObject.transform);
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            if (ballController != null)
            {
                ballController.enabled = true;
            }

            if (thirdPersonCamera != null)
            {
                thirdPersonCamera.enabled = true;
            }

            if (IntroObject != null)
            {
                IntroObject.SetActive(false);
            }
            this.transform.SetParent(null);
        }
	}
}
