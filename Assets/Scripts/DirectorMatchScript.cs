using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorMatchScript : MonoBehaviour {

    public int scorePlayer1;
    public int scorePlayer2;

    public float matchTimer;
    private float matchThreshold = 90f;

    public GameObject player1Anchor;
    public GameObject player2Anchor;

    public GameObject spawnPlayer1;
    public GameObject spawnPlayer2;

    public bool hasMatchBegun;
    public bool hasMatchEnded;

    public float matchBeginTimer;
    public float matchBeginThreshold = 3.1f;

    // Use this for initialization
	void Start () {
        prepMatch();
	}

    void prepMatch()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        matchTimer = 0f;

        hasMatchBegun = false;
        hasMatchEnded = false;

        matchBeginTimer = 0f;

    }

    // Update is called once per frame
    void Update () {
        if (!hasMatchBegun)
        {
            matchBeginTimer += Time.deltaTime;
            if(matchBeginTimer > matchBeginThreshold)
            {
                hasMatchBegun = true;
            }
        }

        if (hasMatchBegun && !hasMatchEnded)
        {
            matchTimer += Time.deltaTime;
        }

	}

    public void player1Hit()
    {

    }

    public void player2hit()
    {

    }
}
