using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CricketScript : MonoBehaviour {

    private Rigidbody2D rigid;
    private CircleCollider2D circleCol;

    private float jumpRangeMax = 22f;
    private float jumpRangeMin = 22f;

    public float jumpTimer = 0f;
    private float jumpThreshold = 4f;

    private float forceFactor = 1f;

    public bool isPickedUpByAPlayer = false;

    private Transform target;

    public SpriteRenderer effectPickupHalo;


    // Use this for initialization
    void Start () {
        rigid = this.GetComponent<Rigidbody2D>();
        circleCol = this.GetComponent<CircleCollider2D>();

        isPickedUpByAPlayer = false;

        jumpTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isPickedUpByAPlayer)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer > jumpThreshold)
            {
                jumpTimer = 0f;
                cricketJump();
            }
            effectPickupHalo.enabled = false;
        }else
        {
            //this.transform.localPosition = Vector3.zero;
            if (target.position != null)
            {
                this.transform.position = target.position;
            }
            effectPickupHalo.enabled = true;
        }

	}

    void cricketJump()
    {
        float tempAngle = Random.Range(0f,Mathf.PI*2f);
        float tempLength = Random.Range(jumpRangeMin, jumpRangeMax);

        Vector2 unitVector = new Vector2(Mathf.Cos(tempAngle), Mathf.Sin(tempAngle));
        Vector2 tempForce = unitVector * tempLength * forceFactor;
        rigid.AddForce(rigid.mass * tempForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player1Tag" || col.gameObject.tag == "Player2Tag")
        {
            Debug.Log("Player picked up the cricket");
            
            if (!isPickedUpByAPlayer)
            {
                isPickedUpByAPlayer = true;

                //rigid.isKinematic = true;
                circleCol.enabled = false;

                //this.transform.parent = col.gameObject.transform;
                target = col.gameObject.transform;
                //this.transform.localPosition = Vector3.zero;
            }
        }
    }

    public void resetCricket()
    {
        jumpTimer = 0f;
        circleCol.enabled = true;
        target = null;
        isPickedUpByAPlayer = false;
    }
    

}
