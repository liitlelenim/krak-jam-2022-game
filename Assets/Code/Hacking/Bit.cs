using UnityEngine;

namespace Hacking
{
    public class Bit : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [Header("Sprites")] [SerializeField] private Sprite bitZeroSprite;
        [SerializeField] private Sprite bitOneSprite;
        public bool valueIsOne;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeValue()
        {
            valueIsOne = !valueIsOne;
            _spriteRenderer.sprite = valueIsOne ? bitOneSprite : bitZeroSprite;
        }
    }
}