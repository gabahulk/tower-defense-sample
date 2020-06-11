using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiecePlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject GamePiecePrefab;

    private KeyCode newTower = KeyCode.A; // debug
    private GameObject currentGamePiece;

    private void Update()
    {
        HandleNewObjectEvent();

        if (currentGamePiece != null)
        {
            MoveCurrentPlaceableObjectToMouse();
            if (Input.GetMouseButtonDown(0))
            {
                CommitGamePiece();
            }
        }
    }

    private void CommitGamePiece()
    {
        currentGamePiece = null;
    }

    private void MoveCurrentPlaceableObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.CompareTag("PlaceableTerrain"))
            {
                currentGamePiece.transform.position = hitInfo.point;
            }
        }
    }

    private void HandleNewObjectEvent()
    {
        if (Input.GetKeyUp(newTower))
        {
            if (currentGamePiece == null)
            {
                currentGamePiece = Instantiate(GamePiecePrefab);
            }
            else
            {
                Destroy(currentGamePiece);
            }

        }
    }
}
