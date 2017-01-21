using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour {
	public List<Color> Colors;
	public ObstacleTypeEnum Destroys = ObstacleTypeEnum.Black;
	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().startColor = Colors[(int)Destroys];
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<ParticleSystem>().startColor = Colors[(int)Destroys];
		// Debug.Log("color: "+GetComponent<ParticleSystem>().startColor);
		// Debug.Log("destroys: "+Destroys);
	}

    internal bool CanDestroy(ObstacleTypeEnum type)
    {
        if(Destroys == type){
			return true;
		}
		return false;
    }
}
