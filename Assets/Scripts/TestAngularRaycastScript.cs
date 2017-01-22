using UnityEngine;
using System.Collections;

public class TestAngularRaycastScript : MonoBehaviour {

    private float speedOfSound = 12f;

    public float angularRangeDegrees = 140;

    public GameObject hitEffect;

    public int bucketsNum = 160;

    private float angleStep;

    private float turnSpeed = 40f;

    public float pulseRange = 3.5f;

    private DirectorMatchScript matchDirector;

    /*
    RANGES:
    AOE = 2
    Short = 3.5f
    Medium = 7
    Long = 11f

    WIDTHS:
    Thin = 30
    Medium = 60
    Wide = 160
    */
	// Use this for initialization
	void Start () {
        matchDirector = GameObject.FindObjectOfType<DirectorMatchScript>();

        angleStep = angularRangeDegrees / bucketsNum;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.I))
        {
            testRays();
            //testRays();
        }

	}

    public void testRays()
    {
        //Debug.Log(" Fired rays ");
        //RaycastHit2D rayHit = Physics2D.Raycast(transform.position, Vector2.up, 100f);

        for (int i = -(bucketsNum / 2); i <= (bucketsNum / 2); i++)
        {
            float tempOffset = 0.3f;

            Vector3 startDir = (this.transform.up + this.transform.right * 0.0f).normalized;
            Vector2 tempDir = new Vector2(startDir.x, startDir.y);
            tempDir = customRotate(tempDir, i * angleStep);
            //Vector3 startPos = this.transform.position + (this.transform.up * tempOffset);
            Vector3 startPos = this.transform.position + (new Vector3(tempDir.x,tempDir.y,0f) * tempOffset);

            //float tempRange = 12f;
            float tempRange = pulseRange - tempOffset;

            RaycastHit2D rayHit = Physics2D.Raycast(startPos, tempDir, tempRange);

            //Debug.DrawRay(startPos, startDir * tempDistance, Color.magenta, 0.1f);

            //float hitDistance = 0f;

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
                float tempDelay = (distance / speedOfSound) - 0.15f;
                tempDelay = Mathf.Max(tempDelay, 0f);

                tempPart.startDelay = tempDelay;
                //tempPart.main.startDelay = tempDelay;
                tempEffect.SetActive(true);

                if(rayHit.collider.tag == "Player1Tag")
                {
                    //Debug.Log("Player 1 is hit");
                    matchDirector.reportPlayer1Hit();
                }

                if (rayHit.collider.tag == "Player2Tag")
                {
                    //Debug.Log("Player 2 is hit");
                    matchDirector.reportPlayer2Hit();
                }


            }
        }
    }

    private Vector2 customRotate(Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }
}
