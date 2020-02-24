using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPCarController : MonoBehaviour
{
    public void Start()
    {
        controlsEnabled = false;

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;

        AudioSource[] audios = GetComponents<AudioSource>();
        crashsfx = audios[1];

        lights = GameObject.FindGameObjectsWithTag("light");

        if (SceneManager.GetActiveScene().name != "Track3")
        {
            foreach (GameObject g in lights)
            {
                g.SetActive(false);
            }
        }
    }

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
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
        }
    }

    private void Update()
    {
        //Handbrake();
        currentSpeed = transform.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        pitch = currentSpeed / topSpeed;

        transform.GetComponent<AudioSource>().pitch = pitch;

        if (LapController.isRaceFinished == true)
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        crashsfx.Play();
    }

    void Handbrake()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rearDriverW.brakeTorque = 100;
            rearDriverW.motorTorque = 0;
            rearPassengerW.brakeTorque = 100;
            rearPassengerW.motorTorque = 0;
        }
    }

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public Transform centerOfMass;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public Rigidbody rb;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public bool controlsEnabled;

    /*public float comX;
    public float comY;
    public float comZ;*/

    public float topSpeed = 100; // km per hour
    public static float currentSpeed = 0;
    private float pitch = 0;
    AudioSource crashsfx;
    GameObject[] lights;
}
