using UnityEngine;
using Leopotam.Ecs;
using Configs;
using Systems.Spawners;

public class MainSceneEcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private EcsSystems _fixedSystem;
    [SerializeField] private BusinessConfigSO _gameConfig; 

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world, "UpdateSystems");
        _fixedSystem = new EcsSystems(_world, "FixedUpdateSystems");
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystem);
#endif

        _systems
            .Add(new CreateBusinessEntitiesSystem())
            .Inject(_gameConfig)
            .Init();
    }

    private void Update()
    {
        _systems?.Run();
    }

    private void FixedUpdate()
    {
        //_fixedSystem?.Run();
    }

    void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;

            _fixedSystem.Destroy();
            _fixedSystem = null;

            _world.Destroy();
            _world = null;
        }
    }
}
