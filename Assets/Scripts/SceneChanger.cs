using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public void changeScene (int sceneIndex) {
        SceneManager.LoadScene (sceneIndex);
    }
    //only for when in selected team view
    public void changeSceneAndClearCurrentData (int sceneIndex) {
        PlayerPrefs.SetInt ("team" + PlayerPrefs.GetInt ("currentTeam").ToString () + "number", 0);
        SceneManager.LoadScene (sceneIndex);
    }
    public void changeSceneAndClearCurrentMatchData (int sceneIndex) {
        PlayerPrefs.SetInt ("match" + PlayerPrefs.GetInt ("currentTeam").ToString () + "number1", 0);
        SceneManager.LoadScene (sceneIndex);
    } 
    public void resetSearchOnline () {
        PlayerPrefs.SetInt("onlineMatch", 0);
        PlayerPrefs.SetInt("onlineTeam", 0);
    }
}
