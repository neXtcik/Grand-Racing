using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TimeCountdownManager : MonoBehaviourPun
{
    private Text TimeUIText;
    private float timeToStartRace = 4.0f;
    public static bool startTimer = false;

    private void Awake()
    {
        TimeUIText = MPGameManager.instance.TimeUIText;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (timeToStartRace >= 0.0f)
            {
                timeToStartRace -= Time.deltaTime;
                photonView.RPC("SetTime", RpcTarget.AllBuffered, timeToStartRace);
            }
            else if (timeToStartRace < 0.0f)
            {
                photonView.RPC("StartRace", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    public void SetTime(float time)
    {
        if(time > 0.0f)
        {
            TimeUIText.text = Mathf.Floor(time).ToString();
            if(Mathf.Floor(time) == 0)
            {
                TimeUIText.text = "START";
            }
        }
        else
        {
            TimeUIText.text = "";
        }
    }

    [PunRPC]
    public void StartRace()
    {
        GetComponent<MPCarController>().controlsEnabled = true;
        this.enabled = false;
        startTimer = true;
    }
}
