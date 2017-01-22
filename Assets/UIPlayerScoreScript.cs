using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerScoreScript : MonoBehaviour {

    private DirectorMatchScript matchDirector;

    public int playerNum;

    private TextMesh txtMesh;

	// Use this for initialization
	void Start () {
        matchDirector = GameObject.FindObjectOfType<DirectorMatchScript>();
        txtMesh = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if(playerNum == 0)
        {
            txtMesh.text = "" + matchDirector.scorePlayer1;
        }else
        {
            txtMesh.text = "" + matchDirector.scorePlayer2;
        }
		
	}
}
