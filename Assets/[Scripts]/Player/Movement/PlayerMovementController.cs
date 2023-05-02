using System;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementController : MonoBehaviour
    {
        
        [SerializeField] private PlayerMovementMode playerDefaultMovementMode;
        
        private void Awake()
        {
            StartDefaultMovement();
        }

        public void StartDefaultMovement()
        {
            playerDefaultMovementMode.enabled = true;
        }

        public void StopPlayer()
        {
            
            playerDefaultMovementMode.enabled = false;
        }
    }
}
