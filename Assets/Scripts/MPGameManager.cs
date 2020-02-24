using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MPGameManager : MonoBehaviour
{
    public GameObject[] PlayerPrefabs;
    public Transform[] InstantiatePositions;
    public Text TimeUIText;
    public GameObject[] FinishOrderUIGameObjects;

    public List<GameObject> checkpoints = new List<GameObject>();

    public static MPGameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            object playerSelectionNumber;
            if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(Multiplayer.PLAYER_SELECTION_NUMBER, out playerSelectionNumber))
            {
                Debug.Log((int)playerSelectionNumber);

                int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
                Vector3 instantiatePosition = InstantiatePositions[actorNumber - 1].position;

                PhotonNetwork.Instantiate(PlayerPrefabs[(int)playerSelectionNumber].name,instantiatePosition,Quaternion.identity);
            }
        }

        foreach (GameObject gm in FinishOrderUIGameObjects)
        {
            gm.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
