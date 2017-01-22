using System.Collections;
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

    private bool isPlayer1Respawning;
    private bool isPlayer2Respawning;

    public GameObject effectPlayer1HitPrefab;
    public GameObject effectPlayer2HitPrefab;

    // Match begin timing
    // 3 seconds total to match tempo taps from the drummer in the main music
    // READY...  prompt
    // BAT-TLE!!      prompt


    // Use this for initialization
    void Start () {
        tempAudioSource = this.GetComponent<AudioSource>();

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
        tempAudioSource.Play();
        readyPromptScript.triggerPrompt();

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
            }
        }

        if (hasMatchBegun && !hasMatchEnded)
        {
            matchTimer += Time.deltaTime;
            if(matchTimer > matchThreshold)
            {
                Debug.Log("The Match has ended!");
                hasMatchEnded = true;
                finishPromptScript.triggerPrompt();
            }
        }

	}

    public void reportPlayer1Hit()
    {
        if (!isPlayer1Respawning)
        {
            isPlayer1Respawning = true;
            Instantiate(effectPlayer1HitPrefab, player1Anchor.transform.position, Quaternion.identity);
            player1Anchor.transform.position = spawnPlayer1Transform.transform.position;
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
            isPlayer1Respawning = false;
        }
    }
}
