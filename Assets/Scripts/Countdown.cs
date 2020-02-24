using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private Text PracticeCountdownText;

    private Text SplitScreenCountdownText;

    private float timeToStartRace = 4.0f;
    // Start is called before the first frame update

    private void Awake()
    {
        PracticeCountdownText = RaceUIManager.instance.PracticeCountdownText;
        SplitScreenCountdownText = RaceUIManager.instance.SplitScreenCountdownText;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToStartRace >= 0.0f)
        {
            timeToStartRace -= Time.deltaTime;
            SetTime(timeToStartRace);
        }
        else if (timeToStartRace < 0.0f)
        {
            StartRace();
        }
    }

    public void SetTime(float time)
    {
        if(MenuManager.gm == "pr")
        {
            if (time > 0.0f)
            {
                PracticeCountdownText.text = Mathf.Floor(time).ToString();
                if (Mathf.Floor(time) == 0)
                {
                    PracticeCountdownText.text = "START";
                }
            }
            else
            {
                PracticeCountdownText.text = "";
            }
        }
        else if(MenuManager.gm == "ss")
        {
            if (time > 0.0f)
            {
                SplitScreenCountdownText.text = Mathf.Floor(time).ToString();
                if (Mathf.Floor(time) == 0)
                {
                    SplitScreenCountdownText.text = "START";
                }
            }
            else
            {
                SplitScreenCountdownText.text = "";
            }
        }
    }

    public void StartRace()
    {
        if (MenuManager.gm == "pr")
            GetComponent<SimpleCarController>().controlsEnabled = true;
        if (MenuManager.gm == "ss")
        {
            GetComponent<SimpleCarController>().controlsEnabled = true;
            GetComponent<CarControllerP2>().controlsEnabled = true;
        }
            
        this.enabled = false;
    }
}
