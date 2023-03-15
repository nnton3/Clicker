using Components.BusinessParams;
using Components.WorldStatuses;
using Leopotam.Ecs;

namespace Systems.BusinessSystems
{
    public class LvlUpSystem : IEcsRunSystem
    {
        private EcsFilter<LvlUpCost, Level, OnClickLvlUp> _clickFilter;
        private EcsFilter<Balance> _balanceFilter;
        private EcsWorld _world;

        public void Run()
        {
            if (_clickFilter.IsEmpty()) return;

            foreach (var index in _clickFilter)
            {
                var cost = _clickFilter.Get1(index).Value;
                if (cost > _balanceFilter.Get1(index).Value)
                    continue;

                ref var entity = ref _clickFilter.GetEntity(index);
                var lvl = _clickFilter.Get2(index).Value;
                lvl++;
                _world.NewEntity().Get<ModifyBalance>() = new ModifyBalance
                {
                    Value = -cost
                };
                entity.Get<OnLvlUpdate>() = new OnLvlUpdate { Value = lvl };
                entity.Get<OnLvlUpCostUpdate>() = new OnLvlUpCostUpdate();
                entity.Get<OnRevenueUpdate>() = new OnRevenueUpdate();
            }
        }
    }
}