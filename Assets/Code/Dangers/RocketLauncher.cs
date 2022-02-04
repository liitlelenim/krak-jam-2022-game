using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] Rigidbody2D rocketRB;
    EnviormentController
 enviormentController;
    public Vector3 direction;
    [SerializeField] float timer = 3;
    [SerializeField] float timeBetweenShots = 3;
    [SerializeField] float timeBetweenShotsInSlowMotion = 6;
    void Awake()
    {
        enviormentController = GameObject.Find("BitsMenu").GetComponent<EnviormentController>();
    }
    void Update()
    {
        if (enviormentController.timeSpeed == 0.5f)
            timeBetweenShots = timeBetweenShotsInSlowMotion;
        if (timer > 0 && enviormentController.timeSpeed != 0f)
            timer -= Time.deltaTime;
        if(timer<=0)
        {
            Instantiate(rocketRB, transform.position, transform.rotation, transform);
            timer = timeBetweenShots;
        }
    }
}
