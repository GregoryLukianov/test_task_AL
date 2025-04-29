using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessConfig", menuName = "Clicker/Business Config")]
public class BusinessesConfig : ScriptableObject
{
    public BusinessConfig[] Businesses;
}

[Serializable]
public class BusinessConfig
{
    public int Id;
    public int StartingLevel;
    public float IncomeDelay;
    public float BasePrice;
    public float BaseIncome;
        
    [Serializable]
    public class UpgradeConfig
    {
        public float Price;
        public float IncomeMultiplier;
        public UpgradeType UpgradeType;
    }
        
    public List<UpgradeConfig> Upgrades;
}

