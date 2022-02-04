using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bit : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [Header("Sprites")] 
    [SerializeField] Sprite bitZeroSprite;
    [SerializeField] Sprite bitOneSprite;
    public bool valueIsOne;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeValue()
    {
        valueIsOne = !valueIsOne;
        if (valueIsOne == true)
            spriteRenderer.sprite = bitOneSprite;
        if (valueIsOne == false)
            spriteRenderer.sprite = bitZeroSprite;
    }
}
