using UnityEngine;
using Leopotam.Ecs;
using Configs;
using Systems.Spawners;
using UnityComponents.Factories;
using Systems.BusinessSystems;
using Systems.WorldStatuses;
using Components.BusinessParams;

public class MainSceneEcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private EcsSystems _fixedSystem;
    [SerializeField] private SceneData _sceneData;
    [SerializeField] private StaticData _statciData;

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

        var spawnSystems = SpawnSystems();
        var releaseSystems = ReleaseSystems();
        var updateViewSystems = UpdateViewSystems();
        var businessManagmentSystems = BusinessManagmentSystems();

        _systems
            .Add(spawnSystems)
            .Add(businessManagmentSystems)
            .Add(updateViewSystems)
            .Add(releaseSystems)
            .Add(new SaveProgressSystem())
            .Inject(_sceneData)
            .Inject(_statciData)
            .Init();

        _fixedSystem
            .Add(new UpdateProgressBarSystem())
            .Inject(_sceneData)
            .Inject(_statciData)
            .Init(); ;
    }

    private void Update()
    {
        _systems?.Run();
    }

    private void FixedUpdate()
    {
        _fixedSystem?.Run();
    }

    private EcsSystems SpawnSystems()
    {
        return new EcsSystems(_world)
            .Add(new CreateBusinessEntitiesSystem())
            .Add(new SpawnBusinessSystem());
    }

    private EcsSystems ReleaseSystems()
    {
        return new EcsSystems(_world)
            .OneFrame<OnClickPurchaseUpgrade>()
            .OneFrame<OnClickLvlUp>();
    }

    private EcsSystems UpdateViewSystems()
    {
        return new EcsSystems(_world)
            .Add(new InitBusinessViewSystem())
            .Add(new UpdateLvlSystem())
            .Add(new UpdateLvlUpCostSystem())
            .Add(new UpdateRevenueSystem())
            .Add(new PurchaseUpgradeSystem())
            .Add(new UpdateUpgradesViewSystem())
            .Add(new UpdateBalanceSystem());
    }

    private EcsSystems BusinessManagmentSystems()
    {
        return new EcsSystems(_world)
            .Add(new LvlUpSystem());
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
