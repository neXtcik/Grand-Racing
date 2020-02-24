using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPSkyboxController : MonoBehaviour
{
    public Material sky1, sky2, sky3;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Track1")
        {
            //RenderSettings.skybox = sky3;
            transform.GetComponent<Skybox>().material = sky1;
        }

        if (SceneManager.GetActiveScene().name == "Track2")
        {
            //RenderSettings.skybox = sky3;
            transform.GetComponent<Skybox>().material = sky2;
        }

        if (SceneManager.GetActiveScene().name == "Track3")
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
