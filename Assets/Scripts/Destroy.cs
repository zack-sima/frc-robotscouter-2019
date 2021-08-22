using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    public bool manualDestruction; 
	public float destroyTimer;

	void Update () {
        if (!manualDestruction) {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
                Destroy (gameObject);
        }
	}
    public void destroySelf () {
        Destroy (gameObject);
    }
}
