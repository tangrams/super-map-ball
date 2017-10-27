using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlayerOnCollide : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            collision.rigidbody.AddForce(Vector3.up * 50, ForceMode.Impulse);
        }
    }
}
