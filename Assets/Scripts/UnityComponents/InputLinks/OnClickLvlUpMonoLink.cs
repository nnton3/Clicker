using Leopotam.Ecs;
using UnityComponents.MonoLinks.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents.InputLinks
{
    public class OnClickLvlUpMonoLink : InputLinkBase
    {
        [SerializeField] private Button _btn;

        private void Start()
        {
            _btn.onClick.AddListener(() => _entity.Get<OnClickLvlUp>());
        }
    }
}