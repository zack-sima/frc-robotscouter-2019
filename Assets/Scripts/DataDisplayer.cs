using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataDisplayer : MonoBehaviour {
    public bool onlineData, isGraph, isObjects; 
    bool uploaded = false; 
    public RectTransform canvas;
    public GameObject successButtonPrefab, graphBarPrefab, graphTextPrefab; 
    //class specifically for use on text in data display mode
    string controllerURL = "http://47.97.222.112:8081/api/";
    bool uploadConfirm = false; 

    void Start () {
        if (!onlineData) {
            Text displayText = GetComponent<Text> ();
            displayText.text = "";
            displayText.text += PlayerPrefs.GetInt ("team" + PlayerPrefs.GetInt ("currentTeam") + "number");
            displayText.text += "\n" + PlayerPrefs.GetInt ("team" + PlayerPrefs.GetInt ("currentTeam") + "matchNumber");
            displayText.text += "\n" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "name");
            if (PlayerPrefs.GetString ("team" + PlayerPrefs.GetInt ("currentTeam") + "sandstormAutonomous") == "True")
                displayText.text += "\nYes";
            else 
                displayText.text += "\nNo";

            displayText.text += "\n" + PlayerPrefs.GetInt ("team" + PlayerPrefs.GetInt ("currentTeam") + "sandstormPanel");
            displayText.text += "\n" + PlayerPrefs.GetInt ("team" + PlayerPrefs.GetInt ("currentTeam") + "sandstormCargo");
            if (PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "sandstormBonus1") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            if (PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "sandstormBonus2") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            displayText.text += "\n" + PlayerPrefs.GetInt ("team" + PlayerPrefs.GetInt ("currentTeam") + "teleOpRocketPanel1");
            displayText.text += "\n" + PlayerPrefs.GetInt ("team" + PlayerPrefs.GetInt ("currentTeam") + "teleOpRocketCargo1");
            displayText.text += "\n" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketPanel2");
            displayText.text += "\n" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketCargo2");
            displayText.text += "\n" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketPanel3");
            displayText.text += "\n" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketCargo3");
            displayText.text += "\n" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpShipPanel");
            displayText.text += "\n" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpShipCargo");

            if (PlayerPrefs.GetString ("team" + PlayerPrefs.GetInt ("currentTeam") + "climb1") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            if (PlayerPrefs.GetString ("team" + PlayerPrefs.GetInt ("currentTeam") + "climb2") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
            if (PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "climb3") == "True")
                displayText.text += "\nYes";
            else
                displayText.text += "\nNo";
        } else {
            StartCoroutine(getData());
        }
    }
    public void uploadTeamData (Text buttonText) {
        if (uploadConfirm) {
            if (!uploaded) {
                StartCoroutine(addFrcData());
                uploaded = true;
                uploadConfirm = false;
                buttonText.text = "Upload";
            }
        } else {
            uploadConfirm = true;
            buttonText.text = "Confirm?";
        }
    }
    float calculateAverage (Strings data, int index) {
        float average = 0f;
        for (int k = 0; k < data.text.Length / 20; k++) {
            average += int.Parse(data.text[k * 20 + index]);
        }
        average /= data.text.Length / 20f;

        //only two decimal points
        return (float)(int)(average * 100) / 100; 
    }
    Strings data; 

    //requires admin
    bool deleteConfirm, deleted;
    public void deleteOnlineData (Text buttonText) {
        if (PlayerPrefs.GetInt("average") != 1) {
            if (deleteConfirm) {
                if (!deleted) {
                    StartCoroutine(deleteFrcData());
                    deleted = true;
                    deleteConfirm = false;
                    buttonText.text = "[Delete Data]";
                }
            } else {
                deleteConfirm = true;
                buttonText.text = "[Confirm?]";
            }
        }
    }
    IEnumerator getData () {
        //currentMatch is set by searcher
        string urlString = ""; 
        if (PlayerPrefs.GetInt("findByTeam") == 1)
            urlString = controllerURL + "getTeamData?matchNumber=" + PlayerPrefs.GetString("onlineMatchNumber") + "&findByTeam=true";
        else
            urlString = controllerURL + "getTeamData?matchNumber=" + PlayerPrefs.GetString("onlineMatchNumber") + "&findByTeam=false";
        WWW variables = new WWW (urlString);
        yield return variables;
        print(variables.text);
        data = JsonUtility.FromJson<Strings> (variables.text);
        int team = PlayerPrefs.GetInt ("onlineCurrentTeam"); //same as index
        Text displayText = GetComponent<Text> ();

        if (!isGraph) {
            if (PlayerPrefs.GetInt("average") == 1) {
                SceneManager.LoadScene(10);
                //jump straight to next
                /*displayText.text = team.ToString();
                displayText.text += "\nX"; 
                displayText.text += "\nX";
                displayText.text += "\nX";
                displayText.text += "\n" + calculateAverage(data, 3).ToString();
                displayText.text += "\n" + calculateAverage(data, 4).ToString();
                displayText.text += "\nX";
                displayText.text += "\nX";
                displayText.text += "\n" + calculateAverage(data, 7).ToString();
                displayText.text += "\n" + calculateAverage(data, 9).ToString();
                displayText.text += "\n" + calculateAverage(data, 8).ToString();
                displayText.text += "\n" + calculateAverage(data, 10).ToString(); 
                displayText.text += "\n" + calculateAverage(data, 11).ToString();
                displayText.text += "\n" + calculateAverage(data, 12).ToString();
                displayText.text += "\nX";
                displayText.text += "\nX";
                displayText.text += "\nX";
                displayText.text += "\n" + calculateAverage(data, 16).ToString();*/
            } else {
                displayText.text = "" + data.text[team * 20 + 0];
                displayText.text += "\n" + data.text[team * 20 + 1];
                displayText.text += "\n" + data.text[team * 20 + 18];
                if (data.text[team * 20 + 2] == "true")
                    displayText.text += "\nYes";
                else
                    displayText.text += "\nNo";
                displayText.text += "\n" + data.text[team * 20 + 3];
                displayText.text += "\n" + data.text[team * 20 + 4];
                if (data.text[team * 20 + 6] == "true")
                    displayText.text += "\nYes";
                else
                    displayText.text += "\nNo";
                if (data.text[team * 20 + 5] == "true")
                    displayText.text += "\nYes";
                else
                    displayText.text += "\nNo";
                displayText.text += "\n" + data.text[team * 20 + 7];
                displayText.text += "\n" + data.text[team * 20 + 10];
                displayText.text += "\n" + data.text[team * 20 + 8];
                displayText.text += "\n" + data.text[team * 20 + 11];
                displayText.text += "\n" + data.text[team * 20 + 9];
                displayText.text += "\n" + data.text[team * 20 + 12];
                displayText.text += "\n" + data.text[team * 20 + 13];
                displayText.text += "\n" + data.text[team * 20 + 14];
                if (data.text[team * 20 + 17] == "true")
                    displayText.text += "\nYes";
                else
                    displayText.text += "\nNo";
                if (data.text[team * 20 + 16] == "true")
                    displayText.text += "\nYes";
                else
                    displayText.text += "\nNo";
                if (data.text[team * 20 + 15] == "true")
                    displayText.text += "\nYes";
                else
                    displayText.text += "\nNo";
            }
        } else if (isObjects) { 
            //graphing here
            //paneled graphing
            for (int i = 0, c = 0; i <= data.text.Length - 20; i += 20, c++) {
                int height = int.Parse(data.text[i + 3]) + int.Parse(data.text[i + 7]) + int.Parse(data.text[i + 8]) + int.Parse(data.text[i + 9]) + int.Parse(data.text[i + 13]);
                GameObject insItem = Instantiate(graphBarPrefab, transform);
                insItem.transform.position = new Vector3(Screen.width / (data.text.Length / 20) * (c + 0.5f), transform.position.y + height * 20 * Screen.height / 950f, transform.position.z);
                insItem.transform.localScale = new Vector3(Screen.width / (data.text.Length / 20) / 2f, height * 40 * Screen.height / 950f, transform.localScale.z);
                insItem.GetComponent<Image>().color = new Color(0.1f, 1f, 0.1f, 1f);
                if (height > 0) {
                    insItem = Instantiate(graphTextPrefab, transform);
                    insItem.transform.position = new Vector3(Screen.width / (data.text.Length / 20) * (c + 0.5f), transform.position.y + 30 * Screen.height / 950f, transform.position.z);
                    insItem.GetComponent<Text>().text = height.ToString();
                }
                //second br
                int height2 = int.Parse(data.text[i + 4]) + int.Parse(data.text[i + 10]) + int.Parse(data.text[i + 11]) + int.Parse(data.text[i + 12]) + int.Parse(data.text[i + 14]);
                insItem = Instantiate(graphBarPrefab, transform);
                insItem.transform.position = new Vector3(Screen.width / (data.text.Length / 20) * (c + 0.5f), transform.position.y + height * 40 * Screen.height / 950f + height2 * 20 * Screen.height / 950f, transform.position.z);
                insItem.transform.localScale = new Vector3(Screen.width / (data.text.Length / 20) / 2f, height2 * 40 * Screen.height / 950f, transform.localScale.z);
                insItem.GetComponent<Image>().color = new Color(0.8f, 0.1f, 0.2f, 1f);
                if (height2 > 0) {
                    insItem = Instantiate(graphTextPrefab, transform);
                    insItem.transform.position = new Vector3(Screen.width / (data.text.Length / 20) * (c + 0.5f), transform.position.y + 30 * Screen.height / 950f + height * 40 * Screen.height / 950f, transform.position.z);
                    insItem.GetComponent<Text>().text = height2.ToString();
                }
            }
        } else {
            for (int i = 0, c = 0; i <= data.text.Length - 20; i += 20, c++) {
                int height = (int.Parse(data.text[i + 3]) + int.Parse(data.text[i + 7]) + int.Parse(data.text[i + 8]) + int.Parse(data.text[i + 9]) + int.Parse(data.text[i + 13])) * 2 + (int.Parse(data.text[i + 4]) + int.Parse(data.text[i + 10]) + int.Parse(data.text[i + 11]) + int.Parse(data.text[i + 12]) + int.Parse(data.text[i + 14])) * 3;
                if (data.text[i + 5] == "true") {
                    height += 6; 
                }
                if (data.text[i + 6] == "true") {
                    height += 3;
                }
                if (data.text[i + 15] == "true") {
                    height += 12;
                }
                if (data.text[i + 16] == "true") {
                    height += 6;
                }
                if (data.text[i + 17] == "true") {
                    height += 3;
                }
                GameObject insItem = Instantiate(graphBarPrefab, transform);
                insItem.transform.position = new Vector3(Screen.width / (data.text.Length / 20f) * (c + 0.5f), transform.position.y + height * 10f * Screen.height / 950f, transform.position.z);
                insItem.transform.localScale = new Vector3(Screen.width / (data.text.Length / 20f) / 2f, height * 20f * Screen.height / 950f, transform.localScale.z);
                insItem.GetComponent<Image>().color = new Color(0.1f, 1f, 0.1f, 1f);
                if (height > 0) {
                    insItem = Instantiate(graphTextPrefab, transform);
                    insItem.transform.position = new Vector3(Screen.width / (data.text.Length / 20f) * (c + 0.5f), transform.position.y + 30 * Screen.height / 950f, transform.position.z);
                    insItem.GetComponent<Text>().text = height.ToString(); 
                }
            }
        }
    }
    IEnumerator addFrcData () {
        string urlString = controllerURL +
        "postTeamData?teamNumber=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "number") +
        "&matchNumber=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "matchNumber") +
        "&sandstormAutonomous=" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "sandstormAutonomous") +
        "&sandstormPanel=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "sandstormPanel") +
        "&sandstormCargo=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "sandstormCargo") +
        "&sandstormBonus2=" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "sandstormBonus2") +
        "&sandstormBonus1=" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "sandstormBonus1") +
        "&teleOpRocketPanel1=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketPanel1") +
        "&teleOpRocketPanel2=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketPanel2") +
        "&teleOpRocketPanel3=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketPanel3") +
        "&teleOpRocketCargo1=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketCargo1") +
        "&teleOpRocketCargo2=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketCargo2") +
        "&teleOpRocketCargo3=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpRocketCargo3") +
        "&teleOpShipPanel=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpShipPanel") +
        "&teleOpShipCargo=" + PlayerPrefs.GetInt("team" + PlayerPrefs.GetInt("currentTeam") + "teleOpShipCargo") +
        "&climb3=" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "climb3") +
        "&climb2=" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "climb2") +
        "&climb1=" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "climb1") +
        "&name=" + PlayerPrefs.GetString("team" + PlayerPrefs.GetInt("currentTeam") + "name");

        WWW variables = new WWW (urlString);
        yield return variables;
        GameObject insItem = Instantiate (successButtonPrefab); 
        insItem.transform.SetParent (canvas);
        insItem.transform.position = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
    }
    IEnumerator deleteFrcData() {
        int team = PlayerPrefs.GetInt("onlineCurrentTeam");
        string urlString = controllerURL +
        "deleteTeamData?teamNumber=" + data.text[team * 20 + 0] +
        "&matchNumber=" + data.text[team * 20 + 1] +
        "&sandstormAutonomous=" + data.text[team * 20 + 2] +
        "&sandstormPanel=" + data.text[team * 20 + 3] +
        "&sandstormCargo=" + data.text[team * 20 + 4] +
        "&sandstormBonus2=" + data.text[team * 20 + 5] +
        "&sandstormBonus1=" + data.text[team * 20 + 6] +
        "&teleOpRocketPanel1=" + data.text[team * 20 + 7] +
        "&teleOpRocketPanel2=" + data.text[team * 20 + 8] +
        "&teleOpRocketPanel3=" + data.text[team * 20 + 9] +
        "&teleOpRocketCargo1=" + data.text[team * 20 + 10] +
        "&teleOpRocketCargo2=" + data.text[team * 20 + 11] +
        "&teleOpRocketCargo3=" + data.text[team * 20 + 12] +
        "&teleOpShipPanel=" + data.text[team * 20 + 13] +
        "&teleOpShipCargo=" + data.text[team * 20 + 14] +
        "&climb3=" + data.text[team * 20 + 15] +
        "&climb2=" + data.text[team * 20 + 16] +
        "&climb1=" + data.text[team * 20 + 17] +
        "&name=" + data.text[team * 20 + 18] +
        "&id=" + data.text[team * 20 + 19]; 

       WWW variables = new WWW(urlString);
        yield return variables;
        print(variables.text);
        SceneManager.LoadScene(8);
    }
}
