using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeP1 : MonoBehaviour
{
    Text laptime;

    private float time;
    List<string> lapTimes = new List<string>();
    void Update()
    {
        if(LapsP1.currentCheckpoint == 1)
        {
            time += Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            int miliseconds = Mathf.FloorToInt((time*1000)%1000);

            //update the label value
            laptime = gameObject.GetComponent<Text>();
                string l = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, miliseconds);
            lapTimes.Add(l);
            for (int i = 0; i < lapTimes.Count; i++)
            {
                laptime.text = lapTimes[i].ToString() + '\n';
            }
                //time = 0;
                //laptime.text = TotalTimeP1.totaltime.text + "\n";
        }
    }
}
