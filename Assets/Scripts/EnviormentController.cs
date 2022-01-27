using UnityEngine;

public class EnviormentController : MonoBehaviour
{
    #region Assignment
    EnviormentController secondBit;
    EnviormentController thirdBit;
    EnviormentController fourthBit;
    HackingMode hackingMode;
    void Awake()
    {
        hackingMode = GameObject.FindGameObjectWithTag("Player").GetComponent<HackingMode>();
    }
    #endregion Assignment

    #region Variables
    [Header("Platforms")][Header("Bits")]
    [SerializeField] bool platformsRotation;
    [SerializeField] bool platformsPosition;
    [Header("Enemies")]
    [SerializeField] bool enemiesC;
    [Header("Weapons")]
    [SerializeField] bool rocketDiractionChangeActive;
    [Header("Time")]
    [SerializeField] bool timeFreeze; 
    [SerializeField] bool slowMotion;
    [Header("Gravity")]
    [SerializeField] bool gravityControl;
    [Space]
    public bool bitEquelsOne;
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
            Vector3 nextDirection;
            GameObject[] rocketsInScene = GameObject.FindGameObjectsWithTag("Rocket");
            secondBit = GameObject.Find("DirectionBit").GetComponent<EnviormentController>();
            thirdBit = GameObject.Find("DirectionSecondBit").GetComponent<EnviormentController>();
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
        #region Platform
        if (platformsRotation)
        {
            float speed = 5f;
            secondBit = GameObject.Find("BlueRotationSecond").GetComponent<EnviormentController>();
            Transform bluePlatform = GameObject.Find("BluePlatform").transform;

            if (bitEquelsOne && secondBit.bitEquelsOne)
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 180f), speed);
            }
            else if (bitEquelsOne && !secondBit.bitEquelsOne)
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, -90f), speed);
            }
            else if (!bitEquelsOne && secondBit.bitEquelsOne)
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 90f), speed);
            }
            else
            {
                bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), speed);
            }

            thirdBit = GameObject.Find("GreenRotationFirst").GetComponent<EnviormentController>();
            fourthBit = GameObject.Find("GreenRotationSecond").GetComponent<EnviormentController>();
            Transform greenPlatform = GameObject.Find("GreenPlatform").transform;
            if (thirdBit.bitEquelsOne && fourthBit.bitEquelsOne)
            {
                greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 180f), speed);   
            }
            else if (thirdBit.bitEquelsOne && !fourthBit.bitEquelsOne)
            {
                greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, -90f), speed);
            }
            else if (!thirdBit.bitEquelsOne && fourthBit.bitEquelsOne)
            {
                greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 90f), speed);
            }
            else
            {
                greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), speed);
            }
        }

        if (platformsPosition)
        {
            GameObject[] yellowPos_1 = GameObject.FindGameObjectsWithTag("YellowPos1");
            GameObject[] yellowPos_2 = GameObject.FindGameObjectsWithTag("YellowPos2");
            GameObject[] yellowPlaftorm = GameObject.FindGameObjectsWithTag("YellowPlatform");

            for (int i = 0; i < yellowPlaftorm.Length; i++) 
            {
                if (bitEquelsOne)
                    yellowPlaftorm[i].transform.position = yellowPos_2[i].transform.position;
                else
                    yellowPlaftorm[i].transform.position = yellowPos_1[i].transform.position;
            }

            secondBit = GameObject.Find("BrownChangePosition").GetComponent<EnviormentController>();
            GameObject[] brownPos_1 = GameObject.FindGameObjectsWithTag("BrownPos1");
            GameObject[] brownPos_2 = GameObject.FindGameObjectsWithTag("BrownPos2");
            GameObject[] brownPlaftorm = GameObject.FindGameObjectsWithTag("BrownPlatform");

            for (int i = 0; i < yellowPlaftorm.Length; i++)
            {
                if (secondBit.bitEquelsOne)
                    brownPlaftorm[i].transform.position = brownPos_2[i].transform.position;
                else
                    brownPlaftorm[i].transform.position = brownPos_1[i].transform.position;
            }
        }
        #endregion Platform
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
