using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class Strings {
    public string[] text;
}
/*[System.Serializable]
public class DoubleStrings {
    public string[][] text;
}*/
/*[System.Serializable]
public class FrcDataSet {
    public int[] teamNumber;
    public int[] matchNumber;
    public string[] crossedLine;
    public int[] autoSwitch;
    public int[] autoScale;
    public int[] autoExchange;
    public int[] teleOpSwitch;
    public int[] teleOpEnemySwitch;
    public int[] teleOpScale;
    public int[] teleOpExchange;
    public string[] climbing;
    public string[] carryClimbing;
} */

public class OnlineStatsTitleDisplayer : MonoBehaviour {
    string controllerURL = "http://47.97.222.112:8081/api/";
    public GameObject buttonPrefab;
    public RectTransform canvas;
    public InputField searchInput1;
    public InputField searchInput2; 
    public GameObject searchButton1;
    public GameObject searchButton2;

    void Start () {
        if (PlayerPrefs.GetInt("onlineMatch") != 0) {
            searchInput1.text = PlayerPrefs.GetInt("onlineMatch").ToString();
            search(true);
        }
        if (PlayerPrefs.GetInt("onlineTeam") != 0) {
            searchInput2.text = PlayerPrefs.GetInt("onlineTeam").ToString(); 
            search(false);
        }
    }
    void Update () {
        
    }
    public void search (bool searchByMatch) {
        if (searchByMatch) {
            PlayerPrefs.SetInt("findByTeam", 0);
            PlayerPrefs.SetString("onlineMatchNumber", searchInput1.text);
            if (searchInput1.text != "" && searchInput1.text != "-") {
                StartCoroutine (getData(true));
                PlayerPrefs.SetInt("onlineMatch", int.Parse(searchInput1.text));
            }
        } else {
            PlayerPrefs.SetInt("findByTeam", 1);
            PlayerPrefs.SetString("onlineMatchNumber", searchInput2.text);
            if (searchInput2.text != "" && searchInput2.text != "-") {
                StartCoroutine (getData(false));
                PlayerPrefs.SetInt("onlineTeam", int.Parse(searchInput2.text));
            }
        }
    }
    IEnumerator getData (bool searchByMatch) {
        if (searchInput1.text == "0" || searchInput2.text == "0")
            yield break; 
        //currentMatch is set by searcher
        string urlString = ""; 
        if (searchByMatch) {
            urlString = controllerURL + "getTeamData?matchNumber=" + searchInput1.text + "&findByTeam=false";
        } else
            urlString = controllerURL + "getTeamData?matchNumber=" + searchInput2.text + "&findByTeam=true";
        WWW variables = new WWW (urlString);
        yield return variables;
        print(variables.text);
        Strings data = JsonUtility.FromJson<Strings> (variables.text);
        if (data.text.Length > 0) {
            if (!searchByMatch) {
                GameObject insItem = Instantiate(buttonPrefab);
                insItem.transform.position = new Vector3(Screen.width / 2, Screen.height - 48 * GetComponent<UIAlignment>().scale - (0 + 1.5f) * 48 * GetComponent<UIAlignment>().scale, 0);
                insItem.GetComponent<DisplayTeamNameButton>().id = 0;
                insItem.GetComponent<DisplayTeamNameButton>().isOnline = true;
                insItem.GetComponent<DisplayTeamNameButton>().isAverage = true;
                int teamNumber = int.Parse(data.text[0]);
                string addOn = "";
                //add zeroes
                for (; teamNumber < 1000; addOn += "0", teamNumber *= 10) { }
                insItem.transform.GetChild(0).GetComponent<Text>().text = addOn + data.text[0] + " (Graphed)";
                insItem.transform.SetParent(canvas);
            }
            for (int i = 0; i < data.text.Length / 20; i++) {
                GameObject insItem = Instantiate (buttonPrefab);
                float down = 2.5f;
                if (searchByMatch)
                    down = 1.5f; 
                insItem.transform.position = new Vector3 (Screen.width / 2, Screen.height - 48 * GetComponent<UIAlignment> ().scale - (i + down) * 48 * GetComponent<UIAlignment> ().scale, 0);
                insItem.GetComponent<DisplayTeamNameButton> ().id = i;
                insItem.GetComponent<DisplayTeamNameButton> ().isOnline = true; 
                int teamNumber = int.Parse(data.text[i * 20]); 
                string addOn = "";
                //add zeroes
                for (; teamNumber < 1000; addOn += "0", teamNumber *= 10) { }
                insItem.transform.GetChild (0).GetComponent<Text> ().text = addOn + data.text[i * 20] + " (Match " + data.text[i * 20 + 1] + ")";
                insItem.transform.SetParent (canvas);
            }
        }
        Destroy (searchInput1.gameObject);
        Destroy (searchButton1);
        Destroy (searchInput2.gameObject);
        Destroy (searchButton2);
    }
}
