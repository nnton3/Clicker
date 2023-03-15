using Components.WorldStatuses;
using Leopotam.Ecs;
using Utilities;

namespace Systems.WorldStatuses
{
    public class UpdateBalanceSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<ModifyBalance> _modifyFilter;
        private EcsFilter<Balance> _balanceFilter;
        private SceneData _sceneData;
        private EcsWorld _world;

        public void Init()
        {
            var balance = SaveUtility.LoadBalance();
            _sceneData.BalanceView.text = balance.ToString();
            _world.NewEntity().Get<Balance>() = new Balance { Value = balance };
        }

        public void Run()
        {
            if (_modifyFilter.IsEmpty()) return;

            ref var balance = ref _balanceFilter.Get1(0);
            foreach (var index in _modifyFilter)
            {
                balance.Value += _modifyFilter.Get1(index).Value;
                _modifyFilter.GetEntity(index).Del<ModifyBalance>();
                SaveUtility.SaveBalance(balance.Value);
            }

            _sceneData.BalanceView.text = balance.Value.ToString();
        }
    }
}