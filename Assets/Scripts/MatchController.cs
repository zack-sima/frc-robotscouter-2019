using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchController : MonoBehaviour {
    //determine whether score is counted as auto or teleop
    public bool isPage1;
    bool alreadyClearedData; 

    int switchBlocks;
    int enemySwitchBlocks;
    int scaleBlocks;
    int exchangeBlocks;

    //note that crossed line refers to all machines crossing, capturing a switch/scale and getting a ranking point; same with climbing
    public Toggle blueCrossLineToggle;
    public Toggle redCrossLineToggle;
    public Toggle blueClimbToggle;
    public Toggle redClimbToggle;

    public InputField bluePoints;
    public InputField redPoints; 

    //blue teams
    public InputField teamNumber1;
    public InputField teamNumber2;
    public InputField teamNumber3;

    //read teams
    public InputField teamNumber4;
    public InputField teamNumber5;
    public InputField teamNumber6;

    public InputField matchNumber;

    public void saveData () {
        for (int i = 1; i <= 20; i++) {
            if (PlayerPrefs.GetInt ("match" + i + "number1") == 0) {
                PlayerPrefs.SetInt ("match" + i + "number1", PlayerPrefs.GetInt ("teamNumber1"));
                PlayerPrefs.SetInt ("match" + i + "number2", PlayerPrefs.GetInt ("teamNumber2"));
                PlayerPrefs.SetInt ("match" + i + "number3", PlayerPrefs.GetInt ("teamNumber3"));
                PlayerPrefs.SetInt ("match" + i + "number4", PlayerPrefs.GetInt ("teamNumber4"));
                PlayerPrefs.SetInt ("match" + i + "number5", PlayerPrefs.GetInt ("teamNumber5"));
                PlayerPrefs.SetInt ("match" + i + "number6", PlayerPrefs.GetInt ("teamNumber6"));
                PlayerPrefs.SetInt ("match" + i + "matchNumber", PlayerPrefs.GetInt ("matchNumber"));
                PlayerPrefs.SetString ("match" + i + "blueCrossedLine", PlayerPrefs.GetString ("blueCrossedLine"));
                PlayerPrefs.SetString ("match" + i + "redCrossedLine", PlayerPrefs.GetString ("redCrossedLine"));
                PlayerPrefs.SetString ("match" + i + "blueClimbed", PlayerPrefs.GetString ("blueClimbed"));
                PlayerPrefs.SetString ("match" + i + "redClimbed", PlayerPrefs.GetString ("redClimbed"));
                PlayerPrefs.SetInt ("match" + i + "bluePoints", PlayerPrefs.GetInt ("bluePoints"));
                PlayerPrefs.SetInt ("match" + i + "redPoints", PlayerPrefs.GetInt ("redPoints"));
                break;
            }
        }
        //clear current data
        PlayerPrefs.SetInt ("teamNumber1", 0);
        PlayerPrefs.SetInt ("teamNumber2", 0);
        PlayerPrefs.SetInt ("teamNumber3", 0);
        PlayerPrefs.SetInt ("teamNumber4", 0);
        PlayerPrefs.SetInt ("teamNumber5", 0);
        PlayerPrefs.SetInt ("teamNumber6", 0);
        PlayerPrefs.SetInt ("matchNumber", 0);
        PlayerPrefs.SetString ("blueCrossedLine", "true");
        PlayerPrefs.SetString ("redCrossedLine", "true");
        PlayerPrefs.SetString ("blueClimbed", "true");
        PlayerPrefs.SetString ("redClimbed", "true");
        PlayerPrefs.SetInt ("bluePoints", 0);
        PlayerPrefs.SetInt ("redPoints", 0);
        alreadyClearedData = true; 
        SceneManager.LoadSceneAsync (0); 
    }
    void Start () {
        if (isPage1) {
            teamNumber1.text = PlayerPrefs.GetInt ("teamNumber1").ToString ();
            teamNumber2.text = PlayerPrefs.GetInt ("teamNumber2").ToString ();
            teamNumber3.text = PlayerPrefs.GetInt ("teamNumber3").ToString ();
            teamNumber4.text = PlayerPrefs.GetInt ("teamNumber4").ToString ();
            teamNumber5.text = PlayerPrefs.GetInt ("teamNumber5").ToString ();
            teamNumber6.text = PlayerPrefs.GetInt ("teamNumber6").ToString ();
            matchNumber.text = PlayerPrefs.GetInt ("matchNumber").ToString ();
        } else {
            bool placeHolder;
            if (bool.TryParse (PlayerPrefs.GetString ("redClimbed"), out placeHolder))
                redClimbToggle.isOn = bool.Parse (PlayerPrefs.GetString ("redClimbed"));
            if (bool.TryParse (PlayerPrefs.GetString ("blueClimbed"), out placeHolder))
                blueClimbToggle.isOn = bool.Parse (PlayerPrefs.GetString ("blueClimbed"));
            if (bool.TryParse (PlayerPrefs.GetString ("redCrossedLine"), out placeHolder))
                redCrossLineToggle.isOn = bool.Parse (PlayerPrefs.GetString ("redCrossedLine"));
            if (bool.TryParse (PlayerPrefs.GetString ("blueCrossedLine"), out placeHolder))
                blueCrossLineToggle.isOn = bool.Parse (PlayerPrefs.GetString ("blueCrossedLine"));
            redPoints.text = PlayerPrefs.GetInt ("redPoints").ToString ();
            bluePoints.text = PlayerPrefs.GetInt ("bluePoints").ToString ();
        }
    }
    void Update () {
        if (!alreadyClearedData) {
            if (isPage1) {
                if (teamNumber1.text != "" && teamNumber1.text != "-")
                    PlayerPrefs.SetInt ("teamNumber1", int.Parse (teamNumber1.text));
                if (teamNumber2.text != "" && teamNumber2.text != "-")
                    PlayerPrefs.SetInt ("teamNumber2", int.Parse (teamNumber2.text));
                if (teamNumber3.text != "" && teamNumber3.text != "-")
                    PlayerPrefs.SetInt ("teamNumber3", int.Parse (teamNumber3.text));
                if (teamNumber4.text != "" && teamNumber4.text != "-")
                    PlayerPrefs.SetInt ("teamNumber4", int.Parse (teamNumber4.text));
                if (teamNumber5.text != "" && teamNumber5.text != "-")
                    PlayerPrefs.SetInt ("teamNumber5", int.Parse (teamNumber5.text));
                if (teamNumber6.text != "" && teamNumber6.text != "-")
                    PlayerPrefs.SetInt ("teamNumber6", int.Parse (teamNumber6.text));
                if (matchNumber.text != "" && matchNumber.text != "-")
                    PlayerPrefs.SetInt ("matchNumber", int.Parse (matchNumber.text));
            } else {
                PlayerPrefs.SetString ("redCrossedLine", redCrossLineToggle.isOn.ToString ());
                PlayerPrefs.SetString ("blueCrossedLine", blueCrossLineToggle.isOn.ToString ());
                PlayerPrefs.SetString ("redClimbed", redClimbToggle.isOn.ToString ());
                PlayerPrefs.SetString ("blueClimbed", blueClimbToggle.isOn.ToString ());
                if (redPoints.text != "" && redPoints.text != "-")
                    PlayerPrefs.SetInt ("redPoints", int.Parse (redPoints.text));
                if (bluePoints.text != "" && bluePoints.text != "-")
                    PlayerPrefs.SetInt ("bluePoints", int.Parse (bluePoints.text));
            }
        }
    }
}
