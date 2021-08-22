using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayTeamNameButton : MonoBehaviour {
    //average calculates the mean of all available data for a specific team; team search exclusive
    public bool isAverage; 
    public bool isOnline; 
    public bool isMatch;
    public int id;
    public void changeScene () {
        if (isOnline) {
            PlayerPrefs.SetInt ("onlineCurrentTeam", id);
            if (isAverage)
                PlayerPrefs.SetInt("average", 1);
            else
                PlayerPrefs.SetInt("average", 0);
            SceneManager.LoadScene (9);
        } else {
            PlayerPrefs.SetInt("average", 0);
            PlayerPrefs.SetInt ("currentTeam", id);
            if (!isMatch) {
                SceneManager.LoadScene (4);
            } else {
                SceneManager.LoadScene (7);
            }
        }
    }
    private void Start () {
    }
}
