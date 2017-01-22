using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowOnFinishCanvasFinishing : MonoBehaviour {

    public DirectorMatchScript directorScript;
    public EffectPromptFadeScript finishScript;
    public Text winText;
    private Canvas canvas;
    private bool isActivated = false;

    // Use this for initialization
    void Start () {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isActivated)
        {
            if (finishScript.IsFinished())
            {
                isActivated = true;
                canvas.enabled = true;
            }
        }
        if (isActivated)
        {

        }
    }
}
