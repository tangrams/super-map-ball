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
    public RawImage targetDirection;

    private Rigidbody rb;
    private float targetDirectionAngle;
    private float initialTargetDistance;
    private Vector3 vectorToTarget;
    private float lasTapTime;

    void Awake() // Recommended to use Awake instead of Start here.
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVelocity;
        targetDirectionAngle = 0.0f;
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
            if (SystemInfo.deviceType == DeviceType.Desktop)
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
            else
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
        }
    }

    void setDistance()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;

        int distanceToTarget = (int)vectorToTarget.magnitude;
        distanceText.text = distanceToTarget.ToString();

        Vector3 currentDirection;

        if (rb.velocity.magnitude > 0.0f)
        {
            currentDirection = rb.velocity.normalized;
        }
        else
        {
            currentDirection = -transform.position.normalized;
        }

        float angleToRotate = SignedAngleBetween(vectorToTarget.normalized, currentDirection, transform.up) + 90;

        targetDirection.rectTransform.Rotate(0.0f, 0.0f, angleToRotate - targetDirectionAngle);
        targetDirectionAngle = angleToRotate;
    }

    float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n){
        float angle = Vector3.Angle(a,b);
        float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));

        return angle * sign;
    }
}
