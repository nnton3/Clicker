using System.Collections.Generic;
using Components.BusinessParams;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "NewBusiness", menuName = "CreateBusinessConfig")]
    public class BusinessConfigSO : ScriptableObject
    {
        public List<BusinessData> Businesses;
    }
}