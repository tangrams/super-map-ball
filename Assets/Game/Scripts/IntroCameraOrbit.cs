using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCameraOrbit : MonoBehaviour {

    public GameObject OrbitTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(OrbitTarget.transform.position, Vector3.up, 0.03f);
	}
}
