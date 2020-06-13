using System;
using UnityEngine;

namespace TowerDefence.Units.Common
{
    public class PlaceableUnitComponent :MonoBehaviour
    {
        public Action UnitPlacedEvent;
        public string PlaceableTerrainTag { get; set; }
    }
}
