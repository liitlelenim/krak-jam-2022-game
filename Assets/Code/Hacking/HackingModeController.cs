using System;
using Player;
using UnityEngine;

namespace Hacking
{
    public class HackingModeController : MonoBehaviour
    {
        [SerializeField] private GameObject hackingModeUI;
        [SerializeField] private float goingToNextBitCooldown = 0.15f;
        private HackingUIController _hackingUIController;
        private PlayerControls _playerControls;
        private PlayerMovement _playerMovement;
        private int BitsAmount => _hackingUIController.BitsAmount;
        private int _currentBitIndex = 0;
        private bool _isOpen = false;
        private int _currentBitGoingDirection = 0;
        private float _goingToNextBitTimer = 0f;

        private void Awake()
        {
            hackingModeUI.SetActive(false);
            _playerControls = new PlayerControls();
            _playerMovement = FindObjectOfType<PlayerMovement>();
            _hackingUIController = hackingModeUI.GetComponent<HackingUIController>();
            _playerControls.Hacking.Activate.performed += _ => EnableHackingMode();
            _playerControls.Hacking.Activate.canceled += _ => DisableHackingMode();

            _playerControls.Hacking.ChangeBit.performed += _ => ChangeCurrentBit();

            _playerControls.Hacking.NextBit.performed += _ =>
            {
                if (_isOpen)
                {
                    _currentBitGoingDirection = 1;
                    GoToAnotherBit(1);
                }
            };
            _playerControls.Hacking.PreviousBit.performed += _ => 
            {
                if (_isOpen)
                {
                    _currentBitGoingDirection = -1;
                    GoToAnotherBit(-1);
                }
            };

            _playerControls.Hacking.NextBit.canceled += _ => _currentBitGoingDirection = 0;
            _playerControls.Hacking.PreviousBit.canceled += _ => _currentBitGoingDirection = 0;
        }

        private void Update()
        {
            if (_currentBitGoingDirection != 0)
            {
                _goingToNextBitTimer += Time.unscaledDeltaTime;
                if (_goingToNextBitTimer >= goingToNextBitCooldown)
                {
                    GoToAnotherBit(_currentBitGoingDirection);
                    _goingToNextBitTimer = 0;
                }
            }
            else
            {
                _goingToNextBitTimer = 0;
            }
       
        }

        private void EnableHackingMode()
        {
            _isOpen = true;
            hackingModeUI.SetActive(true);
            _playerMovement.enabled = false;
            Time.timeScale = 0;
        }

        private void DisableHackingMode()
        {
            _isOpen = false;
            hackingModeUI.SetActive(false);
            _playerMovement.enabled = true;
            Time.timeScale = 1;
            _currentBitGoingDirection = 0;
        }

        private void ChangeCurrentBit()
        {
            if (_isOpen)
            {
                _hackingUIController.BitsControllers[_currentBitIndex].ChangeValue();
            }
        }

        private void GoToAnotherBit(int positionChange)
        {
            _currentBitIndex += positionChange;
            if (_currentBitIndex >= BitsAmount)
            {
                _currentBitIndex = 0;
            }
            else if (_currentBitIndex < 0)
            {
                _currentBitIndex = BitsAmount - 1;
            }

            _hackingUIController.SetCurrentBitIndicatorAccordingly(_currentBitIndex);
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