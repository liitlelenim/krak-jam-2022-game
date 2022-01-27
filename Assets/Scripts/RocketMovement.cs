using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    #region Assignment
    EnviormentController rocketDirectionChange;
    HackingMode hackingMode;
    void Awake()
    {
        rocketDirectionChange = GameObject.Find("RocketControl").GetComponent<EnviormentController>();
        hackingMode = GameObject.FindGameObjectWithTag("Player").GetComponent<HackingMode>();
        firstDirection = transform.parent.GetComponent<RocketLauncher>().direction;
    }
    #endregion Assignment

    #region Variables
    public float rocketSpeed;
    float localTimeSpeed;
    [HideInInspector] public Vector3 firstDirection;
    [HideInInspector] public Vector3 currentDirection;

    #endregion Variables
    void Update()
    {
        if (!rocketDirectionChange.bitEquelsOne)
            currentDirection = firstDirection;
            transform.position += currentDirection * rocketSpeed * hackingMode.timeSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
