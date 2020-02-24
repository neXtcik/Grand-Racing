using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Game Mode Panel")]
    public GameObject GameModePanel;

    [Header("Practice Car Select Panel")]
    public GameObject PracticeCarSelectPanel;

    [Header("Practice Track Select Panel")]
    public GameObject PracticeTrackSelectPanel;

    [Header("Split Screen Car Select Panel")]
    public GameObject SplitScreenCarSelectPanel;

    [Header("Split Screen Track Select Panel")]
    public GameObject SplitScreenTrackSelectPanel;

    public static string gm;


    /*[Header("Create Room Panel")]
    public GameObject CreateRoomUIPanel;
    public InputField RoomNameInputField;
    public string Track;
    public int Size;

    [Header("Inside Room Panel")]
    public GameObject InsideRoomUIPanel;
    public Text RoomInfoText;
    public GameObject PlayerListPrefab;
    public GameObject PlayerListContent;
    public GameObject StartGameButton;
    public Text RoomNameText;
    public Image PanelBackground;
    public Sprite T1Background, T2Background, T3Background;


    [Header("Join Random Room Panel")]
    public GameObject JoinRandomRoomUIPanel;*/

    public GameObject[] SelectableCarsPR;
    public GameObject[] SelectableCarsSS1;
    public GameObject[] SelectableCarsSS2;
    public int prCarSelectionNumber;
    public int ss1CarSelectionNumber;
    public int ss2CarSelectionNumber;
    public GameObject[] SelectableTracksPR;
    public GameObject[] SelectableTracksSS;
    public int prTrackSelectionNumber;
    public int ssTrackSelectionNumber;

    public static string p1Name, p1Tag, p2Name, p2Tag, trName, trTag;

    public object SelectableCarsP1 { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ActivatePanel(GameModePanel.name);
        prCarSelectionNumber = 0;
        ss1CarSelectionNumber = 0;
        ss2CarSelectionNumber = 0;
        prTrackSelectionNumber = 0;
        ssTrackSelectionNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPracticeButtonClicked()
    {
        ActivatePanel(PracticeCarSelectPanel.name);
    }

    public void OnSplitScreenButtonClicked()
    {
        ActivatePanel(SplitScreenCarSelectPanel.name);
    }

    public void OnMultiplayerButtonClicked()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void OnPCSBackButtonClicked()
    {
        ActivatePanel(GameModePanel.name);
    }

    public void OnPCSNextButtonClicked()
    {
        ActivatePanel(PracticeTrackSelectPanel.name);
    }

    public void OnPTSBackButtonClicked()
    {
        ActivatePanel(PracticeCarSelectPanel.name);
    }

    public void OnPTSNextButtonClicked()
    {
        SceneManager.LoadScene("PracticeMode");

        if(prCarSelectionNumber == 0)
        {
            p1Name = "Car1";
            p1Tag = "car1";
        }
        else if (prCarSelectionNumber == 1)
        {
            p1Name = "Car2";
            p1Tag = "car2";
        }
        else if (prCarSelectionNumber == 2)
        {
            p1Name = "Car3";
            p1Tag = "car3";
        }

        if (prTrackSelectionNumber == 0)
        {
            trName = "Track2";
            trTag = "track2";
        }
        else if (prTrackSelectionNumber == 1)
        {
            trName = "Track3";
            trTag = "track3";
        }
        else if (prTrackSelectionNumber == 2)
        {
            trName = "Track4";
            trTag = "track4";
        }

        gm = "pr";
    }

    public void OnSSCSBackButtonClicked()
    {
        ActivatePanel(GameModePanel.name);
    }

    public void OnSSCSNextButtonClicked()
    {
        ActivatePanel(SplitScreenTrackSelectPanel.name);
    }

    public void OnSSTSBackButtonClicked()
    {
        ActivatePanel(SplitScreenCarSelectPanel.name);
    }

    public void OnSSTSNextButtonClicked()
    {
        SceneManager.LoadScene("SplitScreenMode");

        if (ss1CarSelectionNumber == 0)
        {
            p1Name = "Car1";
            p1Tag = "car1";
        }
        else if (ss1CarSelectionNumber == 1)
        {
            p1Name = "Car2";
            p1Tag = "car2";
        }
        else if (ss1CarSelectionNumber == 2)
        {
            p1Name = "Car3";
            p1Tag = "car3";
        }

        if (ss2CarSelectionNumber == 0)
        {
            p2Name = "Car1";
            p2Tag = "car1";
        }
        else if (ss2CarSelectionNumber == 1)
        {
            p2Name = "Car2";
            p2Tag = "car2";
        }
        else if (ss2CarSelectionNumber == 2)
        {
            p2Name = "Car3";
            p2Tag = "car3";
        }

        if (ssTrackSelectionNumber == 0)
        {
            trName = "Track2";
            trTag = "track2";
        }
        else if (ssTrackSelectionNumber == 1)
        {
            trName = "Track3";
            trTag = "track3";
        }
        else if (ssTrackSelectionNumber == 2)
        {
            trName = "Track4";
            trTag = "track4";
        }

        gm = "ss";
    }

    public void NextCarPR()
    {
        prCarSelectionNumber += 1;
        if (prCarSelectionNumber >= SelectableCarsPR.Length)
        {
            prCarSelectionNumber = 0;
        }

        foreach (GameObject selectablePlayer in SelectableCarsPR)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableCarsPR[prCarSelectionNumber].SetActive(true);
    }

    public void PreviousCarPR()
    {
        prCarSelectionNumber -= 1;
        if (prCarSelectionNumber < 0)
        {
            prCarSelectionNumber = SelectableCarsPR.Length - 1;
        }

        foreach (GameObject selectablePlayer in SelectableCarsPR)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableCarsPR[prCarSelectionNumber].SetActive(true);
    }

    public void NextCarSS1()
    {
        ss1CarSelectionNumber += 1;
        if (ss1CarSelectionNumber >= SelectableCarsSS1.Length)
        {
            ss1CarSelectionNumber = 0;
        }

        foreach (GameObject selectablePlayer in SelectableCarsSS1)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableCarsSS1[ss1CarSelectionNumber].SetActive(true);
    }

    public void PreviousCarSS1()
    {
        ss1CarSelectionNumber -= 1;
        if (ss1CarSelectionNumber < 0)
        {
            ss1CarSelectionNumber = SelectableCarsSS1.Length - 1;
        }

        foreach (GameObject selectablePlayer in SelectableCarsSS1)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableCarsSS1[ss1CarSelectionNumber].SetActive(true);
    }

    public void NextCarSS2()
    {
        ss2CarSelectionNumber += 1;
        if (ss2CarSelectionNumber >= SelectableCarsSS2.Length)
        {
            ss2CarSelectionNumber = 0;
        }

        foreach (GameObject selectablePlayer in SelectableCarsSS2)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableCarsSS2[ss2CarSelectionNumber].SetActive(true);
    }

    public void PreviousCarSS2()
    {
        ss2CarSelectionNumber -= 1;
        if (ss2CarSelectionNumber < 0)
        {
            ss2CarSelectionNumber = SelectableCarsSS2.Length - 1;
        }

        foreach (GameObject selectablePlayer in SelectableCarsSS2)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableCarsSS2[ss2CarSelectionNumber].SetActive(true);
    }

    public void NextTrackPR()
    {
        prTrackSelectionNumber += 1;
        if (prTrackSelectionNumber >= SelectableTracksPR.Length)
        {
            prTrackSelectionNumber = 0;
        }

        foreach (GameObject selectablePlayer in SelectableTracksPR)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableTracksPR[prTrackSelectionNumber].SetActive(true);
    }

    public void PreviousTrackPR()
    {
        prTrackSelectionNumber -= 1;
        if (prTrackSelectionNumber < 0)
        {
            prTrackSelectionNumber = SelectableTracksPR.Length - 1;
        }

        foreach (GameObject selectablePlayer in SelectableTracksPR)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableTracksPR[prTrackSelectionNumber].SetActive(true);
    }

    public void NextTrackSS()
    {
        ssTrackSelectionNumber += 1;
        if (ssTrackSelectionNumber >= SelectableTracksSS.Length)
        {
            ssTrackSelectionNumber = 0;
        }

        foreach (GameObject selectablePlayer in SelectableTracksSS)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableTracksSS[ssTrackSelectionNumber].SetActive(true);
    }

    public void PreviousTrackSS()
    {
        ssTrackSelectionNumber -= 1;
        if (ssTrackSelectionNumber < 0)
        {
            ssTrackSelectionNumber = SelectableTracksSS.Length - 1;
        }

        foreach (GameObject selectablePlayer in SelectableTracksSS)
        {
            selectablePlayer.SetActive(false);
        }

        SelectableTracksSS[ssTrackSelectionNumber].SetActive(true);
    }

    public void ActivatePanel(string panelNameToBeActivated)
    {
        GameModePanel.SetActive(GameModePanel.name.Equals(panelNameToBeActivated));
        PracticeCarSelectPanel.SetActive(PracticeCarSelectPanel.name.Equals(panelNameToBeActivated));
        PracticeTrackSelectPanel.SetActive(PracticeTrackSelectPanel.name.Equals(panelNameToBeActivated));
        SplitScreenCarSelectPanel.SetActive(SplitScreenCarSelectPanel.name.Equals(panelNameToBeActivated));
        SplitScreenTrackSelectPanel.SetActive(SplitScreenTrackSelectPanel.name.Equals(panelNameToBeActivated));
    }
}
