using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchDataDisplayer : MonoBehaviour {
    //class specifically for use on text in data display mode
    public bool isRedTeamNumberDisplay;
    public bool isBlueTeamNumberDisplay;
    void Start () {
        Text displayText = GetComponent<Text> ();
        if (isBlueTeamNumberDisplay) {
            int teamNumber1 = Mathf.Abs (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number1"));
            if (teamNumber1 == 0)
                teamNumber1 = 1; 
            string addOn1 = "";
            for (; teamNumber1 < 1000; addOn1 += "0", teamNumber1 *= 10) { }

            int teamNumber2 = Mathf.Abs (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number2"));
            if (teamNumber2 == 0)
                teamNumber2 = 1; 
            string addOn2 = "";
            for (; teamNumber2 < 1000; addOn2 += "0", teamNumber2 *= 10) { }

            int teamNumber3 = Mathf.Abs (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number3"));
            if (teamNumber3 == 0)
                teamNumber3 = 1; 
            string addOn3 = "";
            for (; teamNumber3 < 1000; addOn3 += "0", teamNumber3 *= 10) { }

            displayText.text = "" + addOn1 + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number1");
            displayText.text += "\n" + addOn2 + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number2") + "  (" + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "bluePoints") + ")";
            displayText.text += "\n" + addOn3 + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number3");
        } else if (isRedTeamNumberDisplay) {
            int teamNumber1 = Mathf.Abs (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number4"));
            if (teamNumber1 == 0)
                teamNumber1 = 1; 
            string addOn1 = "";
            for (; teamNumber1 < 1000; addOn1 += "0", teamNumber1 *= 10) { }

            int teamNumber2 = Mathf.Abs (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number5"));
            if (teamNumber2 == 0)
                teamNumber2 = 1; 
            string addOn2 = "";
            for (; teamNumber2 < 1000; addOn2 += "0", teamNumber2 *= 10) { }

            int teamNumber3 = Mathf.Abs (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number6"));
            if (teamNumber3 == 0)
                teamNumber3 = 1; 
            string addOn3 = "";
            for (; teamNumber3 < 1000; addOn3 += "0", teamNumber3 *= 10) { }

            displayText.text = "" + addOn1 + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number4");
            displayText.text += "\n" + "(" + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "redPoints") + ")  " + addOn2 + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number5");
            displayText.text += "\n" + addOn3 + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "number6");
        } else {
            displayText.text = "" + PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "matchNumber");
            if (PlayerPrefs.GetString ("match" + PlayerPrefs.GetInt ("currentTeam") + "blueCrossedLine") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            if (PlayerPrefs.GetString ("match" + PlayerPrefs.GetInt ("currentTeam") + "blueClimbed") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            if (PlayerPrefs.GetString ("match" + PlayerPrefs.GetInt ("currentTeam") + "redCrossedLine") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            if (PlayerPrefs.GetString ("match" + PlayerPrefs.GetInt ("currentTeam") + "redClimbed") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            if (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "bluePoints") > PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "redPoints")) {
                displayText.text += "\nBlue";
            } else if (PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "bluePoints") < PlayerPrefs.GetInt ("match" + PlayerPrefs.GetInt ("currentTeam") + "redPoints"))
                displayText.text += "\nRed";
            else {
                displayText.text += "\nTie";
            }
        }
    }
}