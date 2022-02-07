using Hacking;
using Legacy;
using UnityEngine;

namespace Player
{
    public class PlayerFrozenGhostBehaviour : MonoBehaviour
    {
        [SerializeField] private Sprite[] playerSprites;
        [SerializeField] private Sprite[] frozenSprites;

        [SerializeField] private SpriteRenderer playerSpriteRenderer;
        [SerializeField] private HackingMode hackingMode;

        private SpriteRenderer _spriteRenderer;

        private readonly Color _transparentColor = new Color(0, 0, 0, 0);
        private readonly Color _defaultColor = new Color(1, 1, 1, 1);

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            _spriteRenderer.color = hackingMode.playerIsHacking ? _defaultColor : _transparentColor;
            Sprite playerCurrentSprite = playerSpriteRenderer.sprite;
            Sprite spriteToDisplay;
            if (playerCurrentSprite == playerSprites[1])
            {
                spriteToDisplay = frozenSprites[1];
            }
            else if (playerCurrentSprite == playerSprites[2])
            {
                spriteToDisplay = frozenSprites[2];
            }
            else if (playerCurrentSprite == playerSprites[3])
            {
                spriteToDisplay = frozenSprites[3];
            }
            else
            {
                spriteToDisplay = frozenSprites[0];
            }

            _spriteRenderer.sprite = spriteToDisplay;
            _spriteRenderer.flipX = playerSpriteRenderer.flipX;
        }
    }
}