using System;
using TowerDefence.Units.Common;
using UnityEngine;

namespace TowerDefence.GameController
{
    public class GamePiecePlacementController : MonoBehaviour
    {
        private GameObject currentGamePiece;

        public Action<GameObject> PieceReleasedEvent;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1) && currentGamePiece == null)
            {
                HandlePieceDestruction();
                return;
            }

            if (currentGamePiece == null)
                return;

            HandleNewObjectCancelationEvent();
            MoveCurrentPlaceableObjectToMouse();
            if (Input.GetMouseButtonDown(0))
            {
                CommitGamePiece();
            }
        }

        private void HandlePieceDestruction()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.GetComponent<PlaceableUnitComponent>() != null)
                {
                    PieceReleasedEvent?.Invoke(hitInfo.collider.gameObject);
                }
            }
        }

        private void CommitGamePiece()
        {
            currentGamePiece.GetComponent<PlaceableUnitComponent>().UnitPlacedEvent?.Invoke();
            currentGamePiece = null;
        }

        private void MoveCurrentPlaceableObjectToMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            string placementTag = currentGamePiece.GetComponent<PlaceableUnitComponent>().PlaceableTerrainTag;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.CompareTag(placementTag) && Quaternion.FromToRotation(Vector3.up, hitInfo.normal).eulerAngles == Vector3.zero)
                {
                    currentGamePiece.transform.position = hitInfo.point;
                }
            }
        }

        public void HandleNewObjectEvent(GameObject GamePiecePrefab)
        {
            if (currentGamePiece == null)
            {
                currentGamePiece = GamePiecePrefab;
            }
        }

        private void HandleNewObjectCancelationEvent()
        {
            if (Input.GetMouseButtonUp(1) && currentGamePiece != null)
            {
                PieceReleasedEvent?.Invoke(currentGamePiece);
            }
        }
    }
}
