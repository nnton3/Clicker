using Components.BusinessParams;
using Components.Links.UI;
using Leopotam.Ecs;

namespace Systems.BusinessSystems
{
    public class UpdateLvlSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessViewLink, Level, OnLvlUpdate> _updateFilter;

        public void Run()
        {
            if (_updateFilter.IsEmpty()) return;

            foreach (var index in _updateFilter)
            {
                ref var entity = ref _updateFilter.GetEntity(index);
                var newLvlValue = _updateFilter.Get3(index).Value;
                ref var lvl = ref _updateFilter.Get2(index);
                lvl.Value = newLvlValue;
                _updateFilter.Get1(index).View.SetLvl(newLvlValue);
                entity.Del<OnLvlUpdate>();
            }
        }
    }
}