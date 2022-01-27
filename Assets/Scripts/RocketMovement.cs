using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    public float rocketSpeed;
    Vector3 direction;
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = transform.parent.GetComponent<RocketLauncher>().direction;
    }
    void Update()
    {
        transform.position += direction * rocketSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
