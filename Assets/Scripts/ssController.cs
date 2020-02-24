using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ssController : MonoBehaviour
{
    GameObject[] w1Objects, l1Objects, w2Objects, l2Objects, pauseObjects, finishObjects;

    public static GameObject instance1, instance1P2;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("pauseui");
        finishObjects = GameObject.FindGameObjectsWithTag("finishui");
        w1Objects = GameObject.FindGameObjectsWithTag("winp1");
        l1Objects = GameObject.FindGameObjectsWithTag("losep1");
        w2Objects = GameObject.FindGameObjectsWithTag("winp2");
        l2Objects = GameObject.FindGameObjectsWithTag("losep2");
        hideP1win();
        hideP2win();
        hidePaused();
        hideFinished();
        //GameObject instance = Instantiate(Resources.Load("Track1", typeof(GameObject))) as GameObject;

        GameObject theObjecttr = Resources.Load(MenuManager.trName) as GameObject;
        GameObject instance1tr = Instantiate(theObjecttr);

        Transform newParenttr = GameObject.FindWithTag("racetrack").GetComponent<Transform>();
        instance1tr.transform.SetParent(newParenttr, false);

        GameObject theObject = Resources.Load(MenuManager.p1Name) as GameObject;
        instance1 = Instantiate(theObject);

        Transform newParent = GameObject.FindWithTag("Player1").GetComponent<Transform>();
        instance1.transform.SetParent(newParent, false);

        GameObject theObject2 = Resources.Load("ssP1Camera") as GameObject;
        GameObject instance12 = Instantiate(theObject2);

        instance1.tag = "carP1";

        Transform newParent2 = GameObject.FindWithTag(instance1.tag).GetComponent<Transform>();
        instance12.transform.SetParent(newParent2, false);


        GameObject theObjectP2 = Resources.Load(MenuManager.p2Name) as GameObject;
        instance1P2 = Instantiate(theObjectP2);

        Transform newParentP2 = GameObject.FindWithTag("Player2").GetComponent<Transform>();
        instance1P2.transform.SetParent(newParentP2, false);

        GameObject theObject2P2 = Resources.Load("ssP2Camera") as GameObject;
        GameObject instance12P2 = Instantiate(theObject2P2);

        instance1P2.tag = "carP2";

        Transform newParent2P2 = GameObject.FindWithTag(instance1P2.tag).GetComponent<Transform>();
        instance12P2.transform.SetParent(newParent2P2, false);

        //GameObject.FindGameObjectWithTag(instance1P2.tag).GetComponent<CarControllerP2>().enabled = true;
        GameObject.FindGameObjectWithTag(instance1P2.tag).GetComponent<SimpleCarController>().enabled = false;
        GameObject.FindGameObjectWithTag(instance1.tag).GetComponent<CarControllerP2>().enabled = false;
    }

    void Update()
    {
        if(LapsP1.currentLap == 4 && LapsP2.currentLap < 4)
        {
            Time.timeScale = 0;
            showP1win();
            showFinished();
        }
        else if (LapsP1.currentLap < 4 && LapsP2.currentLap == 4)
        {
            Time.timeScale = 0;
            showP2win();
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

    public void showP1win()
    {
        foreach (GameObject g in w1Objects)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in l2Objects)
        {
            g.SetActive(true);
        }
    }

    public void showP2win()
    {
        foreach (GameObject g in l1Objects)
        {
            g.SetActive(true);
        }

        foreach (GameObject g in w2Objects)
        {
            g.SetActive(true);
        }
    }

    public void hideP1win()
    {
        foreach (GameObject g in w1Objects)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in l2Objects)
        {
            g.SetActive(false);
        }
    }

    public void hideP2win()
    {
        foreach (GameObject g in l1Objects)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in w2Objects)
        {
            g.SetActive(false);
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
        SceneManager.LoadScene("SplitScreenMode");
    }
}
