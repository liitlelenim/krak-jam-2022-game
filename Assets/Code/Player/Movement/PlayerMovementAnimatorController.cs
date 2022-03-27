using System;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementAnimatorController : MonoBehaviour
    {

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        private const string PlayerIsWalkingAnimationBool = "IsWalking";

        private bool _isGrounded = false;

        public bool IsGrounded
        {
            get => _isGrounded;
            set
            {
                _isGrounded = value;
                AnimationUpdate();
            }
        }

        private int _horizontalMovementValue = 0; 
        public int HorizontalMovementValue
        {
            get => _horizontalMovementValue;
            set 
            {
                if (value != _horizontalMovementValue)
                {
                    if (value == 1)
                    {
                        _spriteRenderer.flipX = false;
                    }        
                    else if (value == -1)
                    {
                        _spriteRenderer.flipX = true;
                    }
                }

                _horizontalMovementValue = value;
                AnimationUpdate();
            }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void AnimationUpdate()
        {
            _animator.SetBool(PlayerIsWalkingAnimationBool,IsGrounded&&HorizontalMovementValue!=0);
        }
    }
}
