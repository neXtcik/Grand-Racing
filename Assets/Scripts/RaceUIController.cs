using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceUIController : MonoBehaviour
{
    public Text currentLapText;
    public Text totalTimeText;
    public Text lapTimesText;
    public Text speedometerText;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLapText.text = LapController.lapCounter.ToString() + "/3";
        int rSpeed = Mathf.RoundToInt(MPCarController.currentSpeed);
        speedometerText.text = rSpeed.ToString() + "km/h";
        if(TimeCountdownManager.startTimer == true)
        {
            time += Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            int miliseconds = Mathf.FloorToInt((time * 1000) % 1000);

            totalTimeText.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, miliseconds);
        }
    }
}
