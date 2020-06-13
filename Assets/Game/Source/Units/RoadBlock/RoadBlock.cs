using TowerDefence.Units.Common;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Units.RoadBlock
{
    public class RoadBlock : MonoBehaviour
    {
        public RoadBlockConfig Config;
        public NavMeshObstacle Obstacle;
        [SerializeField]
        private PlaceableUnitComponent placeableUnitComponent = default;

        private void Awake()
        {
            Init(Config);
        }

        public void Init(RoadBlockConfig config)
        {
            Obstacle.enabled = false;
            placeableUnitComponent.PlaceableTerrainTag = Config.PlaceableTerrainTag;
            placeableUnitComponent.UnitPlacedEvent += EnableWall;
        }

        private void EnableWall()
        {
            Obstacle.enabled = true;
        }

        private void OnDestroy()
        {
            placeableUnitComponent.UnitPlacedEvent -= EnableWall;
        }
    }
}
