using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public Material sky1, sky2, sky3;
    // Start is called before the first frame update
    void Start()
    {
        if (MenuManager.trName == "Track2")
        {
            //RenderSettings.skybox = sky3;
            transform.GetComponent<Skybox>().material = sky1;
        }

        if (MenuManager.trName == "Track3")
        {
            //RenderSettings.skybox = sky3;
            transform.GetComponent<Skybox>().material = sky2;
        }

        if (MenuManager.trName == "Track4")
        {
            //RenderSettings.skybox = sky3;
            transform.GetComponent<Skybox>().material = sky3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
