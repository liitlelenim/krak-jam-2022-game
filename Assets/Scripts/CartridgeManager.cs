using System;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeManager : MonoBehaviour
{
    public HashSet<CartridgeType> OwnedCartridges { get; set; }

    public int OwnedCartridgesAmount => OwnedCartridges.Count;

    private void Start()
    {
        OwnedCartridges = new HashSet<CartridgeType>();
    }

    public enum CartridgeType
    {
        Red,
        Orange,
        Green,
        LightBlue,
        DarkBlue
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CartridgeObject>())
        {
            OwnedCartridges.Add(other.GetComponent<CartridgeObject>().Type);
            Destroy(other.gameObject);
        }
    }
}