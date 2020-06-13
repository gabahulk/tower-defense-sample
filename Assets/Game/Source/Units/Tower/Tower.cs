using TowerDefence.Units.Common;
using UnityEngine;

namespace TowerDefence.Units.Tower
{
    public class Tower : MonoBehaviour
    {
        public ShootingComponent ShootingComponent;
        public Transform TowerHead;
        public Transform SpawnPoint;
        public GameObject BulletPrefab;
        public TowerConfig Config;
        [SerializeField]
        private PlaceableUnitComponent placeableUnitComponent = default;

        private Vector3 initialRotation;

        private void Awake()
        {
            Init(Config);
        }

        void Init(TowerConfig Config)
        {
            initialRotation = TowerHead.eulerAngles;
            ShootingComponent.Init(Config.SightRange, Config.Damage, Config.RateOfFire, SpawnPoint, BulletPrefab);

            placeableUnitComponent.PlaceableTerrainTag = Config.PlaceableTerrainTag;
        }

        void Update()
        {
            if (ShootingComponent.target == null)
            {
                TowerHead.eulerAngles = initialRotation;
                return;
            }
           
            TowerHead.LookAt(ShootingComponent.target.transform.position);
        }

    }
}
