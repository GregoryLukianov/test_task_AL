using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BusinessesInfo", menuName = "Clicker/BusinessesInfo")]
public class BusinessesInfo : ScriptableObject
{
    public BusinessInfo[] BusinessesInfos;
    
}

[Serializable]
public class BusinessInfo
{
    public int Id;
    public string Name;
        
    [Serializable]
    public class UpgradeInfo
    {
        public string Name;
        public UpgradeType UpgradeType;
    }
        
    public List<UpgradeInfo> Upgrades;
}


