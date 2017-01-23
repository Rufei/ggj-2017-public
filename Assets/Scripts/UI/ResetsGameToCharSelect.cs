using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetsGameToCharSelect : MonoBehaviour
{
    public ShowOnFinishCanvasFinishing canvasFinisher;
    private GameData gameData;
    private JukeboxBehavior jukebox;

    void Start()
    {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        jukebox = GameObject.Find("/Jukebox").GetComponent<JukeboxBehavior>();
    }

    void OnClick()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        canvasFinisher.EndThis();
        gameData.MakeFresh();
        SceneManager.LoadSceneAsync("CharSelectScene");
    }
}
