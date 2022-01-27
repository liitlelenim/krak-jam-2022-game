using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentController : MonoBehaviour
{
    #region Assignment
    Transform bluePlatform;
    EnviormentController secondBit;
    EnviormentController thirdBit;
    HackingMode hackingMode;
    void Awake()
    {
        bluePlatform = GameObject.Find("BluePlatform").transform;
        hackingMode = GameObject.FindGameObjectWithTag("Player").GetComponent<HackingMode>();
    }
    #endregion Assignment

    #region Variables
    [Header("Bits")]
    [SerializeField] bool platformsRotation;
    [SerializeField] bool platformsPosition;
    [SerializeField] bool enemiesC;
    [SerializeField] bool rocketDiractionChangeActive;
    [SerializeField] bool timeFreeze; 
    [SerializeField] bool slowMotion;
    [SerializeField] bool gravityControl;
    [Space]
    public bool bitEquelsOne;
    [SerializeField] private GameObject[] rocketsInScene;
    Vector3 nextDirection;
    #endregion Variables
    void Update()
    {
        EnviormentControl();
    }
void EnviormentControl()
    {
        #region Time
        if (timeFreeze)
        {
            if (bitEquelsOne)
                hackingMode.timeSpeed = 0f;
            else
                hackingMode.timeSpeed = 1f;
        }
        else if (slowMotion && hackingMode.timeSpeed != 0)
        {
            if (bitEquelsOne)
                hackingMode.timeSpeed = 0.5f;
            else
                hackingMode.timeSpeed = 1f;
        }
        #endregion Time
        #region RocketDirection
        if (rocketDiractionChangeActive && bitEquelsOne)
        {
            secondBit = GameObject.Find("DirectionBit").GetComponent<EnviormentController>();
            thirdBit = GameObject.Find("DirectionSecondBit").GetComponent<EnviormentController>();
            rocketsInScene = GameObject.FindGameObjectsWithTag("Rocket");
            //up
            if (thirdBit.bitEquelsOne && secondBit.bitEquelsOne)
                nextDirection = new Vector3(0f, 1f, 0f);
            //left
            else if (thirdBit.bitEquelsOne && !secondBit.bitEquelsOne)
                nextDirection = new Vector3(-1f, 0f, 0f);
            //right
            else if (!thirdBit.bitEquelsOne && secondBit.bitEquelsOne)
                nextDirection = new Vector3(1f, 0f, 0f);
            //down
            else
                nextDirection = new Vector3(0f, -1f, 0f);

            for (int i = 0; i <rocketsInScene.Length; i++)
                rocketsInScene[i].GetComponent<RocketMovement>().currentDirection = nextDirection;
        }
        #endregion RocketDirection
        #region PlatformRotation
        if (platformsRotation)
        {
            secondBit = GameObject.Find("BlueRotationSecond").GetComponent<EnviormentController>();
            if (bitEquelsOne && secondBit.bitEquelsOne)
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 180f), 5f);
            }
            else if (bitEquelsOne && !secondBit.bitEquelsOne)
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, -90f), 5f);
            }
            else if (!bitEquelsOne && secondBit.bitEquelsOne)
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 90f), 5f);
            }
            else
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 5f);
            }
        }
        #endregion PlatformRotation
        #region Gravity
        if (gravityControl)
        {
            if (bitEquelsOne)
                Physics2D.gravity = new Vector2(0, 9.81f);
            else
                Physics2D.gravity = new Vector2(0, -9.81f);
        }
        #endregion Gravity
    }
}
