using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPortraitScript : MonoBehaviour {

    public SimplePlayerScript playerScript;
    public WallBoundedMoveBehavior playerMoveScript;

    public AnimationCurve animCurveMovement;
    public float movementFactor;

    private float movementTimer;
    private float movementThreshold = 1f;

    public AnimationCurve animCurveShoot;

    private Vector3 startPos;

	// Use this for initialization
	void Start () {

        startPos = this.transform.localPosition;

	}
	
	// Update is called once per frame
	void Update () {
        movementTimer += Time.deltaTime;
        if (playerMoveScript.isPlayerMoving)
        {
            this.transform.localPosition = startPos + new Vector3(0, animCurveMovement.Evaluate((movementTimer % movementThreshold) / movementThreshold), 0);
        }
        else
        {
            movementTimer = 0f;
            this.transform.localPosition = startPos;
        }
	}
}
