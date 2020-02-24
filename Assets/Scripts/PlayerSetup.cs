using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    public Camera PlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            GetComponent<MPCarController>().enabled = true;
            GetComponent<LapController>().enabled = true;
            PlayerCamera.enabled = true;
        }
        else
        {
            GetComponent<MPCarController>().enabled = false;
            GetComponent<LapController>().enabled = false;
            PlayerCamera.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
