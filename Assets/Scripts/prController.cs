using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class prController : MonoBehaviour
{
    GameObject[] pauseObjects, finishObjects;
    GameObject testObject;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("pauseui");
        finishObjects = GameObject.FindGameObjectsWithTag("finishui");
        hidePaused();
        hideFinished();
        //GameObject instance = Instantiate(Resources.Load("Track1", typeof(GameObject))) as GameObject;
        //testObject = Instantiate(Resources.Load("Player1", typeof(GameObject))) as GameObject;

        GameObject theObjecttr = Resources.Load(MenuManager.trName) as GameObject;
        GameObject instance1tr = Instantiate(theObjecttr);

        Transform newParenttr = GameObject.FindWithTag("racetrack").GetComponent<Transform>();
        instance1tr.transform.SetParent(newParenttr, false);

        

        /*if(TrackSelect.name == "Track2")
        {
            //RenderSettings.skybox = sky1;
            transform.GetComponent<Skybox>().material = sky1;
        }

        if (TrackSelect.name == "Track3")
        {
            //RenderSettings.skybox = sky2;
            transform.GetComponent<Skybox>().material = sky2;
        }

        if (TrackSelect.name == "Track4")
        {
            //RenderSettings.skybox = sky3;
            transform.GetComponent<Skybox>().material = sky3;
        }*/

        GameObject theObject = Resources.Load(MenuManager.p1Name) as GameObject;
        GameObject instance1 = Instantiate(theObject);

        Transform newParent = GameObject.FindWithTag("Player1").GetComponent<Transform>();
        instance1.transform.SetParent(newParent, false);

        GameObject theObject2 = Resources.Load("prCamera") as GameObject;
        GameObject instance12 = Instantiate(theObject2);

        Transform newParent2 = GameObject.FindWithTag(MenuManager.p1Tag).GetComponent<Transform>();
        instance12.transform.SetParent(newParent2, false);

        GameObject.FindGameObjectWithTag(MenuManager.p1Tag).GetComponent<CarControllerP2>().enabled = false;

        
    }

    void Update()
    {
        if(LapsP1.currentLap == 4)
        {
            Time.timeScale = 0;
            showFinished();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else
            {
                Time.timeScale = 1;
                hidePaused();
            }
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
        Time.timeScale = 1;
        hidePaused();
    }

    public void exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void restart()
    {
        SceneManager.LoadScene("PracticeMode");
    }

}