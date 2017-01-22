using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	public GameObject Target;

	private Transform target;
	// Use this for initialization
	void Start () {
		target = Target.GetComponent<Transform>();
		 transform.position = new Vector3(target.position.x,transform.position.y,transform.position.z);
		 GetComponent<AudioSource>().volume = 0.25f;
        // transform.rotation = target.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x,transform.position.y,target.position.z), Time.deltaTime * Mathf.Clamp((target.position - transform.position).sqrMagnitude * 6, 30, 5));
        //transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * Mathf.Clamp(Vector3.Angle(target.forward, transform.forward)/90 * 4, 0, 5));
	}
}
