using Components.BusinessParams;
using Components.Links.UI;
using Leopotam.Ecs;

namespace Systems.BusinessSystems
{
    public class UpdateLvlUpCostSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessViewLink, Level, BaseLvlUpCost, LvlUpCost, OnLvlUpCostUpdate> _updateFilter;

        public void Run()
        {
            if (_updateFilter.IsEmpty()) return;

            foreach (var index in _updateFilter)
            {
                var baseCost = _updateFilter.Get3(index).Value;
                var lvl = _updateFilter.Get2(index).Value;
                ref var cost = ref _updateFilter.Get4(index);
                cost.Value = (lvl + 1) * baseCost;
                _updateFilter.Get1(index).View.SetLvlUpCost(cost.Value);

                _updateFilter.GetEntity(index).Del<OnLvlUpCostUpdate>();
            }
        }
    }
}