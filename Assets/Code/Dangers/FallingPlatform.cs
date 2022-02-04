using Hacking;
using UnityEngine;

namespace Dangers
{
    public class FallingPlatform : MonoBehaviour
    {
        EnvironmentController _environmentController;
        float speed = 8;
        bool isFalling;
        bool destroying;
        float timer = 2f;
        void Awake()
        {
            _environmentController = GameObject.Find("BitsMenu").GetComponent<EnvironmentController>();
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
            transform.position += Vector3.down * speed * _environmentController.TimeSpeed * Time.deltaTime;

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Player") || !collision.collider.CompareTag("Cartridge"))
                destroying = true;
        }
    }
}
