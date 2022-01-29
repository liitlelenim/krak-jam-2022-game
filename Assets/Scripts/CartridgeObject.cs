using System;
using UnityEngine;
using CartridgeType = CartridgeManager.CartridgeType;


public class CartridgeObject : MonoBehaviour
{
    public CartridgeType Type;
    [SerializeField] private Sprite[] cartridgesSprites;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = cartridgesSprites[(int)Type];
    }
}