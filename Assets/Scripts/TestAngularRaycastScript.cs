﻿using UnityEngine;
using System.Collections;

public class TestAngularRaycastScript : MonoBehaviour {

    private float speedOfSound = 24f;

    private float angularRangeDegrees = 30f;

    public GameObject hitEffect;

    private int bucketsNum = 100;

    private float angleStep;

    private float turnSpeed = 40f;

	// Use this for initialization
	void Start () {
        angleStep = angularRangeDegrees / bucketsNum;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            testRays();
            //testRays();
        }

	}

    void testRays()
    {

        //RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.up, 100f);

        for (int i = -(bucketsNum / 2); i <= (bucketsNum / 2); i++)
        {

            Vector3 startPos = this.transform.position + (this.transform.up * 0.5f);
            Vector3 startDir = (this.transform.up + this.transform.right * 0.0f).normalized;

            Vector2 tempDir = new Vector2(startDir.x, startDir.y);

            tempDir = customRotate(tempDir, i * angleStep);

            float tempRange = 12f;
            RaycastHit2D rayHit = Physics2D.Raycast(startPos, tempDir, tempRange);

            //Debug.DrawRay(startPos, startDir * tempDistance, Color.magenta, 0.1f);

            float hitDistance = 0f;

            if (rayHit.collider != null)
            {

                //Debug.Log("Object Hit");

                //float distance = Mathf.Abs(rayHit.point.y - transform.position.y);
                float distance = (rayHit.point - new Vector2(this.transform.position.x,this.transform.position.y)).magnitude;

                Vector2 hitPoint = rayHit.point;
                float tempAngle = Mathf.Atan2(rayHit.normal.y, rayHit.normal.x);
                //Debug.Log(tempAngle);


                //float tempLaserAngle = Mathf.Atan2(-startDir.y, -startDir.x);

                //tempAngle = Mathf.LerpAngle(tempAngle, tempLaserAngle, 0.5f);
                tempAngle = (tempAngle * Mathf.Rad2Deg) + 90f;

                Quaternion normalQuat = Quaternion.Euler(0, 0, tempAngle);

                //Mathf.Atan2(missileHit.normal.y, missileHit.normal.x);
                //Instantiate(laserHitEffect, new Vector3(hitPoint.x, hitPoint.y, 0), normalQuat);
                //laserHitEffect.transform.position = new Vector3(hitPoint.x, hitPoint.y, 0);
                //laserHitEffect.transform.rotation = normalQuat;

                GameObject tempEffect = Instantiate(hitEffect, new Vector3(rayHit.point.x, rayHit.point.y, 0f), normalQuat);
                tempEffect.SetActive(false);
                ParticleSystem tempPart = tempEffect.GetComponentInChildren<ParticleSystem>();
                tempPart.startDelay = distance / speedOfSound;
                tempEffect.SetActive(true);

            }
        }
    }

    private Vector2 customRotate(Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }
}
