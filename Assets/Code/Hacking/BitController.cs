using System;
using Hacking.Mechanics;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace Hacking
{
    public class BitController : MonoBehaviour
    {
        [SerializeField] private bool initialValue;

        //Bit mechanic must implement IHackingMechanicInterface to work properly
        [SerializeField, SerializeReference] private HackingMechanic bitMechanic;

        public bool CurrentValue { get; set; }
        [SerializeField] private Sprite[] sprites;
        private Image _image;

        private void Awake()
        {
            if (bitMechanic == null)
            {
                Debug.LogError("Bit mechanic is null!");
            }


            _image = GetComponent<Image>();
            CurrentValue = initialValue;
            SetAccordingSprite();

            if (bitMechanic != null)
            {
                bitMechanic.Initialize();
            }
        }

        private void SetAccordingSprite()
        {
            if (CurrentValue == initialValue)
            {
                _image.sprite = CurrentValue
                    ? sprites[1]
                    : sprites[0];
            }
            else
            {
                _image.sprite = CurrentValue
                    ? sprites[3]
                    : sprites[2];
            }
        }

        public void ChangeValue()
        {
            CurrentValue = !CurrentValue;
            if (bitMechanic != null)
            {
                if (CurrentValue)
                {
                    bitMechanic.OnActivate();
                }
                else
                {
                    bitMechanic.OnDeactivate();
                }
            }

            SetAccordingSprite();
        }
    }
}