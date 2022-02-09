using System.Linq;
using UnityEngine;

namespace Hacking
{
    public class HackingUIController : MonoBehaviour
    {
        [SerializeField] private GameObject[] hackingUISectionsGameObjects;
        [SerializeField] private RectTransform currentBitIndicatorTransform;
        [SerializeField] private RectTransform sectionsHolder;
        private HackingUISectionController[] _hackingUISectionControllers;
        private RectTransform[] _hackingUISectionsTransforms;

        private void Awake()
        {
            _hackingUISectionControllers = new HackingUISectionController[hackingUISectionsGameObjects.Length];
            _hackingUISectionsTransforms = new RectTransform[hackingUISectionsGameObjects.Length];

            for (int i = 0; i < _hackingUISectionControllers.Length; i++)
            {
                _hackingUISectionControllers[i] =
                    Instantiate(hackingUISectionsGameObjects[i], sectionsHolder)
                        .GetComponent<HackingUISectionController>();
                _hackingUISectionsTransforms[i] = _hackingUISectionControllers[i].GetComponent<RectTransform>();
            }
        }
        
        public int GetAmountOfAllBits()
        {
            int sum = 0;
            foreach (HackingUISectionController hackingUISectionController in _hackingUISectionControllers)
            {
                sum += hackingUISectionController.BitsAmount;
            }

            return sum;
        }
    }
}