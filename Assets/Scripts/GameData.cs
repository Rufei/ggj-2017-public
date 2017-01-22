﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int playerNum;
    public Character.CHARTYPE characterType;

    public PlayerData(int playerNum)
    {
        this.playerNum = playerNum;
        characterType = 0;
    }

    public static string GetAxis(int playerNum, string baseAxis)
    {
        return playerNum == 0 ? baseAxis : baseAxis + "Alt";
    }
}

public class GameData : MonoBehaviour
{
    public static class CONST
    {
        public const int NUM_PLAYERS_STANDARD_MATCH = 2;
        public const string HORIZONTAL_AXIS_PLAYER_1 = "Horizontal";
        public const string HORIZONTAL_AXIS_PLAYER_2 = "HorizontalAlt";
        public const string VERTICAL_AXIS_PLAYER_1 = "Vertical";
        public const string VERTICAL_AXIS_PLAYER_2 = "VerticalAlt";
    }

    public List<Character> charactersAvailable;
    public List<PlayerData> playerDataList;
    public bool isGameStarted;

    private int numPlayers = CONST.NUM_PLAYERS_STANDARD_MATCH;

    void Start()
    {
        MakeFresh();
    }

    // Call this to start
    void MakeFresh()
    {
        playerDataList = new List<PlayerData>();
        isGameStarted = false;
        InitPlayers(numPlayers);
    }

    void InitPlayers(int numPlayers)
    {
        for (int i = 0; i < numPlayers; i++)
        {
            AddPlayer(i);
        }
    }

    void AddPlayer(int playerNum)
    {
        playerDataList.Insert(playerNum, new PlayerData(playerNum));
    }
}
