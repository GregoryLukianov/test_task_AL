using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public BalanceComponent Balance;
    public List<BusinessData> BusinessDatas = new List<BusinessData>();
}

[Serializable]
public class BusinessData
{
    public int Id;
    public BusinessIncomeTimerComponent IncomeTimer;
    public BusinessComponent Business;
}