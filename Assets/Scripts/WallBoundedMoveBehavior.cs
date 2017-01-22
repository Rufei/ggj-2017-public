using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoundedMoveBehavior : MonoBehaviour {

    public float turnSpeed;
    public float forwardSpeed;

    private Rigidbody2D myRigidBody2d;

    private SimplePlayerScript playerScript;
    private DirectorMatchScript matchDirector;

    public bool isPlayerMoving;

	// Use this for initialization
	void Start () {
        myRigidBody2d = GetComponent<Rigidbody2D>();
        playerScript = this.GetComponent<SimplePlayerScript>();
        matchDirector = GameObject.FindObjectOfType<DirectorMatchScript>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.up);

        if(myRigidBody2d.velocity.sqrMagnitude > 2f)
        {
            isPlayerMoving = true;
        }else
        {
            isPlayerMoving = false;
        }

        if (matchDirector.hasMatchBegun)
        {
            if (!myRigidBody2d.IsTouchingLayers(LayerMask.NameToLayer("Level Solid")))
            {
                if (playerScript.playerNum == 0)
                {
                    myRigidBody2d.velocity = transform.up * Input.GetAxis("Vertical") * Time.deltaTime * forwardSpeed;
                    myRigidBody2d.MoveRotation(myRigidBody2d.rotation + Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime * -1);
                }
                else
                {
                    myRigidBody2d.velocity = transform.up * Input.GetAxis("VerticalAlt") * Time.deltaTime * forwardSpeed;
                    myRigidBody2d.MoveRotation(myRigidBody2d.rotation + Input.GetAxis("HorizontalAlt") * turnSpeed * Time.deltaTime * -1);
                }
            }
        }
    }
}
