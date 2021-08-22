using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StatsTitleDisplayer : MonoBehaviour {
    public GameObject buttonPrefab;
    public RectTransform canvas; 

    public void moveUp () {
        transform.Translate(Vector3.up * 100f);
    }
    public void moveDown () {
        transform.Translate(Vector3.down * 100f);
    }
    void Start () {
        int j = 0; 
        for (int i = 1; i <= 200; i++) {
            if (PlayerPrefs.GetInt ("team" + i + "number") != 0) {
                j++;
                GameObject insItem = Instantiate (buttonPrefab);
                insItem.transform.position = new Vector3 (Screen.width / 2, Screen.height - 48 * GetComponent<UIAlignment> ().scale - j * 48 * GetComponent<UIAlignment> ().scale, 0);
                insItem.GetComponent<DisplayTeamNameButton> ().id = i; 
                int teamNumber = Mathf.Abs (PlayerPrefs.GetInt ("team" + i + "number"));
                string addOn = ""; 
                for (; teamNumber < 1000; addOn += "0", teamNumber *= 10) {}
                insItem.transform.GetChild (0).GetComponent<Text> ().text = addOn + PlayerPrefs.GetInt ("team" + i + "number").ToString () + " (Match " + PlayerPrefs.GetInt ("team" + i + "matchNumber").ToString () + ")";
                insItem.transform.SetParent (transform);
            }
        }
        for (int i = 1; i <= 200; i++) {
            if (PlayerPrefs.GetInt ("match" + i + "number1") != 0) {
                j++;
                GameObject insItem = Instantiate (buttonPrefab);
                insItem.transform.position = new Vector3 (Screen.width / 2, Screen.height - 48 * GetComponent<UIAlignment> ().scale - j * 48 * GetComponent<UIAlignment> ().scale, 0);
                insItem.GetComponent<DisplayTeamNameButton> ().id = i;
                insItem.GetComponent<DisplayTeamNameButton> ().isMatch = true; 

                int teamNumber1 = Mathf.Abs (PlayerPrefs.GetInt ("match" + i + "number1"));
                if (teamNumber1 == 0)
                    teamNumber1 = 1; 
                string addOn1 = "";
                for (; teamNumber1 < 1000; addOn1 += "0", teamNumber1 *= 10) { }

                int teamNumber2 = Mathf.Abs (PlayerPrefs.GetInt ("match" + i + "number2"));
                if (teamNumber2 == 0)
                    teamNumber2 = 1; 
                string addOn2 = "";
                for (; teamNumber2 < 1000; addOn2 += "0", teamNumber2 *= 10) { }

                int teamNumber3 = Mathf.Abs (PlayerPrefs.GetInt ("match" + i + "number3"));
                if (teamNumber3 == 0)
                    teamNumber3 = 1; 
                string addOn3 = "";
                for (; teamNumber3 < 1000; addOn3 += "0", teamNumber3 *= 10) { }

                int teamNumber4 = Mathf.Abs (PlayerPrefs.GetInt ("match" + i + "number4"));
                if (teamNumber4 == 0)
                    teamNumber4 = 1; 
                string addOn4 = "";
                for (; teamNumber4 < 1000; addOn4 += "0", teamNumber4 *= 10) { }

                int teamNumber5 = Mathf.Abs (PlayerPrefs.GetInt ("match" + i + "number5"));
                if (teamNumber5 == 0)
                    teamNumber5 = 1; 
                string addOn5 = "";
                for (; teamNumber5 < 1000; addOn5 += "0", teamNumber5 *= 10) { }

                int teamNumber6 = Mathf.Abs (PlayerPrefs.GetInt ("match" + i + "number6"));
                if (teamNumber6 == 0)
                    teamNumber6 = 1; 
                string addOn6 = "";
                for (; teamNumber6 < 1000; addOn6 += "0", teamNumber6 *= 10) { }

                insItem.transform.GetChild (0).GetComponent<Text> ().text = addOn1 + PlayerPrefs.GetInt ("match" + i + "number1").ToString () + " + " + addOn2 + PlayerPrefs.GetInt ("match" + i + "number2").ToString () + " + " + addOn3 + PlayerPrefs.GetInt ("match" + i + "number3").ToString () + " vs " + addOn4 + PlayerPrefs.GetInt ("match" + i + "number4").ToString () + " + " + addOn5 + PlayerPrefs.GetInt ("match" + i + "number5").ToString () + " + " + addOn6 + PlayerPrefs.GetInt ("match" + i + "number6").ToString () + " " + " (Match " + PlayerPrefs.GetInt ("team" + i + "matchNumber").ToString () + ")";
                insItem.transform.SetParent (transform);
            }
        }
    }
}
