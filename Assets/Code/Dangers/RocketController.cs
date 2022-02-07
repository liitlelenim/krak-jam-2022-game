using Hacking;
using UnityEngine;

namespace Dangers
{
    public class RocketController : MonoBehaviour
    {
        EnvironmentController _environmentController;

        void Awake()
        {
            _environmentController = GameObject.Find("BitsMenu").GetComponent<EnvironmentController>();
            firstDirection = transform.parent.GetComponent<RocketLauncher>().direction;
        }

        public float rocketSpeed;
        [HideInInspector] public Vector3 firstDirection;
        [HideInInspector] public Vector3 currentDirection;
        
        void Update()
        {
            if (!_environmentController.rocketControlBit)
            {
                currentDirection = firstDirection;
            }
            transform.position += currentDirection * (rocketSpeed * _environmentController.TimeSpeed * Time.deltaTime);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
