using Components.Common;
using Leopotam.Ecs;
using UnityComponents.MonoLinks.Base;
using UnityEngine;

namespace UnityComponents.Factories
{
    public class PrefabFactory : MonoBehaviour
    {
        private EcsWorld _world;

        public void SetWorld(EcsWorld world)
        {
            _world = world;
        }

        public void Spawn(SpawnPrefab spawnData)
        {
            var prefInstance = Instantiate(
                spawnData.Prefab,
                spawnData.Parent);

            prefInstance.name = (spawnData.Name == string.Empty) ? spawnData.Prefab.name : spawnData.Name;
            var monoEntity = prefInstance.GetComponent<MonoEntity>();

            var entity = _world.NewEntity();
            monoEntity.Make(ref entity);
        }
    }
}
