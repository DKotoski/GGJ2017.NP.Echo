using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleTypeEnum
{
    Red,
    Yellow,
    Blue,
    Green,
    Purple,
    Orange,
    White,
    Black
}
public class ObstacleBehavior : MonoBehaviour
{

    // Use this for initialization
    public ObstacleTypeEnum ObstacleType = ObstacleTypeEnum.Black;
    public bool WillBeDestroyed = false;

    public GameObject ParticleSystem;

    public List<Color> Colors;
    void Start()
    {
        ParticleSystem = GameObject.Find("Spotlight Particle");
        var meshRenderer = GetComponent<MeshRenderer>();
        var x = meshRenderer.materials[0];//. = Materials[(int)ObstacleType];
        x.color = Colors[(int)ObstacleType];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrepareForDestruction()
    {
        if (ParticleSystem.GetComponent<ParticleBehavior>().CanDestroy(ObstacleType))
        {
            WillBeDestroyed = true;
        }
    }

    public void Destroy()
    {
        if (WillBeDestroyed) { 
            GetComponent<Rigidbody>().useGravity = true;
            GameObject.Destroy(gameObject, 5); }
    }

    /// <summary>
    /// OnParticleCollision is called when a particle hits a collider.
    /// </summary>
    /// <param name="other">The GameObject hit by the particle.</param>
    void OnParticleCollision(GameObject other)
    {
        ParticleSystem part = ParticleSystem.GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        Debug.Log(numCollisionEvents);
        Debug.Log(collisionEvents.Count);
        if (collisionEvents.Count > 50) Destroy();
    }

}
