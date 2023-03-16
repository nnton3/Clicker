using System;

namespace Components.BusinessParams
{
    [Serializable]
    public struct BusinessData
    {
        public string Label;
        public int Lvl;
        public int BaseRevenue;
        public int BaseLvlUpCost;
        public float RevenuePeriod;
        public BusinessUpgrade[] Upgrades;
    }

    [Serializable]
    public struct BusinessUpgrade
    {
        public string Label;
        public int RevenueBonus;
        public int UpgradeCost;
        /*[HideInInspector] */public bool Purchased;

        public BusinessUpgrade(BusinessUpgrade data)
        {
            Label = data.Label;
            RevenueBonus = data.RevenueBonus;
            UpgradeCost = data.UpgradeCost;
            Purchased = data.Purchased;
        }
    }
}