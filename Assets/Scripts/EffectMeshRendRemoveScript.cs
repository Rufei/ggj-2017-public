using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMeshRendRemoveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //List<MeshRenderer> tempMeshRend = this.GetComponentsInChildren<MeshRenderer>();
        MeshRenderer[] meshRendArray = this.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer meshRend in meshRendArray)
        {
            meshRend.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
