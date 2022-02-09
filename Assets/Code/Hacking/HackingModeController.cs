using System;
using UnityEngine;

namespace Hacking
{
    public class HackingModeController : MonoBehaviour
    {

        [SerializeField] private GameObject hackingModeUI;
        private PlayerControls _playerControls;
        
        
        private void Awake()
        {
            hackingModeUI.SetActive(false);
            _playerControls = new PlayerControls();
            
            _playerControls.Hacking.Activate.performed += _ => EnableHackingMode();
            _playerControls.Hacking.Activate.canceled += _ => DisableHackingMode();

        }

        private void EnableHackingMode()
        {
            hackingModeUI.SetActive(true);
            Time.timeScale = 0;
        }

        private void DisableHackingMode()
        {
            hackingModeUI.SetActive(false);
            Time.timeScale = 1;
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
