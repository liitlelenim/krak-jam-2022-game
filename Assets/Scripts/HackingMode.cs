using System;
using UnityEngine.UI;
using UnityEngine;

public class HackingMode : MonoBehaviour
{
    #region Assignment
    PlayerControls playerControls;
    GameObject bitsMenu;
    GameObject pointer;
    GameObject leftArrow;
    GameObject rightArrow;
    [SerializeField] GameObject shadedTerrain;
    [SerializeField] GameObject[] bitsArray;
    Text instructionText;
    Text bitNameText;
    Text keyboardText;

    private PlayerMovement _playerMovement;

    private bool _openingFrame = false;
    void Awake()
    {
        playerControls = new PlayerControls();
        _playerMovement = GetComponent<PlayerMovement>();

        bitsArray = GameObject.FindGameObjectsWithTag("Bit");
        bitsMenu = GameObject.Find("BitsMenu");

        pointer = bitsMenu.transform.GetChild(0).gameObject;
        leftArrow = bitsMenu.transform.GetChild(1).gameObject;
        rightArrow = bitsMenu.transform.GetChild(2).gameObject;
        for (int i = 0; i < bitsArray.Length; i++)
        {
            Vector3 bitPosition = new Vector3((leftArrow.transform.position.x + (0.6f * (i + 1))), bitsArray[i].transform.position.y, 0f);
            bitsArray[i].transform.position = bitPosition;
            if (i == bitsArray.Length - 1)
            {
                rightArrow.transform.position = new Vector3(bitsArray[i].transform.position.x + 0.6f, rightArrow.transform.position.y, 0f);
            }
        }

        instructionText = GameObject.Find("Instruction").GetComponent<Text>();

        bitNameText = GameObject.Find("BitName").GetComponent<Text>();

        keyboardText = GameObject.Find("Keyboard").GetComponent<Text>();
        keyboardText.text = keysInPlayMode;
        #region Input Actions

        //Switch bit
        playerControls.Hacking.PreviousBit.performed += _ =>
        {
            HandleBitIndexChange(-1);
            _holdingPreviousBitButton = true;
            _holdingNextBitButton = false;
        };
        playerControls.Hacking.PreviousBit.canceled += _ => _holdingPreviousBitButton = false;
        //Change bit
        playerControls.Hacking.NextBit.performed += _ =>
        {
            HandleBitIndexChange(1);
            _holdingPreviousBitButton = false;
            _holdingNextBitButton = true;
        };
        playerControls.Hacking.NextBit.canceled += _ => _holdingNextBitButton = false;
        //Change bit value
        playerControls.Hacking.ChangeBit.performed += ctx => ChangeBitValue();
        // Start hacking
        playerControls.Hacking.Activate.performed += ctx =>
        {
            if (!playerIsHacking)
            {
                playerIsHacking = true;
                _openingFrame = true;
                _playerMovement.enabled = false;
                keyboardText.text = keysInHackingMode;
                HandleDescriptionChange();
            }
        };
        playerControls.Hacking.Quit.performed += _ =>
        {
            playerIsHacking = false;
            _playerMovement.enabled = true;
            keyboardText.text = keysInPlayMode;
            HandleDescriptionChange();
        };
        #endregion Inpur Actions
    }

    #endregion Assignment

    #region Variables

    private bool _holdingNextBitButton = false;
    private bool _holdingPreviousBitButton = false;
    [SerializeField] private float timeBetweenBitChange = 0.5f;
    private float _bitChangeTimer = 0f;

    [HideInInspector] public bool playerIsHacking;
    [HideInInspector] public float timeSpeed = 1;

    #region Text
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

    string keysInHackingMode = "Z - CHANGE BIT VALUE\n\n" +
        "X - QUIT HACKING MODE";
    string keysInPlayMode = "Z - ENTER HACKING MODE\n\n" +
        "< > - CHARACTER MOVEMENT\n\n" +
        "X - JUMP";

    #endregion Text

    [SerializeField] int bitIndex;

    #endregion Variables

    #region Update
    void Update()
    {
        if (playerIsHacking)
        {
            foreach (GameObject bit in bitsArray)
                bit.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            pointer.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
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
            pointer.SetActive(false);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            shadedTerrain.SetActive(false);
            instructionText.text = null;
            bitNameText.text = null;
            Time.timeScale = 1;
        }
    }

    private void LateUpdate()
    {
        _openingFrame = false;
    }

    #endregion Update

    #region Bits

    private void HandleBitIndexChange(int change)
    {
        if (playerIsHacking && !_openingFrame)
        {
            bitIndex += change;
            if (bitIndex < 0)
            {
                bitIndex = bitsArray.Length - 1;
            }
            else if (bitIndex > bitsArray.Length - 1)
            {
                bitIndex = 0;
            }

            HandleDescriptionChange();
        }
    }
    private void HandleDescriptionChange()
    {
        if (playerIsHacking)
        {
            string bitName = bitsArray[bitIndex].name;
            if (bitName == "BlueRotationFirst" || bitName == "BlueRotationSecond" || bitName == "GreenRotationFirst" || bitName == "GreenRotationSecond")
            {
                bitNameText.text = rotatePlatform;
                instructionText.text = rotationInstruction;
            }
            else if (bitName == "YellowChangePosition" || bitName == "BrownChangePosition")
            {

                bitNameText.text = switchPlatfrom;
                instructionText.text = switchPlatformInstruction;
            }
            else if (bitName == "RocketControl")
            {
                bitNameText.text = rocketControl;
                instructionText.text = turnOn;
            }
            else if (bitName == "DirectionBit" || bitName == "DirectionSecondBit")
            {
                bitNameText.text = rocketFlightDirection;
                instructionText.text = direction;
            }
            else if (bitName == "TimeFreeze")
            {
                bitNameText.text = timeFreeze;
                instructionText.text = turnOn;
            }
            else if (bitName == "SlowMotion")
            {
                bitNameText.text = slowMotion;
                instructionText.text = turnOn;
            }
            else if (bitName == "ReverseGravity")
            {
                bitNameText.text = reverseGravity;
                instructionText.text = turnOn;
            }

            else
            {
                instructionText.text = null;
                bitNameText.text = null;
            }
        }
    }
    void SwitchBetweenBits()
    {
        if (playerIsHacking)
        {
            bitIndex = Mathf.Clamp(bitIndex, 0, bitsArray.Length - 1);

            Vector2 pointerPos = new Vector2(bitsArray[bitIndex].transform.position.x,
                bitsArray[bitIndex].transform.position.y - 0.75f);
            pointer.transform.position = pointerPos;
        }
    }
    void ChangeBitValue()
    {
        if (!_openingFrame&&playerIsHacking)
        {
            bitsArray[bitIndex].GetComponent<Bit>().ChangeValue();
        }
    }

    #endregion Bits

    #region OnEnable OnDisable

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    #endregion OnEnable OnDisable
}