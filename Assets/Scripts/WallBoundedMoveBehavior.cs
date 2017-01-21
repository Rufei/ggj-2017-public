using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoundedMoveBehavior : MonoBehaviour {

    public float turnSpeed;
    public float forwardSpeed;

    private Rigidbody2D myRigidBody2d;

	// Use this for initialization
	void Start () {
        myRigidBody2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.up);
        
        if (!myRigidBody2d.IsTouchingLayers(LayerMask.NameToLayer("Level Solid")))
        {
            myRigidBody2d.velocity = transform.up * Input.GetAxis("Vertical") * Time.deltaTime * forwardSpeed;
            myRigidBody2d.MoveRotation(myRigidBody2d.rotation + Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime * -1);
        }
    }
}
