using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] Rigidbody2D rocketRB;
    HackingMode hackingMode;

    public Vector3 direction;
    [SerializeField] float timer = 3;
    [SerializeField] float timeBetweenShots = 3;
    [SerializeField] float timeBetweenShotsInSlowMotion = 6;
    void Awake()
    {
        hackingMode = GameObject.FindGameObjectWithTag("Player").GetComponent<HackingMode>();
    }
    void Update()
    {
        if (hackingMode.timeSpeed == 0.5f)
            timeBetweenShots = timeBetweenShotsInSlowMotion;
        if (timer > 0 && hackingMode.timeSpeed != 0f)
            timer -= Time.deltaTime;
        if(timer<=0)
        {
            Instantiate(rocketRB, transform.position, transform.rotation, transform);
            timer = timeBetweenShots;
        }
    }
}
