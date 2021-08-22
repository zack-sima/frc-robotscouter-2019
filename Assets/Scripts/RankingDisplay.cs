using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingDisplay : MonoBehaviour {
    string controllerURL = "http://47.97.222.112:8081/api/";
    public GameObject graphBarPrefab, graphTextPrefab;
    [Range(1, 4)]
    public int mode; 

    // Use this for initialization 
    void Start() {
        StartCoroutine(getData());
    }

    // Update is called once per frame
    void Update() {

    }
    public void moveUp() {
        transform.Translate(Vector3.up * 100f);
    }
    public void moveDown() {
        transform.Translate(Vector3.down * 100f);
    }
    IEnumerator getData() {
        string urlString = "";
        urlString = controllerURL + "getAll";

        WWW variables = new WWW(urlString);
        yield return variables;
        print(variables.text);
        print(urlString);
        Strings data = JsonUtility.FromJson<Strings>(variables.text);
        Text displayText = GetComponent<Text>();

        string[] teams = new string[data.text.Length / 20];
        float[] teamScores = new float[data.text.Length / 20];
        int[] teamRepeats = new int[data.text.Length / 20];
        string[] orderTeams = new string[teams.Length];
        float[] orderTeamScores = new float[teamScores.Length];

        for (int i = 0, c = 0; i <= data.text.Length - 20; i += 20, c++) {
            int height = 0;
            if (mode == 1) {
                height = (int.Parse(data.text[i + 3]) + int.Parse(data.text[i + 7]) + int.Parse(data.text[i + 8]) + int.Parse(data.text[i + 9]) + int.Parse(data.text[i + 13])) * 2 + (int.Parse(data.text[i + 4]) + int.Parse(data.text[i + 10]) + int.Parse(data.text[i + 11]) + int.Parse(data.text[i + 12]) + int.Parse(data.text[i + 14])) * 3;
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
            } else if (mode == 2) {
                height = int.Parse(data.text[i + 3]) + int.Parse(data.text[i + 7]) + int.Parse(data.text[i + 8]) + int.Parse(data.text[i + 9]) + int.Parse(data.text[i + 13]); 
            } else if (mode == 3) {
                height = int.Parse(data.text[i + 4]) + int.Parse(data.text[i + 10]) + int.Parse(data.text[i + 11]) + int.Parse(data.text[i + 12]) + int.Parse(data.text[i + 14]);
            } else if (true) {
                if (data.text[i + 15] == "true") {
                    height += 12;
                }
                if (data.text[i + 16] == "true") {
                    height += 6;
                }
                if (data.text[i + 17] == "true") {
                    height += 3;
                }
            }
            for (int j = 0; j < teams.Length; j++) {

                if (teams[j] == "" || teams[j] == null) {
                    teamRepeats[j]++; 
                    teams[j] = data.text[i];
                    teamScores[j] += height;
                    break;
                }
                if (teams[j] == data.text[i]) {
                    teamRepeats[j]++; 
                    teamScores[j] += height;
                    break;
                }
            }
        }
        for (int d = 0; d < teams.Length; d++) {
            teamScores[d] /= teamRepeats[d];
        }
        float currentHightest = 0;
        string currentTeam = "";
        float currentRecordedHighest = 1000;
        int currentIndex = 0; 
        for (int k = 0; k < teamScores.Length; k++) {
            currentHightest = 0; 
            for (int d = 0; d < teams.Length; d++) {
                if (teams[d] != "") {
                    if (teamScores[d] > currentHightest && teamScores[d] <= currentRecordedHighest) {
                        currentHightest = teamScores[d];
                        currentTeam = teams[d];
                        currentIndex = d; 
                        //no break; 
                    }
                }
            }
            for (int d = 0; d < teams.Length; d++) {
                if (orderTeams[d] == "" || orderTeams[d] == null) {
                    orderTeams[d] = currentTeam;
                    orderTeamScores[d] = currentHightest;
                    currentRecordedHighest = currentHightest;
                    teamScores[currentIndex] = 0;
                    teams[currentIndex] = " ";
                    break;
                }
            }
        }
        float scaleFactor = 5f;
        if (mode != 1)
            scaleFactor = 12f; 
        for (int i = 0; i < orderTeamScores.Length; i++) {
            if (orderTeams[i] != "" && orderTeamScores[i] > 0) {
                GameObject insItem = Instantiate(graphBarPrefab, transform);
                insItem.transform.position = new Vector3(transform.position.x + (float)orderTeamScores[i] * scaleFactor * Screen.width / 950f, Screen.height - Screen.height / (15) * (i + 1.5f), transform.position.z);
                insItem.transform.localScale = new Vector3(orderTeamScores[i] * scaleFactor * 2 * Screen.width / 950f, Screen.height / (15) / 2f, transform.localScale.z);
                insItem.GetComponent<Image>().color = new Color(0.1f, 1f, 0.1f, 1f);
                insItem = Instantiate(graphTextPrefab, transform);
                insItem.transform.position = new Vector3(transform.position.x + 250 * Screen.width / 950f, Screen.height - Screen.height / (15) * (i + 1.5f), transform.position.z);
                float s = (int)(orderTeamScores[i] * 10) / 10f;
                insItem.GetComponent<Text>().text = s.ToString() + " (" + orderTeams[i] + ")";
                insItem.GetComponent<Text>().alignment = TextAnchor.MiddleLeft; 
            }
        }
        foreach (string i in teams)
            print(i);
        foreach (string i in orderTeams)
            print(i);
    }
}



