using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionsOnAllPlayersReady : MonoBehaviour
{
    public string defaultSceneToLoad;
    private GameData gameData;
    
    void Start ()
    {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
    }
	
	void Update () {
        bool isEveryoneReady = true;
        foreach (PlayerData player in gameData.playerDataList)
        {
            isEveryoneReady &= player.isReady;
        }
        if (isEveryoneReady)
        {
            OnClickLoadsSceneByName.LoadScene(defaultSceneToLoad);
        }
    }
}
