using System;
using UnityEngine;
using CartridgeType = Game.CartridgeManager.CartridgeType;


public class CartridgeObject : MonoBehaviour
{
    public CartridgeType Type;
    [SerializeField] private Sprite[] cartridgesSprites0;
    [SerializeField] private Sprite[] cartridgesSprites1;
    [SerializeField] private Sprite[] cartridgesSprites2;
    [SerializeField] private Sprite[] cartridgesSprites3;
    [SerializeField] private Sprite[] cartridgesSprites4;

    private float _frameLength =0.25f;
    private float _frameTimer;
    private int _currentFrame = 0;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = cartridgesSprites0[(int)Type];
        
    }

    private void Update()
    {
        _frameTimer += Time.deltaTime;
        if(_frameTimer>=_frameLength)
        {
            _frameTimer = 0;
            _currentFrame++;
            if (_currentFrame >= 8)
            {
                _currentFrame = 0;
            }

            switch (_currentFrame)
            {
                case 0:
                    _spriteRenderer.sprite = cartridgesSprites0[(int) Type]; break;
                case 1:
                    _spriteRenderer.sprite = cartridgesSprites1[(int) Type]; break;
                case 2:
                    _spriteRenderer.sprite = cartridgesSprites2[(int) Type]; break;
                case 3:
                    _spriteRenderer.sprite = cartridgesSprites3[(int) Type]; break;
                case 4:
                    _spriteRenderer.sprite = cartridgesSprites4[(int) Type]; break;
                case 5:
                    _spriteRenderer.sprite = cartridgesSprites3[(int) Type]; break;
                case 6:
                    _spriteRenderer.sprite = cartridgesSprites2[(int) Type]; break;
                case 7:
                    _spriteRenderer.sprite = cartridgesSprites1[(int) Type]; break;
            }
        }
    }
}