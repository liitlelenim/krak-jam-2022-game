using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] private Sprite deathSprite;
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

    private void Update()
    {

    }

    public void Death()
    {
        _playerMovement.enabled = false;
        _hackingMode.enabled = false;
        _animator.enabled = false;
        _spriteRenderer.sprite = deathSprite;
        _boxCollider.size = new Vector2(1.75f, 0.85f);
        _boxCollider.offset = new Vector2(0.007746935f, -0.012362f);
        StartCoroutine(RestartSceneWithDelay());
    }

    private IEnumerator RestartSceneWithDelay()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerKiller>() != null)
        {
            Death();
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