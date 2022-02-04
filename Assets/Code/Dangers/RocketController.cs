using Hacking;
using UnityEngine;

namespace Dangers
{
    public class RocketController : MonoBehaviour
    {
        #region Assignment
        EnvironmentController _environmentController;

        void Awake()
        {
            _environmentController = GameObject.Find("BitsMenu").GetComponent<EnvironmentController>();
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
            if (!_environmentController.rocketControlBit)
                currentDirection = firstDirection;
            transform.position += currentDirection * rocketSpeed * _environmentController.TimeSpeed * Time.deltaTime;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
