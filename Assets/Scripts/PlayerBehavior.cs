using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    // Use this for initialization
    public GameObject Light;
	public GameObject Spotlight;
    public GameObject Skin;
	public GameObject ParticleSystem;
    public float Speed = 10;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        if (Input.GetAxis("Fire1") == 1)
        {
			ParticleSystem.GetComponent<ParticleSystem>().Play();
            Light.GetComponent<Animator>().SetTrigger("CastLight");
            Skin.GetComponent<Animator>().SetTrigger("CastLight");
        }
		if (Input.GetAxis("Fire2")==1){
			Spotlight.GetComponent<Animator>().SetTrigger("CastLight");
		}

    }

    void FixedUpdate()
    {
        float movex = 0f;
        float movey = 0f;
        movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().velocity = new Vector3(movex * Speed, 0, movey * Speed);
    }
}
