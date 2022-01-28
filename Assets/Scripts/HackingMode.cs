using UnityEngine.UI;
using UnityEngine;

public class HackingMode : MonoBehaviour
{
    #region Assignment
    [Header("Sprites")] [SerializeField] Sprite bitZeroSprite;
    [SerializeField] Sprite bitOneSprite;

    PlayerControls playerControls;
    GameObject bitsMenu;
    GameObject pointer;
    GameObject leftArrow;
    GameObject rightArrow;
    [SerializeField] GameObject shadedTerrain;
    [SerializeField] GameObject[] bitsArray;
    TextMesh textMesh;
    void Awake()
    {
        playerControls = new PlayerControls();
        bitsArray = GameObject.FindGameObjectsWithTag("Bit");
        bitsMenu = GameObject.Find("BitsMenu");
        pointer = bitsMenu.transform.GetChild(0).gameObject;
        leftArrow = bitsMenu.transform.GetChild(1).gameObject;
        rightArrow = bitsMenu.transform.GetChild(2).gameObject;
        textMesh = GameObject.Find("Instruction").GetComponent<TextMesh>();

        #region Input Actions

        //Hacking start
        playerControls.Hacking.Activate.performed += ctx => playerIsHacking = !playerIsHacking;
        //Switch bit
        playerControls.Hacking.PreviousBit.performed += _ =>
        {
            HandleBitIndexChange(-1);
            _holdingPreviousBitButton = true;
            _holdingNextBitButton = false;
        };
        playerControls.Hacking.PreviousBit.canceled += _ => _holdingPreviousBitButton = false;

        playerControls.Hacking.NextBit.performed += _ =>
        {
            HandleBitIndexChange(1);
            _holdingPreviousBitButton = false;
            _holdingNextBitButton = true;
        };
        playerControls.Hacking.NextBit.canceled += _ => _holdingNextBitButton = false;
        //ChangeBitValue
        playerControls.Hacking.ChangeBit.performed += ctx => ChangeBitValue();

        #endregion Inpur Actions
    }

    #endregion Assignment

    #region Variables

    private bool _holdingNextBitButton = false;
    private bool _holdingPreviousBitButton = false;
    [SerializeField] private float timeBetweenBitChange = 0.5f;
    private float _bitChangeTimer = 0f;

    [HideInInspector] public bool playerIsHacking;
    [HideInInspector] public float timeSpeed;

    [Header("Instructions")]
    string rotatePlatform = "ROTATE PLATFORM \n00 - DEFAULT   11 - REVERSE\n10 - LEFT     01 - RIGHT";
    string switchPlatfrom = "SWITCH PLATFORM POSITION\n0 - FIRST POSITION  \n1 - SECOND POSITION";
    string rocketControl = "ROCKET CONTROL\n0 - ON   1 - OFF";
    string rocketFlightDirection = "ROCKET FLIGHT DIRECION \n00 - DOWN   11 - UP\n10 - LEFT     01 - RIGHT";
    string timeFreeze = "TIME FREEZE\n0 - ON  \n1 - OF";
    string slowMotion = "SLOW MOTION\n0 - ON  \n1 - OF";
    string reverseGravity = "REVERSE GRAVITY\n0 - ON   1 - OFF";

    [SerializeField] int bitIndex;

    #endregion Variables

    private void HandleBitIndexChange(int change)
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
    }

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
                    else if(_holdingPreviousBitButton)
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
            Time.timeScale = 1;
        }
    }

    #region Operations on bits

    void SwitchBetweenBits()
    {
        bitIndex = Mathf.Clamp(bitIndex, 0, bitsArray.Length - 1);
        if (bitIndex == 0 || bitIndex == 1 || bitIndex == 2 || bitIndex == 3)
            textMesh.text = rotatePlatform;
        if (bitIndex == 4 || bitIndex == 5)
            textMesh.text = switchPlatfrom;
        if (bitIndex == 9)
            textMesh.text = rocketControl;
        if (bitIndex == 10 || bitIndex == 11)
            textMesh.text = rocketFlightDirection;
        if (bitIndex == 12)
            textMesh.text = timeFreeze;
        if (bitIndex == 13)
            textMesh.text = slowMotion;
        if (bitIndex == 14)
            textMesh.text = reverseGravity;
        Vector2 pointerPos = new Vector2(bitsArray[bitIndex].transform.position.x,
            bitsArray[bitIndex].transform.position.y - 0.75f);
        pointer.transform.position = pointerPos;
    }

    void ChangeBitValue()
    {
        if (playerIsHacking)
        {
            Sprite sprite = bitsArray[bitIndex].gameObject.GetComponent<SpriteRenderer>().sprite;
            if (sprite == bitZeroSprite)
            {
                bitsArray[bitIndex].gameObject.GetComponent<SpriteRenderer>().sprite = bitOneSprite;
                bitsArray[bitIndex].gameObject.GetComponent<EnviormentController>().bitEquelsOne = true;
            }
            else if (sprite == bitOneSprite)
            {
                bitsArray[bitIndex].gameObject.GetComponent<SpriteRenderer>().sprite = bitZeroSprite;
                bitsArray[bitIndex].gameObject.GetComponent<EnviormentController>().bitEquelsOne = false;
            }
        }
    }

    #endregion operations on bits

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