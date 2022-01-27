using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingMode : MonoBehaviour
{
    #region Assignment
    [SerializeField] Transform[] bitsTranform;
    [Header("Sprites")]
    [SerializeField] Sprite bitZeroSprite;
    [SerializeField] Sprite bitOneSprite;

    PlayerControls playerControls;
    GameObject bitsMenu;
    GameObject pointer;
    GameObject leftArrow;
    GameObject rightArrow; 
    GameObject canvas;
    void Awake()
    {
        playerControls = new PlayerControls();

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
    public bool playerIsHacking;
    int bitIndex;
    public float timeSpeed;
    #endregion Variables
    void Update()
    {
        if (playerIsHacking)
        {
            foreach (Transform bit in bitsTranform)
                bit.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            pointer.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            canvas.SetActive(true);
            SwitchBetweenBits();
        }
        if (!playerIsHacking)
        {
            foreach (Transform bit in bitsTranform)
                bit.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            pointer.SetActive(false);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            canvas.SetActive(false);
        }
        Debug.Log(timeSpeed);
    }

    #region Operations on bits
    void SwitchBetweenBits()
    {
        bitIndex = Mathf.Clamp(bitIndex, 0, bitsTranform.Length - 1);
        Vector2 pointerPos = new Vector2(bitsTranform[bitIndex].position.x, bitsTranform[bitIndex].position.y - 0.75f);
        pointer.transform.position = pointerPos;
    }

    void ChangeBitValue()
    {
        if (playerIsHacking)
        {
            Sprite sprite = bitsTranform[bitIndex].gameObject.GetComponent<SpriteRenderer>().sprite;
            if (sprite == bitZeroSprite)
            {
                bitsTranform[bitIndex].gameObject.GetComponent<SpriteRenderer>().sprite = bitOneSprite;
                bitsTranform[bitIndex].gameObject.GetComponent<EnviormentController>().bitEquelsOne = true;
            }
            else if (sprite == bitOneSprite)
            {
                bitsTranform[bitIndex].gameObject.GetComponent<SpriteRenderer>().sprite = bitZeroSprite;
                bitsTranform[bitIndex].gameObject.GetComponent<EnviormentController>().bitEquelsOne = false;
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
