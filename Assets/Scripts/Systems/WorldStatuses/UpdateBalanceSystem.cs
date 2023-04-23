using System.Threading.Tasks;
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
        private readonly int _saveBalanceTimeout = 3000;
        private int _lastBalance;

        public void Init()
        {
            var balance = SaveUtility.LoadBalance();
            _sceneData.BalanceView.text = balance.ToString();
            _world.NewEntity().Get<Balance>() = new Balance { Value = balance };
            SaveBalanceCycle();
        }

        public void Run()
        {
            if (_modifyFilter.IsEmpty()) return;

            ref var balance = ref _balanceFilter.Get1(0);
            foreach (var index in _modifyFilter)
            {
                balance.Value += _modifyFilter.Get1(index).Value;
                _lastBalance = balance.Value;
                _modifyFilter.GetEntity(index).Del<ModifyBalance>();
            }

            _sceneData.BalanceView.text = balance.Value.ToString();
        }

        private async void SaveBalanceCycle()
        {
            while(true)
            {
                await Task.Delay(_saveBalanceTimeout);
                SaveUtility.SaveBalance(_lastBalance);
            }
        }
    }
}