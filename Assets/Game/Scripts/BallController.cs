using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

    public float forceMult = 30.0f;
    public float maxAngularVelocity = 20.0f;
    public float doubleTapTimeSeconds = 0.3f;
    public float nudgeForceFactor = 5.0f;
    public GameObject target;
    public Text distanceText;
    public GameObject targetDirection;

    public GameObject StartPosition;

    private Rigidbody rb;
    private float initialTargetDistance;
    private Vector3 vectorToTarget;
    private Vector3 currentDirection;
    private float lasTapTime;
    private float angle;

    void Awake() // Recommended to use Awake instead of Start here.
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVelocity;
        setDistance();
    }

    void Update() {
        setDistance();
    }

    void Nudge()
    {
        Vector3 randomForce = Random.onUnitSphere;
        if (randomForce.y < 0.0f)
        {
            randomForce.y *= -1.0f;
        }
        rb.AddForce(randomForce * nudgeForceFactor, ForceMode.Impulse);
        Debug.Log("Nudge");
    }

    void FixedUpdate() 
    {
        {
            #if UNITY_ANDROID || UNITY_IOS
            {
                // TODO: Test
                float moveH = Input.acceleration.x;
                float moveV = -Input.acceleration.z;

                Vector3 move = new Vector3(moveH, 0.0f, moveV);
                move = Camera.main.transform.TransformDirection(move);

                // var torque = Vector3.Cross(Camera.main.transform.up, move);
                // rb.AddTorque(torque * forceMult, ForceMode.VelocityChange);
                rb.AddForce(move * forceMult);

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (Time.time - lasTapTime < doubleTapTimeSeconds)
                    {
                        Nudge();
                    }

                    lasTapTime = Time.time;
                }
            }
            #else
            {
                float moveH = Input.GetAxis("Horizontal");
                float moveV = Input.GetAxis("Vertical");

                Vector3 move = new Vector3(moveH, 0.0f, moveV);
                move = Camera.main.transform.TransformDirection(move);

                var torque = Vector3.Cross(Camera.main.transform.up, move);
                rb.AddTorque(torque * forceMult, ForceMode.VelocityChange);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Nudge();
                }
            }
            #endif

        }
    }

    void setDistance()
    {
        Vector3 vectorToTargetWorld = target.transform.position - transform.position;

        int distanceToTarget = (int)vectorToTargetWorld.magnitude;
        distanceText.text = distanceToTarget.ToString();

        if (rb.velocity.magnitude > 0.0f)
        {
            currentDirection = rb.velocity.normalized;
        }

        var vectorToTargetLocal = Camera.main.transform.InverseTransformVector(vectorToTargetWorld);
        targetDirection.transform.localRotation = Quaternion.FromToRotation(Vector3.up, vectorToTargetLocal);
    }

    public void ResetPosition()
    {
        if (StartPosition != null)
        {
            transform.position = StartPosition.transform.position;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
