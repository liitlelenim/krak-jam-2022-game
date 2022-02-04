using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 screenSize = new Vector2(9, 5);
    private Vector2Int _cameraOnGridPosition = new Vector2Int(0, 0);

    private void Start()
    {
        PlaceCameraOnGrid();
    }

    private void Update()
    {
        Vector3 playerPosition = playerTransform.position;
        if (playerPosition.x < 0)
        {
            _cameraOnGridPosition = new Vector2Int(
                (int) ((playerTransform.position.x - screenSize.x) / screenSize.x / 2),
                _cameraOnGridPosition.y);
        }
        else
        {
            _cameraOnGridPosition = new Vector2Int(
                (int) ((playerTransform.position.x + screenSize.x) / screenSize.x / 2),
                _cameraOnGridPosition.y);
        }

        if (playerPosition.y < 0)
        {
            _cameraOnGridPosition = new Vector2Int(_cameraOnGridPosition.x,
                (int) ((playerTransform.position.y - screenSize.y) / screenSize.y) / 2);
        }
        else
        {
            _cameraOnGridPosition = new Vector2Int(_cameraOnGridPosition.x,
                (int) ((playerTransform.position.y + screenSize.y) / screenSize.y) / 2);
        }

        PlaceCameraOnGrid();
    }

    private void PlaceCameraOnGrid()
    {
        transform.position = new Vector3(
            _cameraOnGridPosition.x * screenSize.x * 2,
            _cameraOnGridPosition.y * screenSize.y * 2,
            transform.position.z);
    }
}