using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    [Header("Title Screen Panel")]
    public GameObject TitleScreenPanel;

    [Header("Credits Panel")]
    public GameObject CreditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(TitleScreenPanel.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartGameButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnCreditsButtonClicked()
    {
        ActivatePanel(CreditsPanel.name);
    }

    public void OnCreditsBackButtonClicked()
    {
        ActivatePanel(TitleScreenPanel.name);
    }

    public void OnQuitGameButtonClicked()
    {
        Application.Quit();
    }

    public void ActivatePanel(string panelNameToBeActivated)
    {
        TitleScreenPanel.SetActive(TitleScreenPanel.name.Equals(panelNameToBeActivated));
        CreditsPanel.SetActive(CreditsPanel.name.Equals(panelNameToBeActivated));
    }
}
