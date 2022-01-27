using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] Rigidbody2D rocketRB;
    public Vector3 direction;
    float timer = 3;
    [SerializeField] float timeBetweenShots;
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        if(timer<=0)
        {
            Instantiate(rocketRB, transform.position, transform.rotation, transform);
            timer = timeBetweenShots;
        }
    }
}
