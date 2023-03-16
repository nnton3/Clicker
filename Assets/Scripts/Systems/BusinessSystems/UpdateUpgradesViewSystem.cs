using Components.BusinessParams;
using Components.Links.UI;
using Leopotam.Ecs;

namespace Systems.BusinessSystems
{
    public class UpdateUpgradesViewSystem : IEcsRunSystem
    {
        private EcsFilter<Upgrades, BusinessViewLink, OnUpdateUpgradesView> _updateFilter;

        public void Run()
        {
            if (_updateFilter.IsEmpty()) return;

            foreach (var index in _updateFilter)
            {
                var view = _updateFilter.Get2(index).View;
                view.InitUpgrades(_updateFilter.Get1(index).Array);
                _updateFilter.GetEntity(index).Del<OnUpdateUpgradesView>();
            }
        }
    }
}