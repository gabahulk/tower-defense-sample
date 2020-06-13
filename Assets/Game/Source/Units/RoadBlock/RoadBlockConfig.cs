using UnityEngine;
namespace TowerDefence.Units.RoadBlock
{
    [CreateAssetMenu(fileName = "RoadBlockConfig", menuName = "Game/RoadBlockConfig")]
    public class RoadBlockConfig : ScriptableObject
    {
        public string PlaceableTerrainTag;
    }
}