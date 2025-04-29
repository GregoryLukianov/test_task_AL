using System.Collections.Generic;

public struct BusinessComponent
{
    public int Id;
    public int Level;
    public Dictionary<UpgradeType, bool> PurchasedUpgradesDictionary;
}