using Hacking;
using UnityEngine;

namespace Dangers
{
    public class FallingPlatform : MonoBehaviour
    {
        EnvironmentController _environmentController;
        private float _speed = 8;
        private bool _isFalling;
        private bool _destroying;
        private float _timer = 2f;
        private void Awake()
        {
            _environmentController = GameObject.Find("BitsMenu").GetComponent<EnvironmentController>();
        }
        private void Update()
        {
            if (!_isFalling)
            {
                _speed += Random.Range(0f, 2f);
                _isFalling = true;
            }

            if (_destroying)
            {
                _timer -= Time.deltaTime;
            }

            if (_timer <= 0)
            {
                Destroy(gameObject);
            }

            transform.position += Vector3.down * (_speed * _environmentController.TimeSpeed * Time.deltaTime);

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.collider.CompareTag("Player") || !collision.collider.CompareTag("Cartridge"))
            {
                _destroying = true;
            }        
        }
    }
}
