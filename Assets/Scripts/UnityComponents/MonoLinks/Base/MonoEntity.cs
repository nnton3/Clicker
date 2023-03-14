using Components.Links;
using Leopotam.Ecs;

namespace UnityComponents.MonoLinks.Base
{
    public class MonoEntity : MonoLinkBase
    {
        private EcsEntity _entity;
        private MonoLinkBase[] _monoLinks;

        public MonoLink<T> Get<T>() where T : struct
        {
            foreach (var link in _monoLinks)
                if (link is MonoLink<T> monoLink)
                    return monoLink;

            return null;
        }

        public override void Make(ref EcsEntity entity)
        {
            _entity = entity;

            _monoLinks = GetComponents<MonoLinkBase>();
            foreach (var monoLink in _monoLinks)
            {
                if (monoLink is MonoEntity)
                    continue;

                monoLink.Make(ref entity);
            }

            entity.Get<GameObjectLink>() = new GameObjectLink { Value = gameObject };
        }

        public ref EcsEntity GetEntity() => ref _entity;

        public void AddComponent<T>() where T : struct => _entity.Get<T>();
        public bool Has<T>() where T : struct => _entity.Has<T>();
    }
}
