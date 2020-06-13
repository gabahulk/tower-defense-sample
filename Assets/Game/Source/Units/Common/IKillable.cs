using System;
using UnityEngine;

namespace TowerDefence.Units.Common
{
    public interface IKillable
    {
        Action<GameObject> OnDeathEvent { get; set; }
    }
}