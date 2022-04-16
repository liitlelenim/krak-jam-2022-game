using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hacking
{
   public class HackingUISectionController : MonoBehaviour
   {
      [SerializeField] private Color cosmeticsColor = Color.white;
      [SerializeField] private GameObject cosmeticsGameObject;
      [SerializeField] private GameObject bitsGameObject;
      public BitController[] HeldBits { get; private set; }
      public int BitsAmount => bitsGameObject.transform.childCount;

      private void Awake()
      {
         HeldBits = bitsGameObject.GetComponentsInChildren<BitController>();
         foreach (Image image in cosmeticsGameObject.GetComponentsInChildren<Image>())
         {
            image.color = cosmeticsColor;
         }  
      }
   }
}
