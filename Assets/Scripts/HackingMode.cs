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
    Transform pointer;
    void Awake()
    {
        playerControls = new PlayerControls();

        bitsMenu = GameObject.Find("BitsMenu");
        bitsMenu.SetActive(false);

        pointer = bitsMenu.transform.GetChild(0).transform;

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

    #endregion Variables
    void Update()
    {
        if (playerIsHacking)
        {
            bitsMenu.SetActive(true);
            SwitchBetweenBits();
        }
        if (!playerIsHacking)
            bitsMenu.SetActive(false);
    }

    #region Operations on bits
    void SwitchBetweenBits()
    {
        bitIndex = Mathf.Clamp(bitIndex, 0, bitsTranform.Length - 1);
        Vector2 pointerPos = new Vector2(bitsTranform[bitIndex].position.x, bitsTranform[bitIndex].position.y - 0.75f);
        pointer.position = pointerPos;
    }

    void ChangeBitValue()
    {
        Sprite sprite = bitsTranform[bitIndex].gameObject.GetComponent<SpriteRenderer>().sprite;
        if (playerIsHacking)
        {
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
