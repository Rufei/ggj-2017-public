using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour {

    private SimplePlayerScript playerScript;
    private GameData gameData;

    [SerializeField]
    private PlayerData playerData;
    private JukeboxBehavior jukebox;

    private int lastHandledBeat = -1;

    public TestAngularRaycastScript guitarScript;
    public TestAngularRaycastScript bassScript;
    public TestAngularRaycastScript drumScript;

    // Use this for initialization
    void Start () {
        playerScript = this.GetComponent<SimplePlayerScript>();
        gameData = GameObject.Find("/GameData").GetComponent<GameData>();
        playerData = gameData.GetPlayerData(playerScript.playerNum);
        jukebox = GameObject.Find("/Jukebox").GetComponent<JukeboxBehavior>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckPullTrigger();
        attemptFireWeapon();
	}

    void CheckPullTrigger()
    {
        if (Input.GetKeyDown(playerScript.playerNum == 0 ? KeyCode.Space : KeyCode.Period))
        {
            //Debug.Log("Player  " + (playerScript.playerNum + 1) + " " + "attempted to fire " + playerData.characterType);
            jukebox.RequestAttack(playerData);
        }
    }

    void attemptFireWeapon()
    {
        JukeboxBehavior.Beat beat = jukebox.GetBeat();
        if (beat != null && lastHandledBeat != beat.beatInSong 
            && (playerScript.playerNum == 0 ? beat.isPlayer1Firing : beat.isPlayer2Firing))
        {
            lastHandledBeat = beat.beatInSong;
            //Debug.Log("Player  " + (playerScript.playerNum + 1) + " " + "FIRING at " + Time.deltaTime);
            switch (playerData.characterType)
            {
                case Character.CHARTYPE.CHAR_GUITAR:
                    guitarScript.testRays();
                    break;
                case Character.CHARTYPE.CHAR_BASS:
                    bassScript.testRays();
                    break;
                case Character.CHARTYPE.CHAR_DRUM:
                    drumScript.testRays();
                    break;
                case Character.CHARTYPE.CHAR_VOCAL:
                    break;
                case Character.CHARTYPE.CHAR_CYMBAL:
                    break;
                default:
                    break;
            }
        }
    }
}
