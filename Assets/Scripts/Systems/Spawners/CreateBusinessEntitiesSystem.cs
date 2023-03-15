using Components.BusinessParams;
using Components.Common;
using Leopotam.Ecs;

namespace Systems.Spawners
{
    public class CreateBusinessEntitiesSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<BusinessData> _initFilter;
        private SceneData _sceneData;
        private StaticData _staticData;
        private EcsWorld _world;

        public void Init()
        {
            foreach (var business in _sceneData._gameConfig.Businesses)
                _world.NewEntity().Get<BusinessData>() = business;
        }

        public void Run()
        {
            if (_initFilter.IsEmpty()) return;

            foreach (var index in _initFilter)
            {
                ref var entity = ref _initFilter.GetEntity(index);
                var initData = _initFilter.Get1(index);
                entity.Get<StaticViewInfo>() = new StaticViewInfo { Label = initData.Label };
                entity.Get<Level>() = new Level { Value = initData.Lvl };
                entity.Get<RevenuePeriod>() = new RevenuePeriod { Value = initData.RevenuePeriod };
                entity.Get<BaseRevenue>() = new BaseRevenue { Value = initData.BaseRevenue };
                entity.Get<Revenue>() = new Revenue { Value = initData.BaseRevenue };
                entity.Get<BaseLvlUpCost>() = new BaseLvlUpCost { Value = initData.BaseLvlUpCost };
                entity.Get<LvlUpCost>() = new LvlUpCost { Value = initData.BaseLvlUpCost };

                entity.Get<Upgrades>() = new Upgrades
                {
                    Array = initData.Upgrades
                };

                entity.Get<OnLvlUpdate>() = new OnLvlUpdate { Value = initData.Lvl };
                entity.Get<OnLvlUpCostUpdate>() = new OnLvlUpCostUpdate();
                entity.Get<OnRevenueUpdate>() = new OnRevenueUpdate();
                entity.Get<OnUpdateUpgradesView>() = new OnUpdateUpgradesView();

                entity.Get<SpawnPrefab>() = new SpawnPrefab
                {
                    Entity = entity,
                    Name = initData.Label,
                    Parent = _sceneData.BusinessParent,
                    Prefab = _staticData.BusinessPref
                };

                entity.Del<BusinessData>();
            }
        }
    }
}