using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPRaceMenuController : MonoBehaviour
{
    GameObject[] pauseObjects, finishObjects;
    private bool paused = true;
    // Start is called before the first frame update
    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("pauseui");
        finishObjects = GameObject.FindGameObjectsWithTag("finishui");
        hidePaused();
        hideFinished();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                hidePaused();
                paused = false;
            }
            else
            {
                showPaused();
                paused = true;
            }
        }

        if (LapController.isRaceFinished == true)
        {
            showFinished();
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void showFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    public void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    public void cont()
    {
        hidePaused();
    }

    public void exit()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("LobbyScene");
    }
}
