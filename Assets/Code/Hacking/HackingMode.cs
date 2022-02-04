using Game;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hacking
{
    public class HackingMode : MonoBehaviour
    {
        private PlayerControls _playerControls;
        private GameObject _bitsMenu;
        private GameObject _pointer;
        private GameObject _leftArrow;
        private GameObject _rightArrow;
        [SerializeField] GameObject shadedTerrain;
        [SerializeField] GameObject[] bitsArray;
        private Text _instructionText;
        private Text _bitNameText;
        private Text _keyboardText;
        private PlayerMovement _playerMovement;
        private SoundsManager _soundsManager;
        private bool _openingFrame = false;
        void Awake()
        {
            _playerControls = new PlayerControls();
            _playerMovement = GetComponent<PlayerMovement>();
            _soundsManager = FindObjectOfType<SoundsManager>();
            bitsArray = GameObject.FindGameObjectsWithTag("Bit");
            _bitsMenu = GameObject.Find("BitsMenu");

            _pointer = _bitsMenu.transform.GetChild(0).gameObject;
            _leftArrow = _bitsMenu.transform.GetChild(1).gameObject;
            _rightArrow = _bitsMenu.transform.GetChild(2).gameObject;
            for (int i = 0; i < bitsArray.Length; i++)
            {
                Vector3 bitPosition = new Vector3((_leftArrow.transform.position.x + (0.6f * (i + 1))), bitsArray[i].transform.position.y, 0f);
                bitsArray[i].transform.position = bitPosition;
                if (i == bitsArray.Length - 1)
                {
                    _rightArrow.transform.position = new Vector3(bitsArray[i].transform.position.x + 0.6f, _rightArrow.transform.position.y, 0f);
                }
            }

            _instructionText = GameObject.Find("Instruction").GetComponent<Text>();

            _bitNameText = GameObject.Find("BitName").GetComponent<Text>();

            _keyboardText = GameObject.Find("Keyboard").GetComponent<Text>();
            _keyboardText.text = keysInPlayMode;

            _playerControls.Hacking.PreviousBit.performed += _ =>
            {
                HandleBitIndexChange(-1);
                _holdingPreviousBitButton = true;
                _holdingNextBitButton = false;
            };
            _playerControls.Hacking.PreviousBit.canceled += _ => _holdingPreviousBitButton = false;
            //Change bit
            _playerControls.Hacking.NextBit.performed += _ =>
            {
                HandleBitIndexChange(1);
                _holdingPreviousBitButton = false;
                _holdingNextBitButton = true;
            };
            _playerControls.Hacking.NextBit.canceled += _ => _holdingNextBitButton = false;
            //Change bit value
            _playerControls.Hacking.ChangeBit.performed += ctx => ChangeBitValue();

            _playerControls.Hacking.BackToMenu.performed += ctx =>
            {
                if (playerIsHacking)
                {
                    playerIsHacking = false;
                    SceneManager.LoadScene(27);
                }
            };

        }



        private bool _holdingNextBitButton = false;
        private bool _holdingPreviousBitButton = false;
        [SerializeField] private float timeBetweenBitChange = 0.5f;
        private float _bitChangeTimer = 0f;

        [HideInInspector] public bool playerIsHacking;
        [HideInInspector] public float timeSpeed = 1;

        string rotatePlatform = "ROTATE PLATFORM";
        string switchPlatfrom = "SWITCH PLATFORM POSITION";
        string rocketControl = "ROCKET CONTROL";
        string rocketFlightDirection = "ROCKET FLIGHT DIRECION";
        string timeFreeze = "TIME FREEZE";
        string slowMotion = "SLOW MOTION";
        string reverseGravity = "REVERSE GRAVITY";

        string rotationInstruction = "INSTRUCTION\n\n" +
                                     "00 - DEFAULT   11 - REVERSED\n" +
                                     "10 - LEFT     01 - RIGHT";
        string switchPlatformInstruction = "INSTRUCTION\n\n" +
                                           "0 - FIRST POSITION  \n" +
                                           "1 - SECOND POSITION";
        string turnOn = "INSTRUCTION\n\n" +
                        "0 - ON   1 - OFF";
        string direction = "INSTRUCTION\n\n" +
                           "00 - DOWN   11 - UP\n" +
                           "10 - LEFT     01 - RIGHT";

        string keysInHackingMode = "ARROW UP/DOWN - CHANGE BIT VALUE\n\n" +
                                   "X - QUIT HACKING MODE\n" +
                                   "ESCAPE - QUIT TO MENU";
        string keysInPlayMode = "Z - ENTER HACKING MODE\n\n" +
                                "< > - CHARACTER MOVEMENT\n\n" +
                                "X OR SPACE - JUMP";


       private int _bitIndex = 0;


        void Update()
        {
            InterfaceControl();
            ActivateHackingMode();
        }
        private void LateUpdate()
        {
            _openingFrame = false;
        }
        void ActivateHackingMode()
        {
            playerIsHacking = _playerControls.Hacking.Activate.ReadValue<float>() > 0.1f;
            if (playerIsHacking)
            {
                _openingFrame = true;
                _playerMovement.enabled = false;
                _keyboardText.text = keysInHackingMode;
                HandleDescriptionChange();
            }
            else if (!playerIsHacking)
            {
                _playerMovement.enabled = true;
                _keyboardText.text = keysInPlayMode;
                HandleDescriptionChange();
            }
        }
        void InterfaceControl()
        {

            if (playerIsHacking)
            {
                foreach (GameObject bit in bitsArray)
                    bit.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                _pointer.SetActive(true);
                _leftArrow.SetActive(true);
                _rightArrow.SetActive(true);
                shadedTerrain.SetActive(true);
                SwitchBetweenBits();
                Time.timeScale = 0;
                if (_holdingNextBitButton || _holdingPreviousBitButton)
                {
                    _bitChangeTimer += Time.unscaledDeltaTime;
                    if (_bitChangeTimer > timeBetweenBitChange)
                    {
                        if (_holdingNextBitButton)
                        {
                            HandleBitIndexChange(1);
                        }
                        else if (_holdingPreviousBitButton)
                        {
                            HandleBitIndexChange(-1);
                        }

                        _bitChangeTimer = 0;
                    }
                }
                else
                {
                    _bitChangeTimer = 0;
                }
            }

            if (!playerIsHacking)
            {
                foreach (GameObject bit in bitsArray)
                    bit.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                _pointer.SetActive(false);
                _leftArrow.SetActive(false);
                _rightArrow.SetActive(false);
                shadedTerrain.SetActive(false);
                _instructionText.text = null;
                _bitNameText.text = null;
                Time.timeScale = 1;
            }

        }
        private void HandleBitIndexChange(int change)
        {
            if (playerIsHacking && !_openingFrame)
            {
                if (_soundsManager != null)
                {
                    _soundsManager.PlaySound(1);
                }
                _bitIndex += change;
                if (_bitIndex < 0)
                {
                    _bitIndex = bitsArray.Length - 1;
                }
                else if (_bitIndex > bitsArray.Length - 1)
                {
                    _bitIndex = 0;
                }

                HandleDescriptionChange();
            }
        }
        private void HandleDescriptionChange()
        {
            if (playerIsHacking)
            {
                string bitName = bitsArray[_bitIndex].name;
                if (bitName == "BlueRotationFirst" || bitName == "BlueRotationSecond" || bitName == "GreenRotationFirst" || bitName == "GreenRotationSecond")
                {
                    _bitNameText.text = rotatePlatform;
                    _instructionText.text = rotationInstruction;
                }
                else if (bitName == "YellowChangePosition" || bitName == "BrownChangePosition")
                {

                    _bitNameText.text = switchPlatfrom;
                    _instructionText.text = switchPlatformInstruction;
                }
                else if (bitName == "RocketControl")
                {
                    _bitNameText.text = rocketControl;
                    _instructionText.text = turnOn;
                }
                else if (bitName == "DirectionBit" || bitName == "DirectionSecondBit")
                {
                    _bitNameText.text = rocketFlightDirection;
                    _instructionText.text = direction;
                }
                else if (bitName == "TimeFreeze")
                {
                    _bitNameText.text = timeFreeze;
                    _instructionText.text = turnOn;
                }
                else if (bitName == "SlowMotion")
                {
                    _bitNameText.text = slowMotion;
                    _instructionText.text = turnOn;
                }
                else if (bitName == "ReverseGravity")
                {
                    _bitNameText.text = reverseGravity;
                    _instructionText.text = turnOn;
                }

                else
                {
                    _instructionText.text = null;
                    _bitNameText.text = null;
                }
            }
        }
        void SwitchBetweenBits()
        {
            if (playerIsHacking)
            {
                _bitIndex = Mathf.Clamp(_bitIndex, 0, bitsArray.Length - 1);

                Vector2 pointerPos = new Vector2(bitsArray[_bitIndex].transform.position.x,
                    bitsArray[_bitIndex].transform.position.y - 0.75f);
                _pointer.transform.position = pointerPos;
            }
        }
        void ChangeBitValue()
        {
            if (!_openingFrame&&playerIsHacking)
            {
                if (_soundsManager != null)
                {
                    _soundsManager.PlaySound(0);
                }
                bitsArray[_bitIndex].GetComponent<Bit>().ChangeValue();
            }
        }
        void OnEnable()
        {
            _playerControls.Enable();
        }

        void OnDisable()
        {
            _playerControls.Disable();
        }

    }
}