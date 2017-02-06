using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMeshRendRemoveScript : MonoBehaviour {

    private DirectorMatchScript matchDirector;

    private Color startColor;

    private MeshRenderer[] mshRendArray;

    private bool hasMatchStarted = false;

    private float glowAmount = 1.0f;
    private float glowFadeRate = 1.0f;
    private float glowAmountSatPoint = 1.0f;

    private float glowDelayTimer = 0f;
    private bool hasGlowed = false;
    
	// Use this for initialization
	void Start () {
        matchDirector = GameObject.FindObjectOfType<DirectorMatchScript>();
        startColor = this.GetComponentInChildren<MeshRenderer>().material.color;

        //List<MeshRenderer> tempMeshRend = this.GetComponentsInChildren<MeshRenderer>();
        /*
       
        MeshRenderer[] meshRendArray = this.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer meshRend in meshRendArray)
        {
            meshRend.enabled = false;
        }
        
        */
        mshRendArray = this.GetComponentsInChildren<MeshRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        hasMatchStarted = (matchDirector.matchIntroTimer > matchDirector.matchIntroThreshold);
		if(!hasMatchStarted)
        {  
            foreach (MeshRenderer meshRend in mshRendArray)
            {
                meshRend.material.color = Color.Lerp(startColor, Color.clear, ((4f*matchDirector.matchIntroTimer) / matchDirector.matchIntroThreshold));
            }
        }else
        {
            if(glowDelayTimer < 0f && !hasGlowed)
            {
                hasGlowed = true;
                glowAmount = 1.0f;
            }else
            {
                glowDelayTimer -= Time.deltaTime;
            }

            glowAmount -= Time.deltaTime * glowFadeRate;
            glowAmount = Mathf.Max(0f, glowAmount);
            glowAmount = Mathf.Min(glowAmount, glowAmountSatPoint);

            foreach (MeshRenderer meshRend in mshRendArray)
            {
                meshRend.material.color = Color.Lerp(Color.clear,startColor,glowAmount*0.20f);
            }
        }


        /*
        if(matchDirector.matchIntroTimer > matchDirector.matchIntroThreshold && !hasMatchStarted)
        {
            hasMatchStarted = true;
            foreach (MeshRenderer meshRend in mshRendArray)
            {
                meshRend.enabled = false;
            }
            
        }
        */
	}

    public void messageGlowAmount(float glowChange)
    {
        glowAmount = glowChange + glowAmount;
        glowAmount = Mathf.Min(glowAmount, glowAmountSatPoint);
        //Debug.Log("received Message " + glowChange + " and Glow is now " + glowAmount);
    }

    public void messageGlowDelay(float time)
    {
        glowDelayTimer = time;
        hasGlowed = false;
    }
}
