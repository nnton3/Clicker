using Components.BusinessParams;
using Components.Links.UI;
using Components.WorldStatuses;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.BusinessSystems
{
    public class UpdateProgressBarSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessViewLink, RevenuePeriod, Revenue> _viewFilter;
        private EcsWorld _world;

        public void Run()
        {
            if (_viewFilter.IsEmpty()) return;

            foreach (var index in _viewFilter)
            {
                var revenue = _viewFilter.Get3(index).Value;
                if (revenue == 0) continue;

                var view = _viewFilter.Get1(index).View;
                var period = _viewFilter.Get2(index).Value;
                var progress = view.ProgressBarValue;
                var additional = Time.fixedDeltaTime / period;
                if (progress + additional > 1f)
                {
                    progress = 0f;
                    _world.NewEntity().Get<ModifyBalance>() = new ModifyBalance
                    {
                        Value = revenue
                    };
                }
                else
                    progress += additional;
                _viewFilter.Get1(index).View.SetProgressBarValue(progress);
            }
        }
    }
}