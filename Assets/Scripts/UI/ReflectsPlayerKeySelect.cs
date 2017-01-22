using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectsPlayerKeySelect : MonoBehaviour
{
    public int playerNum;
    private GameData gameData;
    private JukeboxBehavior jukebox;
    private SpriteRenderer portrait;
    private MeshRenderer readyWhiteText;
    private MeshRenderer readyRedText;
    private Character.CHARTYPE curCharType = 0;

    // Use this for initialization
    void Start()
    {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        jukebox = GameObject.Find("/Jukebox").GetComponent<JukeboxBehavior>();
        portrait = transform.FindChild("Portrait").GetComponent<SpriteRenderer>();
        readyWhiteText = transform.FindChild("ReadyWhite").GetComponent<MeshRenderer>();
        readyRedText = readyWhiteText.transform.FindChild("ReadyRed").GetComponent<MeshRenderer>();
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

            CheckReady(playerData.isReady);
        }
    }

    void SetPortrait(Character.CHARTYPE characterType)
    {
        portrait.sprite = GetCharacterSprite(characterType);
        curCharType = characterType;
    }

    void CheckReady(bool isReady)
    {
        if (readyWhiteText.enabled != isReady)
        {
            jukebox.PlaySfx(isReady ? jukebox.lib.playerReady : jukebox.lib.playerUnready);
        }
        readyWhiteText.enabled = isReady;
        readyRedText.enabled = isReady;
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
