using UnityEngine;
using System.Collections;

public class EffectParticleDeathScript : MonoBehaviour {
	private ParticleSystem particleSys;
    // Use this for initialization
    private float buffer = 0.05f;

    private bool hasBeenSetToDestroy = false;

	void Start () {
		particleSys = this.GetComponentInChildren<ParticleSystem>();
        if(particleSys == null)
        {
            particleSys = this.GetComponent<ParticleSystem>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasBeenSetToDestroy)
        {
            hasBeenSetToDestroy = true;
            Destroy(this.gameObject, particleSys.duration + buffer);
        }
	}
}
