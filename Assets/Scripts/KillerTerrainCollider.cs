using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KillerTerrainCollider : MonoBehaviour
{
    private PlayerLifeController _playerLifeController;

    private void Start()
    {
        _playerLifeController = transform.parent.GetComponent<PlayerLifeController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerKiller>() != null)
        {
            if (other.GetComponent<Tilemap>() != null)
            {
                _playerLifeController.Death();
            }
        }
    }
}
