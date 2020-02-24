using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class LapController : MonoBehaviourPun
{
    private List<GameObject> Checkpoints = new List<GameObject>();

    public enum RaiseEventsCode
    {
        WhoFinishedEventCode = 0
    }

    private int finishOrder = 0;

    private int maxLaps = 3;
    public static int lapCounter = 1;
    public static bool isRaceFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject checkpoints in MPGameManager.instance.checkpoints)
        {
            Checkpoints.Add(checkpoints);
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)RaiseEventsCode.WhoFinishedEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;

            string nickNameOfFinishedPlayer = (string)data[0];

            finishOrder = (int)data[1];

            Debug.Log(nickNameOfFinishedPlayer + " " + finishOrder);

            GameObject orderUITextGameObject = MPGameManager.instance.FinishOrderUIGameObjects[finishOrder - 1];
            orderUITextGameObject.SetActive(true);

            orderUITextGameObject.GetComponent<Text>().text = finishOrder + ". " + nickNameOfFinishedPlayer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Checkpoints.Contains(other.gameObject))
        {
            int indexOfTrigger = Checkpoints.IndexOf(other.gameObject);

            //Checkpoints[indexOfTrigger].SetActive(false);

            if(other.name == "FinishCheckpoint")
            {
                if(lapCounter < maxLaps)
                {
                    lapCounter++;
                }
                else if(lapCounter == maxLaps)
                {
                    isRaceFinished = true;
                    GameFinished();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameFinished()
    {
        GetComponent<PlayerSetup>().PlayerCamera.transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        //GetComponent<MPCarController>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        finishOrder += 1;

        string nickName = photonView.Owner.NickName;

        object[] data = new object[] { nickName, finishOrder };

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache
        };

        SendOptions sendOptions = new SendOptions
        {
            Reliability = false
        };

        PhotonNetwork.RaiseEvent((byte)RaiseEventsCode.WhoFinishedEventCode, data, raiseEventOptions, sendOptions);
    }
}
