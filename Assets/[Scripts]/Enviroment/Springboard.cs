using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enviroment
{
    public class Springboard : MonoBehaviour
    {
        [SerializeField] private bool isActivated;
        private Collider2D triggerCollider;
        private SpriteRenderer spriteRenderer;
        [SerializeField] Color activatedSpringboardColor;
        [SerializeField] Color disactivatedSpringboardColor;
        private void Awake()
        {
            triggerCollider = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            StateChange();
        }
        public void StateChange()
        {
            isActivated = !isActivated;
            triggerCollider.enabled = isActivated;
            if (isActivated)
            {
                spriteRenderer.color = activatedSpringboardColor;
            }
            else
            {
                spriteRenderer.color = disactivatedSpringboardColor;
            }
        }
    }
}
