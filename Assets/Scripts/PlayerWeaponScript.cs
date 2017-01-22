using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InstrumentEnum { None, Guitar, Bass, Drum, Vocals, Cymbal};

public class PlayerWeaponScript : MonoBehaviour {

    public InstrumentEnum equippedWeapon;

    private SimplePlayerScript playerScript;

    public TestAngularRaycastScript guitarScript;
    public TestAngularRaycastScript bassScript;
    public TestAngularRaycastScript drumScript;

    // Use this for initialization
    void Start () {
        playerScript = this.GetComponent<SimplePlayerScript>();
        //equippedWeapon = InstrumentEnum.None;
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(playerScript.playerNum == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                attemptFireWeapon();
            }
        }else
        {
            if (Input.GetKeyDown(KeyCode.Period))
            {
                attemptFireWeapon();
            }
        }

	}

    void attemptFireWeapon()
    {
        //Debug.Log("Player  " + (playerScript.playerNum + 1) + " " + "attempted to fire " + equippedWeapon);
        switch (equippedWeapon)
        {
            case InstrumentEnum.Guitar:
                guitarScript.testRays();
                break;
            case InstrumentEnum.Bass:
                bassScript.testRays();
                break;
            case InstrumentEnum.Drum:
                drumScript.testRays();
                break;
            case InstrumentEnum.Vocals:
                break;
            case InstrumentEnum.Cymbal:
                break;
            default:
                break;
        }

    }
}
