using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminCheck : MonoBehaviour {
    string controllerURL = "http://47.97.222.112:8081/api/";
    float timer = 0;
    private void Start() {
        //testing remove all keys
        GetComponent<InputField>().text = PlayerPrefs.GetString("code");
    }
    void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
        } else {
            timer = 3f;
            if (GetComponent<InputField>().text != "") {
                StartCoroutine(checkCode());
            } else {
                PlayerPrefs.SetInt("adminAccount", 0);
            }
        }
    }
    IEnumerator checkCode() {
        string urlString = controllerURL + "getAdminCode?code=" + GetComponent<InputField>().text;
        WWW variables = new WWW(urlString);
        yield return variables;
        print(variables.text);
        if (variables.text == "true") {
            //unlocked
            PlayerPrefs.SetInt("adminAccount", 1);
            PlayerPrefs.SetString("code", GetComponent<InputField>().text);
            Destroy(gameObject);
        } else {
            PlayerPrefs.SetInt("adminAccount", 0);
        }
    }
}
