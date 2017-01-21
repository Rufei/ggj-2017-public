using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatesWithPlayer : MonoBehaviour {

    public Transform player;
    public float offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = player.rotation;
        transform.Rotate(Vector3.forward, offset);
    }
}
