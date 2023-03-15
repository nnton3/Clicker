using Components.BusinessParams;
using Components.Links.UI;
using Leopotam.Ecs;

namespace Systems.BusinessSystems
{
    public class UpdateRevenueSystem : IEcsRunSystem
    {
        private EcsFilter<Revenue, BaseRevenue, Level, BusinessViewLink, Upgrades, OnRevenueUpdate> _updateFilter;

        public void Run()
        {
            if (_updateFilter.IsEmpty()) return;

            foreach (var index in _updateFilter)
            {
                ref var entity = ref _updateFilter.GetEntity(index);
                ref var revenue = ref _updateFilter.Get1(index);
                var baseRevenue = _updateFilter.Get2(index).Value;
                var lvl = _updateFilter.Get3(index).Value;
                var upgrades = _updateFilter.Get5(index).Array;
                float addtionalValue = 0f;

                foreach (var upgrade in upgrades)
                {
                    if (!upgrade.Purchased) continue;
                    addtionalValue += upgrade.RevenueBonus / 100f;
                }

                revenue.Value = (int)(lvl * baseRevenue * (1f + addtionalValue));

                _updateFilter.Get4(index).View.SetRevenue(revenue.Value);
                entity.Del<OnRevenueUpdate>();
            }
        }
    }
}