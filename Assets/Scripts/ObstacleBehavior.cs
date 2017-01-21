using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour {

	// Use this for initialization
	public bool WillBeDestroyed = false;

	public GameObject ParticleSystem;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PrepareForDestruction(){
		WillBeDestroyed = true;
	}

	public void Destroy(){
		if(WillBeDestroyed) GameObject.Destroy(this);
	}

	/// <summary>
	/// OnParticleCollision is called when a particle hits a collider.
	/// </summary>
	/// <param name="other">The GameObject hit by the particle.</param>
	void OnParticleCollision(GameObject other)
	{
		Debug.Log(other);
	}

}
