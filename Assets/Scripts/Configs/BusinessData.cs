using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessConfig", menuName = "Clicker/Business Config")]
public class BusinessConfig : ScriptableObject
{
    public BusinessData[] Businesses;
    
    
    [Serializable]
    public class BusinessData
    {
        public string Name;
        public int Id;
        public int StartingLevel;
        public float IncomeDelay;
        public float BasePrice;
        public float BaseIncome;
        
        [Serializable]
        public class UpgradeData
        {
            public string Name;
            public float Price;
            public float IncomeMultiplier;
        }
        
        public UpgradeData Upgrade1;
        public UpgradeData Upgrade2;
    }
}


