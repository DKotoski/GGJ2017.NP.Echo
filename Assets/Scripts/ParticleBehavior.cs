using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnParticleCollision is called when a particle hits a collider.
	/// </summary>
	/// <param name="other">The GameObject hit by the particle.</param>
	void OnParticleCollision(GameObject other)
	{
		try{
			other.GetComponent<ObstacleBehavior>().Destroy();
		}catch(Exception ex){
			
		}
	}
}
