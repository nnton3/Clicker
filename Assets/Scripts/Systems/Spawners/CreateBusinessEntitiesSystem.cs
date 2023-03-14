using Components.BusinessParams;
using Components.BusinessParams.BusinessUpgradeParams;
using Configs;
using Leopotam.Ecs;

namespace Systems.Spawners
{
    public class CreateBusinessEntitiesSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<InitBusinessData> _initFilter;
        private BusinessConfigSO _gameConfig;
        private EcsWorld _world;

        public void Init()
        {
            foreach (var business in _gameConfig.Businesses)
                _world.NewEntity().Get<InitBusinessData>() = business;
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
                entity.Get<BaseLvlUpCost>() = new BaseLvlUpCost { Value = initData.BaseLvlUpCost };
                //entity.Get<InitUpgradeData>() = new InitUpgradeData { Value = initData.Upgrades };

                entity.Del<InitBusinessData>();
            }
        }
    }
}