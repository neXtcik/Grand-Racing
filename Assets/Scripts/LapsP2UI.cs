using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapsP2UI : MonoBehaviour
{
    Text lapui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lapui = gameObject.GetComponent<Text>();
        if (LapsP2.currentLap == 0)
            lapui.text = "1/3";
        else if (LapsP2.currentLap == 4)
            lapui.text = "3/3";
        else
            lapui.text = LapsP2.currentLap + "/3";
    }
}
