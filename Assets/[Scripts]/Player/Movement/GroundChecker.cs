using System;
using System.Collections.Generic;
using Enviroment;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Player.Movement
{
    [RequireComponent(typeof(Collider2D))]
    public class GroundChecker : MonoBehaviour
    {
     
        private bool IsTouchingGround => _currentlyTouching.Count>0;
        public bool IsEnabled { get; set; } = true;
        public bool IsGrounded => IsTouchingGround && IsEnabled;

        private List<Collider2D> _currentlyTouching = new List<Collider2D>(); 
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<GroundingCollider>() != null)
            {
                _currentlyTouching.Add(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<GroundingCollider>() != null)
            {
                _currentlyTouching.Remove(other);
            }
        }
    }
}
