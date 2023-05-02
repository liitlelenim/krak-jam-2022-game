using Hacking;
using UnityEngine;

namespace Dangers
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField] Rigidbody2D rocketRB;
        EnvironmentController _environmentController;
        public Vector3 direction;
        [SerializeField] float timer = 3;
        [SerializeField] float timeBetweenShots = 3;
        [SerializeField] float timeBetweenShotsInSlowMotion = 6;
        void Awake()
        {
            _environmentController = FindObjectOfType<EnvironmentController>();
        }
        void Update()
        {
            if (_environmentController.TimeSpeed == 0.5f)
            {
                timeBetweenShots = timeBetweenShotsInSlowMotion;
            }

            if (timer > 0 && _environmentController.TimeSpeed != 0f)
            {
                timer -= Time.deltaTime;
            }
            
            if(timer<=0)
            {
                Instantiate(rocketRB, transform.position, transform.rotation, transform);
                timer = timeBetweenShots;
            }
        }
    }
}
