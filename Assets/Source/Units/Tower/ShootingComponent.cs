using System;
using UnityEngine;

namespace TowerDefence.Units.Tower
{
    public class ShootingComponent : MonoBehaviour
    {
        private float sightRange;
        private int damage;
        private float rateOfFire;
        private Transform shootingPoint;
        private float fireCountdown = 0f;
        private GameObject bulletPrefab;

        public GameObject target;

        public void Init(float sightRange, int damage, float rateOfFire, Transform shootingPoint, GameObject bulletPrefab)
        {
            this.sightRange = sightRange;
            this.damage = damage;
            this.rateOfFire = rateOfFire;
            this.shootingPoint = shootingPoint;
            this.bulletPrefab = bulletPrefab;
        }

        private void Start()
        {
            InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
        }

        void UpdateTarget()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            float minDistance = Mathf.Infinity;
            GameObject nearestEnemy = default;
            foreach (var enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (minDistance > distance && distance <= sightRange)
                {
                    minDistance = distance;
                    nearestEnemy = enemy;
                }
            }

            target = nearestEnemy;
        }

        private void FixedUpdate()
        {
            if (target == null)
                return;

            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1 / rateOfFire;
            }
            fireCountdown -= Time.deltaTime;
        }

        private void Shoot()
        {
            GameObject bulletGO = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            BulletComponent bullet = bulletGO.GetComponent<BulletComponent>();
            bullet.Init(damage);
            bullet.Seek(target);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

    }
}

