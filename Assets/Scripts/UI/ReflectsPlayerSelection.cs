using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReflectsPlayerSelection : MonoBehaviour
{
    private GameData gameData;
    private CharacterSelectBehavior charSelect;
    private Text playerTitle;
    private Image portrait;
    private int playerNum = -1;
    private Character.CHARTYPE curCharType = 0;

    // Use this for initialization
    void Start()
    {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        charSelect = gameData.GetComponent<CharacterSelectBehavior>();
        portrait = transform.FindChild("Portrait").GetComponent<Image>();
        playerTitle = portrait.transform.FindChild("PlayerTitle").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNum >= 0)
        {
            SetPlayerTitle(playerNum);
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

    void SetPortrait(Character.CHARTYPE characterType)
    {
        portrait.sprite = GetCharacterSprite(characterType);
        curCharType = characterType;
    }

    void SetPlayerTitle(int num)
    {
        playerTitle.text = "PLAYER 0" + (num + 1);
    }

    public void SetPlayerNumber(int num)
    {
        playerNum = num;
    }

    public void SpinCharacterSelectLeft()
    {
        charSelect.SetPlayerCharacter(playerNum, -1);
    }

    public void SpinCharacterSelectRight()
    {
        charSelect.SetPlayerCharacter(playerNum, 1);
    }
}
