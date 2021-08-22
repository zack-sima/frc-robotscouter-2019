using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTesting : MonoBehaviour {
    string controllerURL = "http://47.97.222.112:8082/api/";
    private void Start() {
        StartCoroutine(_testCode());
    }
    void Update() {
    }
    IEnumerator _testCode() {
        string urlString = "postData?data={[\njsonhereand stuff here too\nhere too\n]}&name=Zack123"; 
        WWW variables = new WWW(controllerURL + urlString);
        yield return variables;
        print(variables.text);
    }
}
