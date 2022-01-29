using UnityEngine;

public class EnviormentController : MonoBehaviour
{
    #region Assignment
    [SerializeField] Bit rotationBitValue1;
    [SerializeField] Bit rotationBitValue2;
    [SerializeField] Bit rotationBitValue3;
    [SerializeField] Bit rotationBitValue4;
    [SerializeField] Bit switchingBitValue1;
    [SerializeField] Bit switchingBitValue2;
    //[SerializeField] Bit enemiesC;
    [SerializeField] Bit rocketControlBitValue;
    [SerializeField] Bit rocketBitValue1;
    [SerializeField] Bit rocketBitValue2;
    [SerializeField] Bit timerFreezeBitValue;
    [SerializeField] Bit slowMotionBitValue;
    [SerializeField] Bit gravityControlBitValue;

    Transform bluePlatform;
    Transform greenPlatform;
    GameObject[] yellowPos_1;
    GameObject[] yellowPos_2;
    GameObject[] yellowPlaftorm;
    GameObject[] brownPos_1;
    GameObject[] brownPos_2;
    GameObject[] brownPlaftorm;
    HackingMode hackingMode;
    void Awake()
    {
        hackingMode = GameObject.FindGameObjectWithTag("Player").GetComponent<HackingMode>();

        yellowPos_1 = GameObject.FindGameObjectsWithTag("YellowPos1");
        yellowPos_2 = GameObject.FindGameObjectsWithTag("YellowPos2");
        yellowPlaftorm = GameObject.FindGameObjectsWithTag("YellowPlatform");

        brownPos_1 = GameObject.FindGameObjectsWithTag("BrownPos1");
        brownPos_2 = GameObject.FindGameObjectsWithTag("BrownPos2");
        brownPlaftorm = GameObject.FindGameObjectsWithTag("BrownPlatform");

        bluePlatform = GameObject.Find("BluePlatform").transform;
        greenPlatform = GameObject.Find("GreenPlatform").transform;
    }
    #endregion Assignment

    #region Variables
    [Header("Platforms")]
    [Header("Bits")]
    [SerializeField] bool rotationBit1;
    [SerializeField] bool rotationBit2;
    [SerializeField] bool rotationBit3;
    [SerializeField] bool rotationBit4;
    [Space]
    [SerializeField] bool switchingBit1;
    [SerializeField] bool switchingBit2;
    [Header("Enemies")]
    //[SerializeField] bool enemiesC;
    [Header("Weapons")]
    public bool rocketControlBit;
    [SerializeField] bool rocketBit1;
    [SerializeField] bool rocketBit2;
    [Header("Time")]
    [SerializeField] bool timerFreezeBit;
    [SerializeField] bool slowMotionBit;
    [Header("Gravity")]
    [SerializeField] bool gravityControlBit;

    #endregion Variables

    #region Hacking Execution
    void Update()
    {
        EnviormentControl();
        UpdateBitValue();
    }
    void EnviormentControl()
    {


        #region Platform Rotation
        float speed = 5f;


        if (rotationBit1 && rotationBit2)
        {
            bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 180f), speed);
        }
        else if (rotationBit1 && !rotationBit2)
        {
            bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, -90f), speed);
        }
        else if (!rotationBit1 && rotationBit2)
        {
            bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 90f), speed);
        }
        else
        {
            bluePlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), speed);
        }

        if (rotationBit3 && rotationBit4)
        {
            greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 180f), speed);
        }
        else if (rotationBit3 && !rotationBit4)
        {
            greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, -90f), speed);
        }
        else if (!rotationBit3 && rotationBit4)
        {
            greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 90f), speed);
        }
        else
        {
            greenPlatform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), speed);
        }

        #endregion Platform Rotation

        #region Switch Platform Position

        if (switchingBit1)
        {
            for (int i = 0; i < yellowPlaftorm.Length; i++)
            {
                yellowPlaftorm[i].transform.position = yellowPos_2[i].transform.position;
            }
        }
        else
            for (int i = 0; i < yellowPlaftorm.Length; i++)
            {
                yellowPlaftorm[i].transform.position = yellowPos_1[i].transform.position;
            }

        if (switchingBit2)
        {
            for (int i = 0; i < yellowPlaftorm.Length; i++)
            {
                brownPlaftorm[i].transform.position = brownPos_2[i].transform.position;
            }
        }
        else
            for (int i = 0; i < yellowPlaftorm.Length; i++)
            {
                brownPlaftorm[i].transform.position = brownPos_1[i].transform.position;
            }

        #endregion Platform

        #region RocketDirection
        if (rocketControlBit)
        {
            Vector3 nextDirection;
            GameObject[] rocketsInScene = GameObject.FindGameObjectsWithTag("Rocket");
            //up
            if (rocketBit1 && rocketBit2)
                nextDirection = new Vector3(0f, 1f, 0f);
            //left
            else if (rocketBit1 && !rocketBit2)
                nextDirection = new Vector3(-1f, 0f, 0f);
            //right
            else if (!rocketBit1 && rocketBit2)
                nextDirection = new Vector3(1f, 0f, 0f);
            //down
            else
                nextDirection = new Vector3(0f, -1f, 0f);

            for (int i = 0; i < rocketsInScene.Length; i++)
                rocketsInScene[i].GetComponent<RocketMovement>().currentDirection = nextDirection;
        }
        #endregion RocketDirection

        #region Time
        if (timerFreezeBit)
            hackingMode.timeSpeed = 0f;
        if (slowMotionBit)
            hackingMode.timeSpeed = 0.5f;
        else
            hackingMode.timeSpeed = 1f;
        #endregion Time

        #region Gravity
        if (gravityControlBit)
            Physics2D.gravity = new Vector2(0, 9.81f);
        else
            Physics2D.gravity = new Vector2(0, -9.81f);

        #endregion Gravity
    }
    void UpdateBitValue()
    {
        rotationBit1 = rotationBitValue1.valueIsOne;
        rotationBit2 = rotationBitValue2.valueIsOne;
        rotationBit3 = rotationBitValue3.valueIsOne;
        rotationBit4 = rotationBitValue4.valueIsOne;
        switchingBit1 = switchingBitValue1.valueIsOne;
        switchingBit2 = switchingBitValue2.valueIsOne;
        rocketControlBit = rocketControlBitValue.valueIsOne;
        rocketBit1 = rocketBitValue1.valueIsOne;
        rocketBit2 = rocketBitValue2.valueIsOne;
        timerFreezeBit = timerFreezeBitValue.valueIsOne;
        slowMotionBit = slowMotionBitValue.valueIsOne;
        gravityControlBit = gravityControlBitValue.valueIsOne;
    }
    #endregion Hacking Execution
}
