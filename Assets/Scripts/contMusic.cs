using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class contMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("bgmusic");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PracticeMode" || SceneManager.GetActiveScene().name == "SplitScreenMode" 
            || SceneManager.GetActiveScene().name == "Track1" || SceneManager.GetActiveScene().name == "Track2"
            || SceneManager.GetActiveScene().name == "Track3")
        {
            Destroy(this.gameObject);
        }
    }
}
