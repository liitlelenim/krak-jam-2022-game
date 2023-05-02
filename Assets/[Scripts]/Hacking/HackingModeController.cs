using System.Linq;
using Game;
using Player;
using UnityEngine;

namespace Hacking
{
    public class HackingModeController : MonoBehaviour
    {
        #region Assignments In Script
        private HackingUIController _hackingUIController;
        private PlayerControls _playerControls;
        private RamBarController _ramBarController;

        #endregion
        
        public int ChangedBitsAmount =>
            _hackingUIController.BitsControllers.Count((controller) =>
                controller.CurrentValue != controller.InitialValue);

        [SerializeField] private GameObject hackingModeUI;
        [SerializeField] private float goingToNextBitCooldown = 0.15f;
        
        private int BitsAmount => _hackingUIController.BitsAmount;
        private int _currentBitIndex = 0;
        private bool _isOpen = false;
        private int _currentBitGoingDirection = 0;
        private float _goingToNextBitTimer = 0f;

        private void Awake()
        {
            hackingModeUI.SetActive(false);
            _playerControls = new PlayerControls();
            _hackingUIController = hackingModeUI.GetComponent<HackingUIController>();
            _ramBarController = FindObjectOfType<RamBarController>();

            #region Input Actions
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
        #endregion
        
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

        #region Hacking Mode Activation/Disactivation
        private void EnableHackingMode()
        {
            _isOpen = true;
            hackingModeUI.SetActive(true);
            Time.timeScale = 0.2f;
        }

        private void DisableHackingMode()
        {
            _isOpen = false;
            hackingModeUI.SetActive(false);
            Time.timeScale = 1;
            _currentBitGoingDirection = 0;
        }
        #endregion

        #region Bit Actions
        private void ChangeCurrentBit()
        {
            if (_isOpen)
            {
                if (ChangedBitsAmount < _ramBarController.MaxValue ||
                    _hackingUIController.BitsControllers[_currentBitIndex].CurrentValue !=
                    _hackingUIController.BitsControllers[_currentBitIndex].InitialValue)
                {
                    _hackingUIController.BitsControllers[_currentBitIndex].ChangeValue();
                    _ramBarController.CurrentValue = _ramBarController.MaxValue - ChangedBitsAmount;
                }
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
        #endregion

        #region On Enable - On Disable
        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }
        #endregion
    }
}