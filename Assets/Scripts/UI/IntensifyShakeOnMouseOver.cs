using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntensifyShakeOnMouseOver : MonoBehaviour {

    public float newGenRange;
    private SimpleShakeScript sss;
    private float oldGenRange;

    void Start () {
        sss = GetComponent<SimpleShakeScript>();
        oldGenRange = sss.genRange;
	}

    void OnMouseOver()
    {
        sss.genRange = newGenRange;
    }

    void OnMouseExit()
    {
        sss.genRange = oldGenRange;
    }
}
