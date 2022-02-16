using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class RamBarController : MonoBehaviour
    {
        [SerializeField] private int maxValue;

        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                maxValue = Mathf.Clamp(maxValue, 1, 10);
                AdjustRamBarSize();
            }
        }

        [SerializeField] private int currentValue;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0, MaxValue);
                AdjustRamBarValue();
            }
        }


        [Header("Required prefabs"), SerializeField, Space]
        private GameObject emptyImagePrefab;

        [Header("Required sprites")] [SerializeField]
        private Sprite leftRamSprite;

        [SerializeField] private Sprite middleRamSprite;
        [SerializeField] private Sprite rightRamSprite;
        [SerializeField] private Sprite emptyRamSlotSprite;
        [SerializeField] private Sprite fullRamSlotSprite;

        private GameObject[] _fullRamIndicatorsObjects;

        private void Awake()
        {
            AdjustRamBarSize();
            CurrentValue = 5;
        }

        private void AdjustRamBarSize()
        {
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }

            _fullRamIndicatorsObjects = new GameObject[maxValue];
            CreateSprite(leftRamSprite, "LeftRamSprite");
            for (int i = 1; i <= MaxValue; i++)
            {
                RectTransform ramSpriteTransform = CreateSprite(middleRamSprite, "MiddleRamSprite");
                ramSpriteTransform.anchoredPosition = new Vector2(i * 3.5f + 0.5f, 0);

                RectTransform ramSlotSprite = CreateSprite(emptyRamSlotSprite, "EmptySlotIndicator");
                ramSlotSprite.anchoredPosition = new Vector2(+i * 4, 0);

                RectTransform ramBatterySlotIndicator = CreateSprite(fullRamSlotSprite, "FullSlotIndicator");
                ramBatterySlotIndicator.anchoredPosition = new Vector2(i * 4, 0);
                _fullRamIndicatorsObjects[i - 1] = ramBatterySlotIndicator.gameObject;
                _fullRamIndicatorsObjects[i - 1].SetActive(false);
            }

            RectTransform lastMiddleRamSprite = CreateSprite(middleRamSprite, "MiddleRamSprite");
            lastMiddleRamSprite.anchoredPosition = new Vector3(maxValue * 4, 0);
            RectTransform rightRamSpriteTransform = CreateSprite(rightRamSprite, "RightRamSprite");
            rightRamSpriteTransform.anchoredPosition = new Vector2((MaxValue) * 4 + 3f, 0);
            AdjustRamBarValue();
        }

        private void AdjustRamBarValue()
        {
            for (int i = 0; i < _fullRamIndicatorsObjects.Length; i++)
            {
                _fullRamIndicatorsObjects[i].SetActive(i < CurrentValue);
            }
        }

        private RectTransform CreateSprite(Sprite sprite, String gameObjectName)
        {
            GameObject emptyImageObject = Instantiate(emptyImagePrefab, transform);
            emptyImageObject.name = gameObjectName;
            Image image = emptyImageObject.GetComponent<Image>();
            image.sprite = sprite;
            image.rectTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
            return image.GetComponent<RectTransform>();
        }
    }
}