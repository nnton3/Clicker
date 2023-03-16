using Components.BusinessParams;
using Components.Links.UI;
using Components.WorldStatuses;
using Leopotam.Ecs;

namespace Systems.BusinessSystems
{
    public class PurchaseUpgradeSystem : IEcsRunSystem
    {
        private EcsFilter<Upgrades, BusinessViewLink, OnClickPurchaseUpgrade> _updateFilter;
        private EcsFilter<Balance> _balanceFilter;
        private EcsWorld _world;

        public void Run()
        {
            if (_updateFilter.IsEmpty()) return;

            foreach (var index in _updateFilter)
            {
                ref var upgrades = ref _updateFilter.Get1(index);
                var upgradeIndex = _updateFilter.Get3(index).Index;
                var targetUpgrade = upgrades.Array[upgradeIndex];
                if (targetUpgrade.UpgradeCost > _balanceFilter.Get1(0).Value) continue;

                targetUpgrade.Purchased = true;
                _world.NewEntity().Get<ModifyBalance>() = new ModifyBalance
                {
                    Value = -targetUpgrade.UpgradeCost
                };
                var view = _updateFilter.Get2(index).View;
                upgrades.Array[upgradeIndex].Purchased = true;
                view.PurchaseUpgrade(upgradeIndex);
                _updateFilter.GetEntity(index).Get<OnRevenueUpdate>();
                _world.NewEntity().Get<OnBusinessesSave>();
            }
        }
    }
}