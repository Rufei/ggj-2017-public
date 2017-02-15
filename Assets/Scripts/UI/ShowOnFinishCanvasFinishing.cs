using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowOnFinishCanvasFinishing : MonoBehaviour
{
    
    public DirectorMatchScript directorScript;
    public EffectPromptFadeScript finishScript;
    public JukeboxBehavior jukebox;
    private GameData gameData;
    public Text winText;
    private Canvas canvas;
    private bool isActivated = false;
    private bool isFinished = false;

    public AudioClip player1Clip;
    public AudioClip player2Clip;
    public AudioClip drawClip;

    private AudioSource matchOverAudioSource;

    private bool hasAnnouncerPlayed = false;

    // Use this for initialization
    void Start()
    {
        jukebox = GameObject.Find("/Jukebox").GetComponent<JukeboxBehavior>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();

        matchOverAudioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinished)
        {
            if (!isActivated)
            {
                if (finishScript.IsFinished())
                {
                    isActivated = true;
                    canvas.enabled = true;
                }
            }
            if (isActivated)
            {
                if (directorScript.scorePlayer1 > directorScript.scorePlayer2)
                {
                    winText.text = "Player 1 Wins!";
                    if (!hasAnnouncerPlayed)
                    {
                        hasAnnouncerPlayed = true;
                        matchOverAudioSource.clip = player1Clip;
                        matchOverAudioSource.Play();
                    }
                }
                else if (directorScript.scorePlayer2 > directorScript.scorePlayer1)
                {
                    winText.text = "Player 2 Wins!";
                    if (!hasAnnouncerPlayed)
                    {
                        hasAnnouncerPlayed = true;
                        matchOverAudioSource.clip = player2Clip;
                        matchOverAudioSource.Play();
                    }
                }
                else
                {
                    winText.text = "It's a tie!";
                        if (!hasAnnouncerPlayed)
                        {
                            hasAnnouncerPlayed = true;
                            matchOverAudioSource.clip = drawClip;
                            matchOverAudioSource.Play();
                        }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ResetGame();
                }
            }
        }


    }

    public void EndThis()
    {
        isFinished = true;
        canvas.enabled = false;
    }

    public void ResetGame()
    {
        EndThis();
        gameData.MakeFresh();
        SceneManager.LoadSceneAsync("CharSelectScene");
    }
}
