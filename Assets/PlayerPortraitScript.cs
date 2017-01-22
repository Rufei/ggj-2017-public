using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPortraitScript : MonoBehaviour {

    private GameData gameData;

    public int playerNum;

    private Character.CHARTYPE playerType;

    private Image attachedImage;

    public Sprite guitarSprite;
    public Sprite drumSprite;
    public Sprite cymbalsSprite;
    public Sprite drumsSprite;
    public Sprite vocalSprite;
    public Sprite bassSprite;

    // Use this for initialization
    void Start () {
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        playerType = gameData.GetPlayerData(playerNum).characterType;
        attachedImage = this.GetComponent<Image>();
        changePortrait();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void changePortrait()
    {
        switch (playerType)
        {
            case Character.CHARTYPE.CHAR_BASS:
                attachedImage.sprite = bassSprite;
                break;
            case Character.CHARTYPE.CHAR_CYMBAL:
                attachedImage.sprite = cymbalsSprite;
                break;
            case Character.CHARTYPE.CHAR_DRUM:
                attachedImage.sprite = drumSprite;
                break;
            case Character.CHARTYPE.CHAR_GUITAR:
                attachedImage.sprite = guitarSprite;
                break;
            case Character.CHARTYPE.CHAR_VOCAL:
                attachedImage.sprite = vocalSprite;
                break;
            default:
                break;
        }
    }
}
