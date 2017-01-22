using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShakeScript : MonoBehaviour {

    private Vector3 startPos;
    public float genRange;

	// Use this for initialization
	void Start () {
        startPos = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = startPos + new Vector3(Random.Range(-genRange, genRange), Random.Range(-genRange, genRange), 0f);
	}
}
