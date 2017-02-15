using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EffectTextFollowScript : MonoBehaviour {

    private TextMesh txtMesh;
    private TextMesh parentTxtMesh;

    private float previousHashCode;

	// Use this for initialization
	void Start () {
        txtMesh = this.GetComponent<TextMesh>();
        parentTxtMesh = this.transform.parent.GetComponent<TextMesh>();
        txtMesh.text.GetHashCode();
        txtMesh.characterSize = parentTxtMesh.characterSize;
        txtMesh.fontSize = parentTxtMesh.fontSize;
        txtMesh.anchor = parentTxtMesh.anchor;
        txtMesh.alignment = parentTxtMesh.alignment;

        float tempCharFactor = txtMesh.fontSize;
        this.transform.localPosition = new Vector3(0.000875f * tempCharFactor, -0.0005f * tempCharFactor, 0.0001f);
	}
	
	// Update is called once per frame
	void Update () {
		if(parentTxtMesh.text.GetHashCode() != previousHashCode)
        {
            updateLabelText();
        }
	}

    void updateLabelText()
    {
        txtMesh.text = parentTxtMesh.text;
        previousHashCode = txtMesh.text.GetHashCode();
    }
}
