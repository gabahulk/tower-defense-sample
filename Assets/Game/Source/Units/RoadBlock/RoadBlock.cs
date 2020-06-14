using System;
using System.Collections;
using TowerDefence.Common.Health;
using TowerDefence.Units.Common;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Units.RoadBlock
{
    public class RoadBlock : MonoBehaviour, IKillable
    {
        public RoadBlockConfig Config;
        public NavMeshObstacle Obstacle;
        [SerializeField]
        private PlaceableUnitComponent placeableUnitComponent = default;

        private IHealthComponent healthComponent;
        [SerializeField]
        private HealthBar HealthBar = default;
        private WaitForSeconds waitForOneSecond;

        public Action<GameObject> OnDeathEvent { get; set; }

        private void Awake()
        {
            waitForOneSecond = new WaitForSeconds(1);
            Init(Config);
        }

        public void Init(RoadBlockConfig config)
        {
            if (healthComponent == null)
            {
                healthComponent = new HealthComponent(config.HealthDuration);
                healthComponent.OnHealthPercentageChanged += HealthBar.OnHealthChanged;
            }
            Obstacle.enabled = false;
            placeableUnitComponent.PlaceableTerrainTag = Config.PlaceableTerrainTag;
            placeableUnitComponent.UnitPlacedEvent += EnableWall;
        }

        private void EnableWall()
        {
            Obstacle.enabled = true;
            StartCoroutine(nameof(StartLosingHealth));
        }

        IEnumerator StartLosingHealth()
        {
            while (healthComponent.GetCurrentHealth() > 0)
            {
                yield return waitForOneSecond;
                TakeDamage(1);
            }
        }

        private void TakeDamage(int damage)
        {
            healthComponent.UpdateHealth(-damage);
            if (healthComponent.GetCurrentHealth() <= 0)
            {
                OnDeathEvent?.Invoke(gameObject);
                healthComponent.SetHealth(Config.HealthDuration);
            }
        }

        private void OnDestroy()
        {
            placeableUnitComponent.UnitPlacedEvent -= EnableWall;
            if (healthComponent != null)
            {
                healthComponent.OnHealthPercentageChanged -= HealthBar.OnHealthChanged;
            }
        }
    }
}
