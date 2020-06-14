using System.Collections;
using TMPro;
using TowerDefence.Common;
using TowerDefence.Units.RoadBlock;
using TowerDefence.Units.Tower;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.GameController.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public InventoryConfig config;
        public GameObject TowerPrefab;
        public GameObject RoadBlockPrefab;
        public GamePiecePlacementController PlacementController;

        public TMP_Text TowerQuantityText;
        public TMP_Text RoadBlockQuantityText;
        public Button TowerButton;
        public Button RoadBlockButton;

        private ObjectPooler towerPool;
        private Coroutine towerCooldownCoroutine;

        private ObjectPooler roadBlockPool;

        private int availableTowers;
        private int availableRoadBlocks;

        public void Init()
        {
            availableTowers = config.NumberOfTowers;
            availableRoadBlocks = config.NumberOfRoadBlock;
            towerPool = new ObjectPooler(config.NumberOfTowers, TowerPrefab);
            roadBlockPool = new ObjectPooler(config.NumberOfRoadBlock, RoadBlockPrefab);
            UpdateQuantityText(TowerQuantityText, config.NumberOfTowers, availableTowers);
            UpdateQuantityText(RoadBlockQuantityText, config.NumberOfRoadBlock, availableRoadBlocks);
            PlacementController.PieceReleasedEvent += HandleReturnedObjectEvent;
        }

        private void OnDestroy()
        {
            PlacementController.PieceReleasedEvent -= HandleReturnedObjectEvent;
        }

        private void UpdateQuantityText(TMP_Text text, int maxQuantity, int quantity)
        {
            text.SetText($"{maxQuantity}/{quantity}");
        }

        public void HandleNewTowerEvent()
        {
            if (!towerPool.HasAvailableObject())
                return;
            var obj = towerPool.GetObject();
            PlacementController.HandleNewObjectEvent(obj);

            availableTowers--;
            towerCooldownCoroutine = StartCoroutine(StartCooldown(TowerButton, config.TowerCooldown, TowerQuantityText, config.NumberOfTowers, availableTowers));
        }

        public void HandleNewRoadBlockEvent()
        {
            if (!roadBlockPool.HasAvailableObject())
                return;
            var obj = roadBlockPool.GetObject();
            PlacementController.HandleNewObjectEvent(obj);
            obj.GetComponent<RoadBlock>().OnDeathEvent += HandleReturnedObjectEvent;
            RoadBlockButton.interactable = false;

            availableRoadBlocks--;
        }

        IEnumerator StartCooldown(Button button, float cooldownInSeconds, TMP_Text text, int maxQuantity, int quantity)
        {
            UpdateQuantityText(text, maxQuantity, quantity);
            button.interactable = false;
            yield return new WaitForSeconds(cooldownInSeconds);
            button.interactable = quantity > 0;
            UpdateQuantityText(text, maxQuantity, quantity);
        }

        private void HandleReturnedObjectEvent(GameObject obj)
        {
            obj.SetActive(false);
            TMP_Text text = default;
            int maxQuantity = 0;
            int quantity = 0;

            if (obj.GetComponent<Tower>() != null)
            {
                availableTowers++;
                text = TowerQuantityText;
                maxQuantity = config.NumberOfTowers;
                quantity = availableTowers;
                StopCoroutine(towerCooldownCoroutine);
                TowerButton.interactable = availableTowers > 0;
            }
            else if (obj.GetComponent<RoadBlock>() != null)
            {
                availableRoadBlocks++;
                text = RoadBlockQuantityText;
                maxQuantity = config.NumberOfRoadBlock;
                quantity = availableRoadBlocks;
                StartCoroutine(StartCooldown(RoadBlockButton, config.RoadBlockCooldown, RoadBlockQuantityText, config.NumberOfRoadBlock, availableRoadBlocks));
                RoadBlockButton.interactable = availableRoadBlocks > 0;
                obj.GetComponent<RoadBlock>().OnDeathEvent -= HandleReturnedObjectEvent;
            }

            UpdateQuantityText(text, maxQuantity, quantity);
        }
    }
}
