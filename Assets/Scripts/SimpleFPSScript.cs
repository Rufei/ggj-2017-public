using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFPSScript : MonoBehaviour {

    private TextMesh txtMesh;

	// Use this for initialization
	void Start () {
        txtMesh = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        txtMesh.text = "F: " + ((int)(1 / Time.smoothDeltaTime))/5 * 5;
	}
}
