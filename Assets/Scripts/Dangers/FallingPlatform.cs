using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    EnviormentController enviormentController;
    float speed = 8;
    bool isFalling;
    bool destroying;
    float timer = 2f;
    void Awake()
    {
        enviormentController = GameObject.Find("BitsMenu").GetComponent<EnviormentController>();
    }
    private void Update()
    {
        if (!isFalling)
        {
            speed += Random.Range(0f, 2f);
            isFalling = true;
        }
        if (destroying)
            timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
        transform.position += Vector3.down * speed * enviormentController.timeSpeed * Time.deltaTime;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player") || !collision.collider.CompareTag("Cartridge"))
            destroying = true;
    }
}
