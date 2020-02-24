using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalTimeP2 : MonoBehaviour
{
    public static Text totaltime;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LapsP2.currentLap >= 1)
        {
            time += Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            int miliseconds = Mathf.FloorToInt((time * 1000) % 1000);

            //update the label value
            totaltime = gameObject.GetComponent<Text>();
            totaltime.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, miliseconds);
        }
    }
}
