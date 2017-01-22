using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectsPlayerKeySelect : MonoBehaviour
{
    public int playerNum;
    private GameData gameData;
    private SpriteRenderer portrait;
    private Character.CHARTYPE curCharType = 0;

    // Use this for initialization
    void Start()
    {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        portrait = transform.FindChild("Portrait").GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerNum >= 0)
        {
            if (gameData.playerDataList.Count < playerNum)
            {
                return;
            }
            PlayerData playerData = gameData.playerDataList[playerNum];
            if (curCharType != playerData.characterType)
            {
                SetPortrait(playerData.characterType);
            }
        }

    }

    void SetPortrait(Character.CHARTYPE characterType)
    {
        portrait.sprite = GetCharacterSprite(characterType);
        curCharType = characterType;
    }

    Sprite GetCharacterSprite(Character.CHARTYPE charType)
    {
        foreach (Character character in gameData.charactersAvailable)
        {
            if (charType == character.type)
            {
                return character.portrait;
            }
        }
        return null;
    }
}
