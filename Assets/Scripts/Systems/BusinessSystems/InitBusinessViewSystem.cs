using Components.BusinessParams;
using Components.Links.UI;
using Leopotam.Ecs;

namespace Systems.BusinessSystems
{
    public class InitBusinessViewSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessViewLink, StaticViewInfo> _viewFilter;

        public void Run()
        {
            if (_viewFilter.IsEmpty()) return;

            foreach (var index in _viewFilter)
            {
                _viewFilter.Get1(index).View.SetLabel(_viewFilter.Get2(index).Label);
                _viewFilter.GetEntity(index).Del<StaticViewInfo>();
            }
        }
    }
}