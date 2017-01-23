﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorMatchScript : MonoBehaviour {

    public int scorePlayer1;
    public int scorePlayer2;

    public float matchTimer;
    private float matchThreshold = 72.0f;

    public GameObject player1Anchor;
    public GameObject player2Anchor;

    public Transform spawnPlayer1Transform;
    public Transform spawnPlayer2Transform;

    public bool hasMatchBegun;
    public bool hasMatchEnded;

    public float matchIntroTimer;
    public float matchIntroThreshold = 3.0f;

    public EffectPromptFadeScript readyPromptScript;
    public EffectPromptFadeScript battlePromptScript;
    public EffectPromptFadeScript finishPromptScript;

    private AudioSource tempAudioSource;
    private GameData gameData;
    private JukeboxBehavior jukebox;

    private bool isPlayer1Respawning;
    private bool isPlayer2Respawning;

    public GameObject effectPlayer1HitPrefab;
    public GameObject effectPlayer2HitPrefab;

    private CricketScript crickScript;

    public GameObject effectCricketScorePrefab;

    // Match begin timing
    // 3 seconds total to match tempo taps from the drummer in the main music
    // READY...  prompt
    // BAT-TLE!!      prompt

    public AudioSource readyAudio;

    public AudioSource battleAudio;

    public AudioSource finishAudio;

    // Use this for initialization
    void Start () {
        tempAudioSource = this.GetComponent<AudioSource>();
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        jukebox = GameObject.Find("/Jukebox").GetComponent<JukeboxBehavior>();
        crickScript = GameObject.FindObjectOfType<CricketScript>();

        prepMatch();
	}

    void prepMatch()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        matchTimer = 0f;

        hasMatchBegun = false;
        hasMatchEnded = false;

        matchIntroTimer = 0f;
        player1Anchor.transform.position = spawnPlayer1Transform.position;
        player1Anchor.transform.rotation = spawnPlayer1Transform.rotation;

        player2Anchor.transform.position = spawnPlayer2Transform.position;
        player2Anchor.transform.rotation = spawnPlayer2Transform.rotation;
        jukebox.PlayGameMusic(gameData.GetPlayerData(0).characterType, gameData.GetPlayerData(1).characterType);
        //tempAudioSource.Play();
        readyPromptScript.triggerPrompt();
        readyAudio.Play();

    }

    // Update is called once per frame
    void Update () {
        if (!hasMatchBegun)
        {
            matchIntroTimer += Time.deltaTime;
            if(matchIntroTimer > matchIntroThreshold)
            {
                Debug.Log("The Match has begun!");
                hasMatchBegun = true;
                battlePromptScript.triggerPrompt();
                battleAudio.Play();
            }
        }

        if (hasMatchBegun && !hasMatchEnded)
        {
            matchTimer += Time.deltaTime;
            if(matchTimer > matchThreshold)
            {
                Debug.Log("The Match has ended!");
                hasMatchEnded = true;

                crickScript.transform.position = new Vector3(100f, 100f, 0f);
                crickScript.isPickedUpByAPlayer = true;

                finishAudio.Play();
                finishPromptScript.triggerPrompt();
            }
        }

        if (crickScript.isPickedUpByAPlayer)
        {
            if ((crickScript.transform.position - spawnPlayer1Transform.position).magnitude < 1f)
            {
                reportPlayer2Scored();
            }

            if ((crickScript.transform.position - spawnPlayer2Transform.position).magnitude < 1f)
            {
                reportPlayer1Scored();
            }
        }

        if (hasMatchEnded)
        {

            /*
            if (!crickScript.isPickedUpByAPlayer)
            {
                crickScript.transform.position = new Vector3(100f, 100f, 0f);
                crickScript.isPickedUpByAPlayer = true;
            }
            */
        }

	}

    public void reportPlayer1Hit()
    {
        if (!isPlayer1Respawning)
        {
            isPlayer1Respawning = true;
            Instantiate(effectPlayer1HitPrefab, player1Anchor.transform.position, Quaternion.identity);
            player1Anchor.transform.position = spawnPlayer1Transform.transform.position;
            player1Anchor.transform.rotation = spawnPlayer1Transform.transform.rotation;
            //respawnCricket();
            crickScript.resetCricket();
            isPlayer1Respawning = false;
        }
    }

    public void reportPlayer2Hit()
    {
        if (!isPlayer2Respawning)
        {
            isPlayer1Respawning = true;
            Instantiate(effectPlayer2HitPrefab, player2Anchor.transform.position, Quaternion.identity);
            player2Anchor.transform.position = spawnPlayer2Transform.transform.position;
            player2Anchor.transform.rotation = spawnPlayer2Transform.transform.rotation;
            //respawnCricket();
            crickScript.resetCricket();
            isPlayer1Respawning = false;
        }
    }

    void reportPlayer1Scored()
    {
        Debug.Log("Player 1 SCORED");
        Instantiate(effectCricketScorePrefab, spawnPlayer2Transform.transform.position, Quaternion.identity);
        scorePlayer1++;
        respawnCricket();
    }

    void reportPlayer2Scored()
    {
        Debug.Log("Player 2 SCORED");
        Instantiate(effectCricketScorePrefab, spawnPlayer1Transform.transform.position, Quaternion.identity);
        scorePlayer2++;
        respawnCricket();
    }

    public void respawnCricket()
    {
        crickScript.resetCricket();
        float tempRadius = Random.Range(0f, 2f);
        float tempAngle = Random.Range(0f, 2f * Mathf.PI);
        Vector3 spawnCricketPos = new Vector3(tempRadius * Mathf.Cos(tempAngle), tempRadius * Mathf.Sin(tempAngle), 0f);
        crickScript.transform.position = spawnCricketPos;

    }

}
