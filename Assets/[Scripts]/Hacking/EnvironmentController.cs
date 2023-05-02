using Dangers;
using UnityEngine;

namespace Hacking
{
    public class EnvironmentController : MonoBehaviour
    {
        [SerializeField] private Bit rotationBitValue1;
        [SerializeField] private Bit rotationBitValue2;
        [SerializeField] private Bit rotationBitValue3;
        [SerializeField] private Bit rotationBitValue4;
        [SerializeField] private Bit switchingBitValue1;
        [SerializeField] private Bit switchingBitValue2;

        //[SerializeField] Bit enemiesC;
        [SerializeField] private Bit rocketControlBitValue;
        [SerializeField] private Bit rocketBitValue1;
        [SerializeField] private Bit rocketBitValue2;
        [SerializeField] private Bit timerFreezeBitValue;
        [SerializeField] private Bit slowMotionBitValue;
        [SerializeField] private Bit gravityControlBitValue;

        private GameObject[] _bluePlatform;
        private GameObject[] _greenPlatform;
        private GameObject[] _yellowPos1;
        private GameObject[] _yellowPos2;
        private GameObject[] _yellowPlatform;
        private GameObject[] _brownPos1;
        private GameObject[] _brownPos2;
        private GameObject[] _brownPlatform;

        void Awake()
        {
            _yellowPos1 = GameObject.FindGameObjectsWithTag("YellowPos1");
            _yellowPos2 = GameObject.FindGameObjectsWithTag("YellowPos2");
            _yellowPlatform = GameObject.FindGameObjectsWithTag("YellowPlatform");

            _brownPos1 = GameObject.FindGameObjectsWithTag("BrownPos1");
            _brownPos2 = GameObject.FindGameObjectsWithTag("BrownPos2");
            _brownPlatform = GameObject.FindGameObjectsWithTag("BrownPlatform");

            _bluePlatform = GameObject.FindGameObjectsWithTag("BluePlatform");
            _greenPlatform = GameObject.FindGameObjectsWithTag("GreenPlatform");
        }

        [Header("Platforms")] [Header("Bits")] [SerializeField]
        int defaultRotZ_blue;

        [SerializeField] private int defaultRotZGreen;
        private bool _rotationBit1;
        private bool _rotationBit2;
        private bool _rotationBit3;
        private bool _rotationBit4;
        private bool _switchingBit1;
        private bool _switchingBit2;

        //bool enemiesC;
        [Header("Weapons")] [HideInInspector] public bool rocketControlBit;
        bool _rocketBit1;
        bool _rocketBit2;
        bool _timerFreezeBit;
        bool _slowMotionBit;
        bool _gravityControlBit;
        public float TimeSpeed { get; private set; } = 1;


        void Update()
        {
            EnvironmentControl();
            UpdateBitValue();
        }

        void EnvironmentControl()
        {

            float speed = 5f;

            for (int i = 0; i < _bluePlatform.Length; i++)
            {
                if (_rotationBit1 && _rotationBit2)
                {
                    _bluePlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZ_blue + 180f), speed);
                }
                else if (_rotationBit1 && !_rotationBit2)
                {
                    _bluePlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZ_blue + -90f), speed);
                }
                else if (!_rotationBit1 && _rotationBit2)
                {
                    _bluePlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZ_blue + 90f), speed);
                }
                else
                {
                    _bluePlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZ_blue), speed);
                }
            }

            for (int i = 0; i < _greenPlatform.Length; i++)
            {
                if (_rotationBit3 && _rotationBit4)
                {
                    _greenPlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZGreen + 180f), speed);
                }
                else if (_rotationBit3 && !_rotationBit4)
                {
                    _greenPlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZGreen + -90f), speed);
                }
                else if (!_rotationBit3 && _rotationBit4)
                {
                    _greenPlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZGreen + 90f), speed);
                }
                else
                {
                    _greenPlatform[i].transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.Euler(0f, 0f, defaultRotZGreen), speed);
                }
            }
        
            if (_switchingBit1)
            {
                for (int i = 0; i < _yellowPlatform.Length; i++)
                {
                    _yellowPlatform[i].transform.position = _yellowPos2[i].transform.position;
                }
            }
            else
            {
                for (int i = 0; i < _yellowPlatform.Length; i++)
                {
                    _yellowPlatform[i].transform.position = _yellowPos1[i].transform.position;
                }
            }

            if (_switchingBit2)
            {
                for (int i = 0; i < _brownPlatform.Length; i++)
                {
                    _brownPlatform[i].transform.position = _brownPos2[i].transform.position;
                }
            }
            else
            {
                for (int i = 0; i < _brownPlatform.Length; i++)
                {
                    _brownPlatform[i].transform.position = _brownPos1[i].transform.position;
                }
            }



            if (rocketControlBit)
            {
                Vector3 nextDirection;
                GameObject[] rocketsInScene = GameObject.FindGameObjectsWithTag("Rocket");
                //up
                if (_rocketBit1 && _rocketBit2)
                {
                    nextDirection = new Vector3(0f, 1f, 0f);
                }
                else if (_rocketBit1 && !_rocketBit2)
                {
                    nextDirection = new Vector3(-1f, 0f, 0f);
                }
                else if (!_rocketBit1 && _rocketBit2)
                {
                    nextDirection = new Vector3(1f, 0f, 0f);
                }
                else
                {
                    nextDirection = new Vector3(0f, -1f, 0f);
                }

                for (int i = 0; i < rocketsInScene.Length; i++)
                {
                    rocketsInScene[i].GetComponent<RocketController>().currentDirection = nextDirection;
                }
            }
        
            if (_timerFreezeBit)
            {
                TimeSpeed = 0f;
            }
            else if (_slowMotionBit)
            {
                TimeSpeed = 0.5f;
            }
            else
            {
                TimeSpeed = 1f;
            }

            if (_gravityControlBit)
            {
                Physics2D.gravity = new Vector2(0, 9.81f);
            }
            else
            {
                Physics2D.gravity = new Vector2(0, -9.81f);
            }

        }

        void UpdateBitValue()
        {
            _rotationBit1 = rotationBitValue1.valueIsOne;
            _rotationBit2 = rotationBitValue2.valueIsOne;
            _rotationBit3 = rotationBitValue3.valueIsOne;
            _rotationBit4 = rotationBitValue4.valueIsOne;
            _switchingBit1 = switchingBitValue1.valueIsOne;
            _switchingBit2 = switchingBitValue2.valueIsOne;
            rocketControlBit = rocketControlBitValue.valueIsOne;
            _rocketBit1 = rocketBitValue1.valueIsOne;
            _rocketBit2 = rocketBitValue2.valueIsOne;
            _timerFreezeBit = timerFreezeBitValue.valueIsOne;
            _slowMotionBit = slowMotionBitValue.valueIsOne;
            _gravityControlBit = gravityControlBitValue.valueIsOne;
        }
    }
}