using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    [Header("Login UI")]
    public GameObject LoginUIPanel;
    public InputField PlayerNameInput;

    [Header("Connecting Info Panel")]
    public GameObject ConnectingInfoUIPanel;

    [Header("Creating Room Info Panel")]
    public GameObject CreatingRoomInfoUIPanel;

    [Header("GameOptions  Panel")]
    public GameObject GameOptionsUIPanel;


    [Header("Create Room Panel")]
    public GameObject CreateRoomUIPanel;
    public InputField RoomNameInputField;
    public string Track;
    public int Size;

    [Header("Inside Room Panel")]
    public GameObject InsideRoomUIPanel;
    public Text RoomInfoText;
    public GameObject PlayerListPrefab;
    public GameObject PlayerListContent;
    public GameObject StartGameButton;
    public Text RoomNameText;
    public Image PanelBackground;
    public Sprite T1Background, T2Background, T3Background;


    [Header("Join Random Room Panel")]
    public GameObject JoinRandomRoomUIPanel;

    private Dictionary<int, GameObject> playerListGameObjects;

    #region UNITY Methods
    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(LoginUIPanel.name);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region UI Callback Methods

    public void OnLoginButtonClicked()
    {
        string playerName = PlayerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            ActivatePanel(ConnectingInfoUIPanel.name);
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName;
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        else
        {
            Debug.Log("Player name is invalid");
        }
    }

    public void OnLoginPanelBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnCancelButtonClicked()
    {
        ActivatePanel(GameOptionsUIPanel.name);
    }

    public void OnCreateRoomButtonClicked()
    {
        ActivatePanel(CreatingRoomInfoUIPanel.name);
        if (Track != null && Size != 0)
        {
            string roomName = RoomNameInputField.text;

            if (string.IsNullOrEmpty(roomName))
            {
                roomName = "Room" + Random.Range(1000, 10000);
            }

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = (byte)Size;

            string[] roomPropsInLobby = { "map" };

            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", Track } };

            roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
            roomOptions.CustomRoomProperties = customRoomProperties;

            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    public void OnJoinRandomRoomButtonClicked(string _track)
    {
        Track = _track;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", _track } };
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
    }

    public void OnBackButtonClicked()
    {
        ActivatePanel(GameOptionsUIPanel.name);
    }

    public void OnLeaveGameButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnStartGameButtonClicked()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("map"))
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t1"))
            {
                PhotonNetwork.LoadLevel("Track1");
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t2"))
            {
                PhotonNetwork.LoadLevel("Track2");
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t3"))
            {
                PhotonNetwork.LoadLevel("Track3");
            }
        }
    }

    #endregion


    #region Photon Callbacks

    public override void OnConnected()
    {
        Debug.Log("Connected");
    }

    public override void OnConnectedToMaster()
    {
        ActivatePanel(GameOptionsUIPanel.name);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name+ "Player count:"+ PhotonNetwork.CurrentRoom.PlayerCount);

        ActivatePanel(InsideRoomUIPanel.name);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("map"))
        {
            RoomNameText.text = PhotonNetwork.CurrentRoom.Name;

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t1"))
            {
                RoomInfoText.text = "Track: Track 1" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
                PanelBackground.sprite = T1Background;
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t2"))
            {
                RoomInfoText.text = "Track: Track 2" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
                PanelBackground.sprite = T2Background;
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t3"))
            {
                RoomInfoText.text = "Track: Track 3" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
                PanelBackground.sprite = T3Background;
            }

            if (playerListGameObjects == null)
            {
                playerListGameObjects = new Dictionary<int, GameObject>();
            }

            foreach (Player player in PhotonNetwork.PlayerList)
            {
                GameObject playerListGameObject = Instantiate(PlayerListPrefab);
                playerListGameObject.transform.SetParent(PlayerListContent.transform);
                playerListGameObject.transform.localScale = Vector3.one;
                playerListGameObject.GetComponent<PlayerListEntryInitializer>().Initialize(player.ActorNumber, player.NickName);

                object isPlayerReady;
                if(player.CustomProperties.TryGetValue(Multiplayer.PLAYER_READY, out isPlayerReady))
                {
                    playerListGameObject.GetComponent<PlayerListEntryInitializer>().SetPlayerReady((bool)isPlayerReady);
                }

                playerListGameObjects.Add(player.ActorNumber, playerListGameObject);
            }
        }

        StartGameButton.SetActive(false);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        GameObject playerListGameObject;
        if (playerListGameObjects.TryGetValue(targetPlayer.ActorNumber, out playerListGameObject))
        {
            object isPlayerReady;
            if(changedProps.TryGetValue(Multiplayer.PLAYER_READY, out isPlayerReady))
            {
                playerListGameObject.GetComponent<PlayerListEntryInitializer>().SetPlayerReady((bool)isPlayerReady);
            }
        }

        StartGameButton.SetActive(CheckPlayersReady());
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("map"))
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t1"))
            {
                RoomInfoText.text = "Track: Track 1" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t2"))
            {
                RoomInfoText.text = "Track: Track 2" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t3"))
            {
                RoomInfoText.text = "Track: Track 3" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
            }
        }
            /*RoomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;*/

        GameObject playerListGameObject = Instantiate(PlayerListPrefab);
        playerListGameObject.transform.SetParent(PlayerListContent.transform);
        playerListGameObject.transform.localScale = Vector3.one;
        playerListGameObject.GetComponent<PlayerListEntryInitializer>().Initialize(newPlayer.ActorNumber, newPlayer.NickName);

        playerListGameObjects.Add(newPlayer.ActorNumber, playerListGameObject);

        StartGameButton.SetActive(CheckPlayersReady());
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("map"))
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t1"))
            {
                RoomInfoText.text = "Track: Track 1" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t2"))
            {
                RoomInfoText.text = "Track: Track 2" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
            }
            else if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsValue("t3"))
            {
                RoomInfoText.text = "Track: Track 3" + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;
            }
        }
        /*RoomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
                " Players/Max. Players: " +
                PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                PhotonNetwork.CurrentRoom.MaxPlayers;*/

        Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
        playerListGameObjects.Remove(otherPlayer.ActorNumber);

        StartGameButton.SetActive(CheckPlayersReady());
    }

    public override void OnLeftRoom()
    {
        ActivatePanel(GameOptionsUIPanel.name);

        foreach(GameObject playerListGameObject in playerListGameObjects.Values)
        {
            Destroy(playerListGameObject);
        }
        playerListGameObjects.Clear();
        playerListGameObjects = null;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
            StartGameButton.SetActive(CheckPlayersReady());
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        if (Track != null)
        {
            string roomName = RoomNameInputField.text;

            if (string.IsNullOrEmpty(roomName))
            {
                roomName = "Room" + Random.Range(1000, 10000);
            }

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;

            string[] roomPropsInLobby = { "map" };

            ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "map", Track } };

            roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
            roomOptions.CustomRoomProperties = customRoomProperties;


            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    #endregion

    #region Public Methods
    public void ActivatePanel(string panelNameToBeActivated)
    {
        LoginUIPanel.SetActive(LoginUIPanel.name.Equals(panelNameToBeActivated));
        ConnectingInfoUIPanel.SetActive(ConnectingInfoUIPanel.name.Equals(panelNameToBeActivated));
        CreatingRoomInfoUIPanel.SetActive(CreatingRoomInfoUIPanel.name.Equals(panelNameToBeActivated));
        CreateRoomUIPanel.SetActive(CreateRoomUIPanel.name.Equals(panelNameToBeActivated));
        GameOptionsUIPanel.SetActive(GameOptionsUIPanel.name.Equals(panelNameToBeActivated));
        JoinRandomRoomUIPanel.SetActive(JoinRandomRoomUIPanel.name.Equals(panelNameToBeActivated));
        InsideRoomUIPanel.SetActive(InsideRoomUIPanel.name.Equals(panelNameToBeActivated));
    }

    public void SetTrack(string _track)
    {
        Track = _track;
    }

    public void SetRoomSize(int _size)
    {
        Size = _size;
    }
    #endregion


    #region Private Methods

    private bool CheckPlayersReady()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return false;
        }
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            object isPlayerReady;
            if(player.CustomProperties.TryGetValue(Multiplayer.PLAYER_READY, out isPlayerReady))
            {
                if (!(bool)isPlayerReady)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    #endregion
}
