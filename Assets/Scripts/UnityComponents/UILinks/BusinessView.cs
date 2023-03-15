using Components.BusinessParams;
using Components.Links.UI;
using Leopotam.Ecs;
using TMPro;
using UnityComponents.MonoLinks.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UnityComponents.UILinks
{
    public class BusinessView : MonoLink<BusinessViewLink>
    {
        public float ProgressBarValue => _progressBar.value;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _lvl;
        [SerializeField] private TMP_Text _lvlUpCost;
        [SerializeField] private TMP_Text _revenue;
        [SerializeField] private Slider _progressBar;
        [SerializeField] private BusinessUpgradeView[] _upgrades;

        public override void Make(ref EcsEntity entity)
        {
            entity.Get<BusinessViewLink>() = new BusinessViewLink { View = this };
        }

        public void SetLabel(string label) => _label.text = label;
        public void SetLvl(int lvl) => _lvl.text = lvl.ToString();
        public void SetLvlUpCost(int lvlUpCost) => _lvlUpCost.text = lvlUpCost.ToString();
        public void SetRevenue(int revenue) => _revenue.text = revenue.ToString();
        public void SetProgressBarValue(float progress) => _progressBar.value = progress;

        public void InitUpgrades(BusinessUpgrade[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var tmpData = data[i];
                if (tmpData.Purchased)
                    _upgrades[i].SetPurchasedBanner();
                else
                    _upgrades[i].Init(tmpData.Label, tmpData.RevenueBonus, tmpData.UpgradeCost);
            }
        }

        public void PurchaseUpgrade(int upgradeIndex) =>
            _upgrades[upgradeIndex].SetPurchasedBanner();
    }
}