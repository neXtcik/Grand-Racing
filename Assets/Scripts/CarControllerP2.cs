using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerP2 : MonoBehaviour
{
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    private bool paused;

    public Transform centerOfMass;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public Rigidbody rb;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public bool controlsEnabled;

    public float topSpeed = 200; // km per hour
    public static float currentSpeed = 0;
    private float pitch = 0;
    AudioSource crashsfx;
    GameObject[] lights;
    private bool braked = false;

    public void Start()
    {
        controlsEnabled = false;

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;

        AudioSource[] audios = GetComponents<AudioSource>();
        crashsfx = audios[1];

        lights = GameObject.FindGameObjectsWithTag("light");

        if (MenuManager.trName != "Track4")
        {
            foreach (GameObject g in lights)
            {
                g.SetActive(false);
            }
        }
    }

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal2");
        m_verticalInput = Input.GetAxis("Vertical2");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
        rearDriverW.motorTorque = m_verticalInput * motorForce;
        rearPassengerW.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        if (controlsEnabled)
        {
            GetInput();
            Steer();
            Accelerate();
            UpdateWheelPoses();
            Handbrake();
        }
    }

    private void Update()
    {
        currentSpeed = transform.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        pitch = currentSpeed / topSpeed;

        transform.GetComponent<AudioSource>().pitch = pitch;
    }

    private void Handbrake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            braked = true;
        }
        else
        {
            braked = false;
        }
        if (braked)
        {
            rearDriverW.brakeTorque = 100;
            rearPassengerW.brakeTorque = 100;
            rearDriverW.motorTorque = 0;
            rearPassengerW.motorTorque = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        crashsfx.Play();
    }
}
