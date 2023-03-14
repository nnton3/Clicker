using Leopotam.Ecs;
using UnityEngine;

namespace Components.Common
{
    public struct SpawnPrefab
    {
        public GameObject Prefab;
        public Transform Parent;
        public string Name;
        public EcsEntity Entity;
    }
}
