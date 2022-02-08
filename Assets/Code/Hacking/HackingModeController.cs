using System;
using UnityEngine;

namespace Hacking
{
    public class HackingModeController : MonoBehaviour
    {
        private PlayerControls _playerControls;
        
        private void Awake()
        {
            _playerControls = new PlayerControls();      
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
    }
}
