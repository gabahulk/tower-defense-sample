using UnityEngine;

namespace TowerDefence.Units.Tower
{
    public class Tower : MonoBehaviour
    {
        public ShootingComponent ShootingComponent;
        public Transform TowerHead;
        public Transform SpawnPoint;
        public GameObject BulletPrefab;

        private Vector3 initialRotation;
        // Start is called before the first frame update
        void Start()
        {
            initialRotation = TowerHead.eulerAngles;
            ShootingComponent.Init(10, 1, 1, SpawnPoint, BulletPrefab);
        }

        // Update is called once per frame
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
