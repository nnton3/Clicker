using Components.WorldStatuses;
using Leopotam.Ecs;

namespace Systems.WorldStatuses
{
    public class UpdateBalanceSystem : IEcsRunSystem
    {
        private EcsFilter<ModifyBalance> _modifyFilter;
        private EcsFilter<Balance> _balanceFilter;
        private SceneData _sceneData;

        public void Run()
        {
            if (_modifyFilter.IsEmpty()) return;

            ref var balance = ref _balanceFilter.Get1(0);
            foreach (var index in _modifyFilter)
            {
                balance.Value += _modifyFilter.Get1(index).Value;
                _modifyFilter.GetEntity(index).Del<ModifyBalance>();
            }

            _sceneData.BalanceView.text = balance.Value.ToString();
        }
    }
}