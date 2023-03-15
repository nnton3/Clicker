using Components.BusinessParams;
using Leopotam.Ecs;
using UnityComponents.MonoLinks.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents.InputLinks
{
    public class OnClickPurchaseUpgradeMonoLink : InputLinkBase
    {
        [SerializeField] private Button[] _upgradeBtns;

        private void Start()
        {
            for (int i = 0; i < _upgradeBtns.Length; i++)
            {
                var index = i;
                _upgradeBtns[i].onClick.AddListener(() =>
                    _entity.Get<OnClickPurchaseUpgrade>() = new OnClickPurchaseUpgrade
                    { 
                        Index = index 
                    });

            }
        }
    }
}