using Leopotam.Ecs;
using Components.Common;
using UnityComponents.Factories;

public class SpawnBusinessSystem : IEcsPreInitSystem, IEcsRunSystem
{
    private EcsFilter<SpawnPrefab> _spawnFilter;
    private SceneData _sceneData;
    private PrefabFactory _factory;
    private EcsWorld _world;

    public void PreInit()
    {
        _factory = _sceneData.Factory;
        _factory.SetWorld(_world);
    }

    public void Run()
    {
        if (_spawnFilter.IsEmpty()) return;

        foreach (var index in _spawnFilter)
        {
            _factory.Spawn(_spawnFilter.Get1(index));
            _spawnFilter.GetEntity(index).Del<SpawnPrefab>();
        }
    }
}
