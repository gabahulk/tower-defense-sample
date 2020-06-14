using UnityEngine;

namespace TowerDefence.GameController.Inventory
{
    [CreateAssetMenu(fileName = "InventoryConfig", menuName = "Game/InventoryConfig")]
    public class InventoryConfig : ScriptableObject
    {
        public int NumberOfTowers;
        public float TowerCooldown;

        public int NumberOfRoadBlock;
        public float RoadBlockCooldown;
    }
}