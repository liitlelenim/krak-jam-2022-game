using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentController : MonoBehaviour
{
    #region Assignment
    Transform bluePlatform;
    EnviormentController secondBit;
    void Awake()
    {
        bluePlatform = GameObject.Find("BluePlatform").transform;
        secondBit = GameObject.Find("BlueRotationSecond").GetComponent<EnviormentController>();
    }
    #endregion Assignment

    #region Variables
    [Header("Bits")]
    [SerializeField] bool platformsRotation;
    [SerializeField] bool platformsPosition;
    [SerializeField] bool enemiesC;
    [SerializeField] bool bullets;
    [SerializeField] bool time;
    [SerializeField] bool gravityControl;
    [Space]
    public bool bitEquelsOne;

    #endregion Variables
    void Update()
    {
        EnviormentControl();
    }
    void UpdateAssignment()
    {
        
    }
void EnviormentControl()
    {
        #region PlatformRotation
        if (platformsRotation)
        {
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
