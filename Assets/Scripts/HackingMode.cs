using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingMode : MonoBehaviour
{
    #region Assignment
    [Header("Sprites")]
    [SerializeField] Sprite bitZeroSprite;
    [SerializeField] Sprite bitOneSprite;

    PlayerControls playerControls;
    GameObject bitsMenu;
    GameObject pointer;
    GameObject leftArrow;
    GameObject rightArrow; 
    GameObject canvas;
    GameObject[] bitsArray;
    void Awake()
    {
        playerControls = new PlayerControls();
        bitsArray = GameObject.FindGameObjectsWithTag("Bit");
        bitsMenu = GameObject.Find("BitsMenu");
        pointer = bitsMenu.transform.GetChild(0).gameObject;
        leftArrow = bitsMenu.transform.GetChild(1).gameObject;
        rightArrow = bitsMenu.transform.GetChild(2).gameObject;
        canvas = bitsMenu.transform.GetChild(3).gameObject;

        #region Input Actions
        //Hacking start
        playerControls.Hacking.Activate.performed += ctx => playerIsHacking = !playerIsHacking;
        //Switch bit
        playerControls.Hacking.PreviousBit.performed += ctx => bitIndex--;
        playerControls.Hacking.NextBit.performed += ctx => bitIndex++;
        //ChangeBitValue
        playerControls.Hacking.ChangeBit.performed += ctx => ChangeBitValue();

        #endregion Inpur Actions
    }
    #endregion Assignment

    #region Variables
    [HideInInspector] public bool playerIsHacking;
    [HideInInspector] public float timeSpeed;
    int bitIndex;
    #endregion Variables
    void Update()
    {
        if (playerIsHacking)
        {
            foreach (GameObject bit in bitsArray)
                bit.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            pointer.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            canvas.SetActive(true);
            SwitchBetweenBits();
        }
        if (!playerIsHacking)
        {
            foreach (GameObject bit in bitsArray)
                bit.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            pointer.SetActive(false);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            canvas.SetActive(false);
        }
    }

    #region Operations on bits
    void SwitchBetweenBits()
    {
        bitIndex = Mathf.Clamp(bitIndex, 0, bitsArray.Length - 1);
        Vector2 pointerPos = new Vector2(bitsArray[bitIndex].transform.position.x, bitsArray[bitIndex].transform.position.y - 0.75f);
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
