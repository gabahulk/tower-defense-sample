using UnityEngine;

namespace TowerDefence.GameController
{
    public class GamePiecePlacementController : MonoBehaviour
    {
        [SerializeField]
        private GameObject GamePiecePrefab = default;
        private string PlaceTagFilter = "PlaceableTerrain";

        private KeyCode newTower = KeyCode.A; // debug
        private GameObject currentGamePiece;

        private void Update()
        {
            HandleNewObjectEvent();
            if (currentGamePiece == null)
                return;

            MoveCurrentPlaceableObjectToMouse();
            if (Input.GetMouseButtonDown(0))
            {
                CommitGamePiece();
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
                if (hitInfo.collider.gameObject.CompareTag(PlaceTagFilter) && Quaternion.FromToRotation(Vector3.up, hitInfo.normal).eulerAngles == Vector3.zero)
                {
                    currentGamePiece.transform.position = hitInfo.point;
                }
            }
        }

        public void HandleNewObjectEvent()
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
}
