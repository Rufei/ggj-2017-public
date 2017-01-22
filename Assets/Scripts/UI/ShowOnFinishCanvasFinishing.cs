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

    // Use this for initialization
    void Start()
    {
        jukebox = GameObject.Find("/Jukebox").GetComponent<JukeboxBehavior>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
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
                    winText.text = "Player 01 Wins!";
                }
                else if (directorScript.scorePlayer1 > directorScript.scorePlayer2)
                {
                    winText.text = "Player 02 Wins!";
                }
                else
                {
                    winText.text = "It's a tie!";
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
