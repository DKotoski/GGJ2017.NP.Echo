using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class PlayerBehavior : MonoBehaviour
{

    // Use this for initialization
    public GameObject MenuUI;
    public GameObject InstructionsUI;
    public GameObject IntroAnimation;
    public GameObject Light;
    public GameObject Spotlight;
    public GameObject Skin;
    public GameObject ParticleSystem;
    public GameObject Spotparticle;
    public float Speed = 10;
    private float ParticleTimer = 0;
    private float SpotParticleTimer = 0;
    public List<ObstacleTypeEnum> Powerups = new List<ObstacleTypeEnum>();

    //UI
    public GameObject RedUI;
    public GameObject YellowUI;
    public GameObject BlueUI;
    //end UI
    private bool CanPlay;
    void Start()
    {
        CanPlay = false;
        Skin.GetComponent<Animator>().SetTrigger("IddleFreq");
        ResetCollectablesUI();
    }
    // Update is called once per frame

    void Update()
    {
        Timers();
        if (!CanPlay)
        {
            return;
        }
        

        if (Input.GetAxis("Fire1") == 1)
        {
            ParticleSystem.GetComponent<ParticleSystem>().Play();

            Light.GetComponent<Animator>().SetTrigger("CastLight");
            Skin.GetComponent<Animator>().SetTrigger("CastLight");
            Skin.GetComponent<Animator>().SetTrigger("LowFreq");
            Skin.GetComponent<Animator>().SetTrigger("LowFreq");
        }
        if (Input.GetAxis("Fire2") == 1)
        {
            Spotlight.GetComponent<Animator>().SetTrigger("CastLight");
            Spotparticle.GetComponent<ParticleSystem>().Play();
            Skin.GetComponent<Animator>().SetTrigger("HighFreq");
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit, 8f))
            {
                try
                {
                    var particleType = Spotparticle.GetComponent<ParticleBehavior>();
                    var obstacleType = hit.transform.gameObject.GetComponent<ObstacleBehavior>().ObstacleType;
                    if (CanDestroy(obstacleType))
                    {
                        Spotparticle.GetComponent<ParticleBehavior>().Destroys = hit.transform.gameObject.GetComponent<ObstacleBehavior>().ObstacleType;
                        ResetCollectablesUI();
                        hit.transform.gameObject.GetComponent<ObstacleBehavior>().PrepareForDestruction();
                    }
                    //Debug.Log(hit.transform.gameObject);
                }
                catch (Exception ex)
                {

                    Skin.GetComponent<Animator>().SetTrigger("HighFreq");


                }

            }
        }

    }

    void FixedUpdate()
    {
        if (!CanPlay)
        {
            if(!IntroAnimation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Intro") && animationPlaying){
                IntroAnimation.SetActive(false);
                
            }
            return;
        }
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.x, offset.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        float movex = 0f;
        float movey = 0f;
        movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");
        //Debug.Log(movex+" "+movey);
        GetComponent<Rigidbody>().velocity = new Vector3(movex * Speed, 0, movey * Speed);
    }
    bool animationPlaying = false;
    double AnimatorTimer = 0;
    void Timers()
    {
        if(AnimatorTimer>1){
            animationPlaying = true;
        }
        if(!animationPlaying){
            AnimatorTimer += Time.deltaTime;
        }
        if (ParticleSystem.GetComponent<ParticleSystem>().isPlaying)
        {
            ParticleTimer += Time.deltaTime;
        }
        if (ParticleTimer / 3 > 1)
        {
            ParticleSystem.GetComponent<ParticleSystem>().Stop();
            ParticleTimer = 0;
        }
        if (Spotparticle.GetComponent<ParticleSystem>().isPlaying)
        {
            SpotParticleTimer += Time.deltaTime;
        }
        if (SpotParticleTimer / 3 > 1)
        {
            Spotparticle.GetComponent<ParticleSystem>().Stop();
            SpotParticleTimer = 0;
        }
    }

    bool CanDestroy(ObstacleTypeEnum type)
    {

        if (Powerups.Contains(type))
        {
            Powerups.Remove(type);
            return true;
        }

        switch (type)
        {
            case ObstacleTypeEnum.Green:
                if (Powerups.Contains(ObstacleTypeEnum.Blue) && Powerups.Contains(ObstacleTypeEnum.Yellow))
                {
                    Powerups.Remove(ObstacleTypeEnum.Blue);
                    Powerups.Remove(ObstacleTypeEnum.Yellow);
                    return true;
                }
                break;
            case ObstacleTypeEnum.Purple:
                if (Powerups.Contains(ObstacleTypeEnum.Blue) && Powerups.Contains(ObstacleTypeEnum.Red))
                {
                    Powerups.Remove(ObstacleTypeEnum.Blue);
                    Powerups.Remove(ObstacleTypeEnum.Red);
                    return true;
                }
                break;
            case ObstacleTypeEnum.Orange:
                if (Powerups.Contains(ObstacleTypeEnum.Yellow) && Powerups.Contains(ObstacleTypeEnum.Red))
                {
                    Powerups.Remove(ObstacleTypeEnum.Yellow);
                    Powerups.Remove(ObstacleTypeEnum.Red);
                    return true;
                }
                break;
            case ObstacleTypeEnum.White:
                if (Powerups.Contains(ObstacleTypeEnum.Yellow) && Powerups.Contains(ObstacleTypeEnum.Red) && Powerups.Contains(ObstacleTypeEnum.Blue))
                {
                    Powerups.Remove(ObstacleTypeEnum.Yellow);
                    Powerups.Remove(ObstacleTypeEnum.Red);
                    Powerups.Remove(ObstacleTypeEnum.Blue);
                    return true;
                }
                break;
            default: return false;
        }
        return false;
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            var collectableType = other.gameObject.GetComponentInChildren<CollectableSkinController>().collectableType;
            Powerups.Add(collectableType);
            ResetCollectablesUI();
            GameObject.Destroy(other.gameObject);
        }
    }

    void ResetCollectablesUI()
    {

        var redText = RedUI.GetComponent<Text>();
        var yellowText = YellowUI.GetComponent<Text>();
        var blueText = BlueUI.GetComponent<Text>();

        var countedPowerups = Powerups.GroupBy(x => x);
        Debug.Log(countedPowerups.Where(x => x.Key == ObstacleTypeEnum.Red).Count());
        Debug.Log(countedPowerups.Where(x => x.Key == ObstacleTypeEnum.Yellow).Count());
        Debug.Log(countedPowerups.Where(x => x.Key == ObstacleTypeEnum.Blue).Count());

        if (countedPowerups.Where(x => x.Key == ObstacleTypeEnum.Red).Count() == 0)
        {
            RedUI.SetActive(false);
        }
        else
        {
            redText.text = (countedPowerups.FirstOrDefault(x => x.Key == ObstacleTypeEnum.Red).Count() + "x");

            RedUI.SetActive(true);
        }
        if (countedPowerups.Where(x => x.Key == ObstacleTypeEnum.Blue).Count() == 0)
        {

            BlueUI.SetActive(false);
        }
        else
        {
            blueText.text = (countedPowerups.FirstOrDefault(x => x.Key == ObstacleTypeEnum.Blue).Count() + "x");

            BlueUI.SetActive(true);
        }
        if (countedPowerups.Where(x => x.Key == ObstacleTypeEnum.Yellow).Count() == 0)
        {
            YellowUI.SetActive(false);
        }
        else
        {
            yellowText.text = (countedPowerups.FirstOrDefault(x => x.Key == ObstacleTypeEnum.Yellow).Count() + "x");

            YellowUI.SetActive(true);
        }
    }

    public void Play()
    {
        CanPlay = true;
        MenuUI.SetActive(false);
    }

    public void Instructions()
    {
        InstructionsUI.SetActive(true);
        MenuUI.SetActive(false);
    }

    public void CancelInstructions()
    {
        InstructionsUI.SetActive(false);
        MenuUI.SetActive(true);
    }
}

