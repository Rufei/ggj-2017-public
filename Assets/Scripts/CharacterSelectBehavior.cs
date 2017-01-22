using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Character
{
    public enum CHARTYPE : int
    {
        CHAR_GUITAR = 0, CHAR_BASS = 1, CHAR_DRUM = 2, CHAR_VOCAL = 3, CHAR_CYMBAL = 4
    }
    public CHARTYPE type;
    public Sprite portrait;
}

public class CharacterSelectBehavior : MonoBehaviour
{
    private GameData gameData;

    void Start()
    {
        gameData = GetComponent<GameData>();
    }

    public void SetPlayerCharacter(int playerNum, int charDelta)
    {
        if (gameData.playerDataList.Count < playerNum || gameData.playerDataList[playerNum] == null)
        {
            throw new System.IndexOutOfRangeException("Attempting to set player " + playerNum + " but it was not initialized yet!");
        }

        Character.CHARTYPE curCharacter = gameData.playerDataList[playerNum].characterType;
        int numCharTypes = Enum.GetValues(typeof(Character.CHARTYPE)).Length;
        int num = (int)curCharacter + charDelta;
        while (num < 0)
        {
            num += numCharTypes;
        }
        num %= numCharTypes;
        gameData.playerDataList[playerNum].characterType = (Character.CHARTYPE)num;
    }
}
