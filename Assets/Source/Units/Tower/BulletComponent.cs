using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefence.Units.Enemy;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public GameObject target;
    private float speed = 80f;
    private int damage;

    public void Init(int bulletDamage)
    {
        damage = bulletDamage;
    }

    public void Seek(GameObject targetToSeek)
    {
        target = targetToSeek;
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = target.transform.position - transform.position;
        float distanceToWalk = speed * Time.deltaTime;

        if (direction.magnitude <= distanceToWalk)
        {
            HitTarget(target);
        }
        transform.Translate((target.transform.position - transform.position).normalized * distanceToWalk, Space.World);
    }

    private void HitTarget(GameObject target)
    {
        target.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
