using UnityEngine;

namespace TowerDefence.Units.Tower
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "Game/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {
        public float SightRange;
        public int Damage;
        public float RateOfFire;
        public string PlaceableTerrainTag;
    }
}
