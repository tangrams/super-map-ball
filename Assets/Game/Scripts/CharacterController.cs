using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float forceMult = 30.0f;
    public float maxAngularVelocity = 20.0f;

    private Rigidbody rb;

    void Awake() // Recommended to use Awake instead of Start here.
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVelocity;
    }

    void FixedUpdate() 
    {
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                float moveH = Input.GetAxis("Horizontal");
                float moveV = Input.GetAxis("Vertical");

                Vector3 move = new Vector3(moveH, 0.0f, moveV);
                move = Camera.main.transform.TransformDirection(move);

                var torque = Vector3.Cross(Camera.main.transform.up, move);
                rb.AddTorque(torque * forceMult, ForceMode.VelocityChange);
            }
            else
            {
                // TODO: Test
                float moveH = Input.acceleration.x;
                float moveV = -Input.acceleration.z;

                Vector3 move = new Vector3(moveH, 0.0f, moveV);
                move = Camera.main.transform.TransformDirection(move);

                var torque = Vector3.Cross(Camera.main.transform.up, move);
                rb.AddTorque(torque * forceMult, ForceMode.VelocityChange);
            }
        }
    }
}
