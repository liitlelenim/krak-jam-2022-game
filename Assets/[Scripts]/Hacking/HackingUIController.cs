using System.Collections.Generic;
using UnityEngine;

namespace Hacking
{
    public class HackingUIController : MonoBehaviour
    {
        public BitController[] BitsControllers { get; private set; }
        public int BitsAmount => BitsControllers.Length;
        [SerializeField] private GameObject[] hackingUISectionsGameObjects;
        [SerializeField] private RectTransform currentBitIndicatorTransform;
        [SerializeField] private Vector2 bitIndicatorOffset = new Vector2(25f, -100f);
        [SerializeField] private RectTransform sectionsHolder;
        
        private HackingUISectionController[] _hackingUISectionControllers;

        private void Awake()
        {
            _hackingUISectionControllers = new HackingUISectionController[hackingUISectionsGameObjects.Length];
            List<BitController> tempBitsControllers = new List<BitController>();
            for (int i = 0; i < _hackingUISectionControllers.Length; i++)
            {
                _hackingUISectionControllers[i] =
                    Instantiate(hackingUISectionsGameObjects[i], sectionsHolder)
                        .GetComponent<HackingUISectionController>();
                tempBitsControllers.AddRange(_hackingUISectionControllers[i].HeldBits);
            }

            BitsControllers = tempBitsControllers.ToArray();
            SetCurrentBitIndicatorAccordingly(0);
        }

        public void SetCurrentBitIndicatorAccordingly(int position)
        {
            currentBitIndicatorTransform.SetParent(BitsControllers[position].transform);
            currentBitIndicatorTransform.anchoredPosition = bitIndicatorOffset;
        }
    }
}