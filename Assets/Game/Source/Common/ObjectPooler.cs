using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Common
{
    /// <summary>
    /// A simple ObjectPooler class for GameObjects
    /// Usage: 
    /// Instantiate a pool passing your prefab.
    /// Instead of instantiating your prefab, get one from the pool using the GetObject() method.
    /// Instead of killing your object, just set it inactive using the SetActive() method.
    /// </summary>
    public class ObjectPooler
    {
        private List<GameObject> pooledObjects;
        private GameObject pooledObjectPrefab;

        public ObjectPooler(int initialNumberOfInstances, GameObject pooledObjectPrefab)
        {
            this.pooledObjectPrefab = pooledObjectPrefab;
            pooledObjects = new List<GameObject>(initialNumberOfInstances);
            for (int i = 0; i < initialNumberOfInstances; i++)
            {
                var item = CreateNewPooledObject(false, " [Pooled] " + i);
                pooledObjects.Add(item);
                item.SetActive(false);
            }
        }

        public GameObject GetObject()
        {
            if (HasAvailableObject())
            {
                return GetAvailableObject();
            }

            return CreateNewPooledObject(true, "[Pooled] [Extra]");
        }

        private GameObject GetAvailableObject()
        {
            foreach (GameObject item in pooledObjects)
            {
                if (!item.activeSelf)
                {
                    item.SetActive(true);
                    return item;
                }
            }

            return null;
        }


        private GameObject CreateNewPooledObject(bool startsActive, string name)
        {
            GameObject obj = Object.Instantiate(pooledObjectPrefab);
            obj.name += name;
            pooledObjects.Add(obj);
            obj.SetActive(startsActive);

            return obj;
        }

        public bool HasAvailableObject()
        {
            foreach (GameObject item in pooledObjects)
            {
                if (!item.activeSelf)
                {
                    return true;
                }
            }

            return false;
        }

        public List<GameObject> GetAllObjects()
        {
            return pooledObjects;
        }
    }
}