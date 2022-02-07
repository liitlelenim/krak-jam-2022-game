using System.Collections;
using Hacking;
using Legacy;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Player
{
    public class PlayerLifeController : MonoBehaviour
    {
        [SerializeField] private Sprite deathSprite;
        [SerializeField] private GameObject bloodParticles;
        private bool _dead = false;
        private PlayerMovement _playerMovement;
        private HackingMode _hackingMode;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _boxCollider;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _hackingMode = GetComponent<HackingMode>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }
    
        public void Death()
        {
            if (!_dead)
            {
                _dead = true;
                _playerMovement.enabled = false;
                _hackingMode.enabled = false;
                _animator.enabled = false;
                _spriteRenderer.sprite = deathSprite;
                _boxCollider.size = new Vector2(1.5f, 0.45f);
                _boxCollider.offset = new Vector2(0.007746935f, -0.012362f);
                Instantiate(bloodParticles, transform.position + transform.up * 1f, Quaternion.identity);
                StartCoroutine(RestartSceneWithDelay());
            }
        }

        private IEnumerator RestartSceneWithDelay()
        {
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerKiller>() != null)
            {
                if (other.GetComponent<Tilemap>() == null)
                {
                    Death();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerKiller>() != null)
            {
                Death();
            }

        }
    }
}