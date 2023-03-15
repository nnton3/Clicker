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
    public class BusinessUpgrade
    {
        public string Label;
        public int RevenueBonus;
        public int UpgradeCost;
        [NonSerialized] public bool Purchased;
    }
}