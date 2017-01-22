using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSkinController : MonoBehaviour
{

    public CollectableColor animationColor;
    public ObstacleTypeEnum collectableType
    {
        get
        {
            switch (animationColor)
            {
                case CollectableColor.Red: return ObstacleTypeEnum.Red;
                case CollectableColor.Blue: return ObstacleTypeEnum.Blue;
                case CollectableColor.Yellow: return ObstacleTypeEnum.Yellow;
                default: return ObstacleTypeEnum.Black;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        if (animationColor == CollectableColor.Blue)
        {
            GetComponent<Animator>().SetTrigger("BlueAnimation");
        }

        else if (animationColor == CollectableColor.Yellow)
        {
            GetComponent<Animator>().SetTrigger("YellowAnimation");
        }

        else
        {
            GetComponent<Animator>().SetTrigger("RedAnimation");
        }

    }

}

public enum CollectableColor
{
    Red = 1,
    Blue = 2,
    Yellow = 3
}
