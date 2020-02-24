using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUIP1 : MonoBehaviour
{
    Text speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        speed = gameObject.GetComponent<Text>();
        speed.text = Mathf.FloorToInt(SimpleCarController.currentSpeed) + "km/h";
    }
}
