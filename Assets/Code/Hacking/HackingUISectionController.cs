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
      public int BitsAmount => bitsGameObject.transform.childCount;

      private void Awake()
      {
         foreach (Image image in cosmeticsGameObject.GetComponentsInChildren<Image>())
         {
            image.color = cosmeticsColor;
         }  
      }
   }
}
