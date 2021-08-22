using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminAccessButton : MonoBehaviour {
    void Start() {
        if (PlayerPrefs.GetInt("adminAccount") != 1) {
            Destroy(gameObject); 
        }
    }
}
