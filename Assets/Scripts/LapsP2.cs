using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapsP2 : MonoBehaviour
{
    public Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0;
    public static int currentLap = 0;
    public Vector3 startPos;
    public int Lap;
    public int Checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Lap = currentLap;
        Checkpoint = currentCheckpoint;
        checkpointA = checkPointArray;
    }
}
