using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    public static Text test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (!other.CompareTag(MenuManager.p1Tag) && !other.CompareTag(ssController.instance1.tag) && !other.CompareTag(ssController.instance1P2.tag))
            return;

        if (other.CompareTag(MenuManager.p1Tag) || other.CompareTag(ssController.instance1.tag))
        {
            if (transform == LapsP1.checkpointA[LapsP1.currentCheckpoint].transform)
            {
                //Check so we dont exceed our checkpoint quantity
                if (LapsP1.currentCheckpoint + 1 < LapsP1.checkpointA.Length)
                {
                    //Add to currentLap if currentCheckpoint is 0
                    if (LapsP1.currentCheckpoint == 0)
                        LapsP1.currentLap++;
                    LapsP1.currentCheckpoint++;

                }
                else
                {
                    //If we dont have any Checkpoints left, go back to 0
                    LapsP1.currentCheckpoint = 0;
                }
            }
        }

        if (other.CompareTag(ssController.instance1P2.tag))
        {
            if (transform == LapsP2.checkpointA[LapsP2.currentCheckpoint].transform)
            {
                if (LapsP2.currentCheckpoint + 1 < LapsP2.checkpointA.Length)
                {
                    if (LapsP2.currentCheckpoint == 0)
                        LapsP2.currentLap++;
                    LapsP2.currentCheckpoint++;
                }
                else
                {
                    LapsP2.currentCheckpoint = 0;
                }
            }
        }
    }
}
