using System.Collections.Generic;
using Components.BusinessParams;
using Components.WorldStatuses;
using Leopotam.Ecs;
using Utilities;

namespace Systems.BusinessSystems
{
    public class SaveProgressSystem : IEcsRunSystem
    {
        private EcsFilter<OnBusinessesSave> _updateFilter;
        private EcsFilter<Level, RevenuePeriod, BaseRevenue, BaseLvlUpCost, Upgrades, BusinessName> _businessFilter;

        public void Run()
        {
            if (_updateFilter.IsEmpty()) return;

            var businessesData = new List<BusinessData>(_businessFilter.GetEntitiesCount());
            var counter = 0;
            foreach (var index in _businessFilter)
            {
                var businessData = new BusinessData();
                businessData.Lvl = _businessFilter.Get1(index).Value;
                businessData.RevenuePeriod = _businessFilter.Get2(index).Value;
                businessData.BaseRevenue = _businessFilter.Get3(index).Value;
                businessData.BaseLvlUpCost = _businessFilter.Get4(index).Value;
                businessData.Upgrades = _businessFilter.Get5(index).Array;
                businessData.Label = _businessFilter.Get6(index).Value;
                businessesData.Add(businessData);
                counter++;
            }

            SaveUtility.SaveBusinessesProgress(businessesData);

            foreach (var index in _updateFilter)
                _updateFilter.GetEntity(index).Del<OnBusinessesSave>();
        }
    }
}