using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPromptFadeScript : MonoBehaviour {

    private TextMesh txtMesh;
    private TextMesh txtMeshBack;

    private Color startColor;
    private Color startColorBack;

    public float fadeTimer;
    public float fadeThreshold;

    private bool isTriggered;

    public AnimationCurve alphaCurve;
    public AnimationCurve scaleCurve;

    private Vector3 startScale;

	// Use this for initialization
	void Start () {
        txtMesh = this.GetComponent<TextMesh>();
        //txtMeshBack = this.GetComponentInChildren<TextMesh>();
        txtMeshBack = this.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        fadeTimer = 0f;

        startColor = txtMesh.color;
        startColorBack = txtMeshBack.color;

        txtMesh.color = Color.clear;
        txtMeshBack.color = Color.clear;

        startScale = this.transform.localScale;

        //.Log(txtMeshBack.text);
    }
	
	// Update is called once per frame
	void Update () {
        if (isTriggered)
        {
            //Debug.Log("is triggered and changing");
            fadeTimer += Time.deltaTime;
            txtMesh.color = Color.Lerp(startColor, Color.clear, 1f - alphaCurve.Evaluate(fadeTimer / fadeThreshold));
            txtMeshBack.color = Color.Lerp(startColorBack, Color.clear,1f -  alphaCurve.Evaluate(fadeTimer / fadeThreshold));

            this.transform.localScale = startScale * scaleCurve.Evaluate(fadeTimer / fadeThreshold);

            if(fadeTimer > fadeThreshold)
            {
                isTriggered = false;
            }
        }
        else
        {
            //Debug.Log("NOT changing");
            fadeTimer = 0f;
            txtMesh.color = Color.clear;
            txtMeshBack.color = Color.clear;
        }
	}

    public void triggerPrompt()
    {
        isTriggered = true;
    }
}
