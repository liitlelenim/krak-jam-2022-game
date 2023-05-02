using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BatteryBarController : MonoBehaviour
    {
        [SerializeField] private int maxValue;

        public int MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                maxValue = Mathf.Clamp(maxValue, 1, 10);
                AdjustBatteryBarSize();
            }
        }

        [SerializeField] private int currentValue;

        public int CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0, MaxValue);
                AdjustBatteryBarValue();
            }
        }


        [Header("Required prefabs"), SerializeField, Space]
        private GameObject emptyImagePrefab;

        [Header("Required sprites")] [SerializeField]
        private Sprite leftBatterySprite;

        [SerializeField] private Sprite middleBatterySprite;
        [SerializeField] private Sprite rightBatterySprite;
        [SerializeField] private Sprite emptyBatterySlotSprite;
        [SerializeField] private Sprite fullBatterySlotSprite;

        private GameObject[] _fullBatteryIndicatorsObjects;

        private void Awake()
        {
            AdjustBatteryBarSize();
            CurrentValue = 5;
        }

        private void Update()
        {
        }

        private void AdjustBatteryBarSize()
        {
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject);
            }

            _fullBatteryIndicatorsObjects = new GameObject[maxValue];
            CreateSprite(rightBatterySprite, "RightBatterySprite");
            for (int i = 1; i <= MaxValue; i++)
            {
                RectTransform batterySpriteTransform = CreateSprite(middleBatterySprite, "MiddleBatterySprite");
                batterySpriteTransform.anchoredPosition = new Vector2(-i * 3 - 0.5f, 0);

                RectTransform batterySlotSprite = CreateSprite(emptyBatterySlotSprite, "EmptySlotIndicator");
                batterySlotSprite.anchoredPosition = new Vector2(-i * 3, 0);

                RectTransform fullBatterySlotIndicator = CreateSprite(fullBatterySlotSprite, "FullSlotIndicator");
                fullBatterySlotIndicator.anchoredPosition = new Vector2(-i * 3, 0);
                _fullBatteryIndicatorsObjects[i - 1] = fullBatterySlotIndicator.gameObject;
                _fullBatteryIndicatorsObjects[i - 1].SetActive(false);
            }


            RectTransform rightBatterySpriteTransform = CreateSprite(leftBatterySprite, "LeftBatterySprite");
            rightBatterySpriteTransform.anchoredPosition = new Vector2(-(MaxValue + 1) * 3, 0);
            AdjustBatteryBarValue();
        }

        private void AdjustBatteryBarValue()
        {
            for (int i = 0; i < _fullBatteryIndicatorsObjects.Length; i++)
            {
                _fullBatteryIndicatorsObjects[i].SetActive(i < CurrentValue);
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